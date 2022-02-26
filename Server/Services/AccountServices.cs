using Microsoft.IdentityModel.Tokens;
using Start2.Server.DBContext;
using Start2.Shared.Model;
using Start2.Shared.Model.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Claim = Start2.Shared.Model.Account.Claim;

namespace Start2.Server.Services;
public class AccountServices : ApiServices
{
    public AccountServices(IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ApiServices> logger,
        StartContext db) : base(configuration, httpContextAccessor, logger, db)
    {
    }
    public async Task<IResult> LoginAsync(LoginModel postParams)
    {
        try
        {
            while (true)
            {
                if (
                    string.IsNullOrEmpty(postParams.Username)
                    || string.IsNullOrEmpty(postParams.Password)
                    ) break;

                User? user = await db.GetUserByUsernameAsync(postParams.Username);
                if (user == null) break;
                List<Claim>? claims = await db.GetClaimsByUserIdAsync(user.Id);
                if (claims == null) break;

                string? s = postParams.Username.ToUpper().Trim() + postParams.Password.Trim();
                string? token = CreateToken(s);
                LoginToken? loginToken = await db.GetLoginTokenAsunc(user.Id);
                if (loginToken == null)
                {
                    loginToken = await db.SaveLoginTokenAsync(user.Id, token);
                }
                if (loginToken == null || loginToken.Value != token) break;
                token = CreateJWTToken(user.Id);

                var result = new LoginResponseModel()
                {
                    SessioneExpires = DateTime.UtcNow.AddDays(1),
                    Token = token,
                    User = user,
                    Claims = claims
                };
                return Results.Ok(result);

            }
        }
        catch (Exception e)
        {
            logger.LogError("Error: ", e);
        }
        return Results.Conflict("Login attempt failed.");
    }
    public async Task<IResult> GetUsersAsync(UserSearchModel postParams)
    {
        try
        {
            bool hasrights = await CheckIsAdminAsync(UserId);
            if (hasrights == false)
            {
                return AccessDenied();
            }
            List<User>? users = null;
            if (postParams.Searchkey == "1")
            {
                users = await db.GetUsersByUsernameAsync(postParams.Searchfield ?? "");
            }
            else
                users = await db.GetUsersByLastNameAsync(postParams.Searchfield ?? "");
            return Results.Ok(users);
        }
        catch (Exception e)
        {
            logger.LogError("Error: ", e);
        }
        return Results.Conflict("Userlist search failed.");
    }
    public async Task<IResult> SaveUserAsync(User postParams)
    {
        try
        {

            if (postParams == null) return NoData();
            string message = "";
            if (string.IsNullOrEmpty(postParams.Username))
                message = "Username missing.";
            if (postParams.FirstName == null)
                message += " "+"Firstname missing.";
            if (postParams.LastName == null)
                message += " " + "Lastname missing.";

            if (string.IsNullOrEmpty(message)==false)
                return Results.Conflict(message);

            User? exitingUser = await db.GetUserByUsernameAsync(
                postParams.Username);
            if (postParams.Id == 0)
            {
                if (exitingUser != null)
                {
                    message = "Username is already in use.";
                    return Results.Conflict(message);
                }
            }
            else
            {
                if (exitingUser != null && exitingUser.Id != postParams.Id)
                {
                    message = "Username is already in use.";
                    return Results.Conflict(message);
                }
            }
            await using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                User? user = await db.SaveUserAsync(postParams, exitingUser);
                await db.Database.CommitTransactionAsync();
                return Results.Ok(user);
            }
            catch
            {
                await db.Database.RollbackTransactionAsync();
                throw;
            }

        }
        catch (Exception e)
        {
            logger.LogError("Error: ", e);
        }
        return Results.Conflict("User save failed.");
    }
    public async Task<IResult> SavePasswordAsync(SavePasswordModel postParams)
    {
        try
        {
            if (postParams.Username == null
                || postParams.NewPassword == null)
                return NoData();
            bool adminupdate = false;
            if (string.IsNullOrEmpty(postParams.OldPassword))
            {
                bool isAdmin = await CheckIsAdminAsync(UserId);
                if (isAdmin == false)
                    return AccessDenied();
                adminupdate = true;
            }
            User? exitingUser = await db.GetUserByUsernameAsync(postParams.Username);
            if (exitingUser != null)
            {
                bool userupdate = false;
                if (adminupdate == false)
                {
                    if (postParams.OldPassword == null)
                        return NoData();
                    var s = postParams.Username.ToUpper().Trim() + postParams.OldPassword.Trim();
                    userupdate = UserId == exitingUser.Id;
                }
                if (adminupdate || userupdate)
                {
                    string? token = CreateToken(
                        (postParams.Username.ToUpper() +
                         postParams.NewPassword));
                    _ = await db.SaveLoginTokenAsync(exitingUser.Id, token);
                    return Results.Ok("Password save success.");
                };
            }
        }
        catch (Exception e)
        {
            logger.LogError("ex: ", e);
        }
        return Results.Conflict("Password save failed.");
    }
    public async Task<IResult> GetClaimsAsync(int userId)
    {
        try
        {
            List<Claim>? claims = await db.GetClaimsByUserIdAsync(userId);
            return Results.Ok(claims);
        }
        catch (Exception e)
        {
            logger.LogError("Error: ", e);
        }
        return Results.Conflict("Claims search failed.");
    }
    public async Task<IResult> SaveClaimAsync(Claim postParams)
    {
        try
        {
            if (postParams.UserId == 0)
                return InvalidParameters();
            bool isAdmin = await CheckIsAdminAsync(UserId);
            if (isAdmin == false)
                return AccessDenied();
            Claim? claim = await db.SaveClaimAsync(postParams);
            return Results.Ok(claim);
        }
        catch (Exception e)
        {
            logger.LogError("ex: ", e);
        }
        return Results.Conflict("Claim save failed.");
    }
    private string CreateJWTToken(int UserId)
    {
        string? JWTPassword = configuration["Appsettings:JWTPassword"];
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new System.Security.Claims.Claim[] {
                new System.Security.Claims.Claim("UserID", UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JWTPassword)),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken? securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);


    }
    public string CreateToken(string token)
    {
        if (token == "") return Guid.NewGuid().ToString();
        byte[]? dataArray = Encoding.ASCII.GetBytes(token);
        HashAlgorithm sha = SHA256.Create();
        byte[] result = sha.ComputeHash(dataArray);
        return Encoding.ASCII.GetString(result);
    }


}
