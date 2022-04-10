using Microsoft.EntityFrameworkCore;
using NUlid;
using Start2.Shared.Model.Account;

namespace Start2.DBContext;

public partial class StartContext
{
    public async Task<User?> GetUserByIdAsync(string id)
    {
        User? user = await Users
            .Include(c => c.Claims)
            .FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        User? user = await Users
            .Include(c => c.Claims)
            .FirstOrDefaultAsync(x => x.Username == username);
        return user;
    }
    public async Task<List<User>?> GetUsersByUsernameAsync(string username)
    {
        List<User>? user = await Users.Where(x => x.Username == username)
            .Include(c => c.Claims)
            .ToListAsync();
        return user;
    }
    public async Task<List<User>?> GetUsersByLastNameAsync(string lastname)
    {
        return await Users.Where(x => x.LastName.Contains(lastname))
            .Include(c => c.Claims)
            .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
            .Take(20)
            .ToListAsync();
    }


    public async Task<User> SaveUserAsync(User user, User? existingUser)
    {

        if (existingUser != null)
        {
            Entry(existingUser).State = EntityState.Detached;
            Entry(user).State = EntityState.Modified;
        }
        else
        {
            Entry(user).State = EntityState.Added;
            if (user.Id == "")
            {
                user.Id = Ulid.NewUlid().ToString();
            }
        }
        user.Modified = DateTime.Now;
        await SaveChangesAsync();
        return user;
    }
    public async Task<LoginToken?> GetLoginTokenAsunc(string UserId)
    {
        return await LoginTokens.FirstOrDefaultAsync(x => x.UserId == UserId);
    }

    public async Task<LoginToken?> SaveLoginTokenAsync(string UserId, string token)
    {
        LoginToken? logintoken = await GetLoginTokenAsunc(UserId);
        if (logintoken == null)
        {
            logintoken = new LoginToken()
            {
                Id = Ulid.NewUlid().ToString(),
                UserId = UserId,
                Value = token
            };
            LoginTokens.Add(logintoken);
        }
        else
        {
            logintoken.Value = token;
        }
        logintoken.Modified = DateTime.Now;
        await SaveChangesAsync();
        return logintoken;
    }

    public async Task<List<Claim>> GetClaimsByUserIdAsync(string userId)
    {
        if (Claims != null)
        {
            List<Claim>? result = await Claims.Where(x => x.UserId == userId).ToListAsync();
            if (result != null)
                return result;
        }
        return new List<Claim>();
    }

    public async Task<Claim> SaveClaimAsync(Claim claim)
    {
        if (claim.Id != "")
        {
            Claim? oldclaim = await Claims.FirstOrDefaultAsync(x => x.Id == claim.Id);
            if (oldclaim! != null)
            {
                oldclaim.ClaimValue = claim.ClaimValue;
            }
        }
        else
        {
            claim.Id = Ulid.NewUlid().ToString();
            await AddAsync(claim);
        }

        claim.Modified = DateTime.Now;
        await SaveChangesAsync();
        return claim;
    }

}
