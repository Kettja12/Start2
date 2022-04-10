using Start2.DBContext;
using Start2.Shared;
using System.Text.Json;

namespace Start2.Server.Services
{
    public class ApiServices
    {
        public readonly IConfiguration configuration;
        public readonly StartContext db;
        public readonly ILogger logger;
        public readonly HttpContext? httpContext;
        public readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public string UserId
        {
            get
            {
                try
                {
                    if (httpContext == null) return "";
                    if (httpContext.User == null) return "";
                    var id = httpContext.User.FindFirst("UserID");
                    if (id == null) return "";
                    return id.Value;
                }
                catch (Exception e)
                {
                    logger.LogError(e.StackTrace);
                }
                return "";
            }
        }

        public async Task<bool> CheckIsAdminAsync(string userId)
        {
            var claims = await db.GetClaimsByUserIdAsync(userId);
            if (claims == null) return false;
            return claims.IsAdmin();
        }

        public ApiServices(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ApiServices> logger,
            StartContext db
            )
        {
            this.configuration = configuration;
            httpContext = httpContextAccessor.HttpContext;
            this.logger = logger;
            this.db = db;
  
        }
       
        public IResult NoData()
        {
            return Results.Conflict("No data.");
        }

        public IResult UnacceptedContent()
        {
            return Results.Conflict("Unaccepted Content.");
        }


        public IResult InvalidParameters()
        {
            return Results.Conflict("Invalid parameters.");
        }

        public IResult AccessDenied()
        {
            return Results.Conflict("Access denied.");
        }

    }

}
