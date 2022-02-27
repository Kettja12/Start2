using Start2.Server.DBContext;
using Start2.Shared;
using Start2.Shared.Model.Dashboard;
using System.Text.Json;

namespace Start2.Server.Services
{
    public class DashboardServices : ApiServices
    {
     

        public DashboardServices(IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor, 
            ILogger<ApiServices> logger, 
            StartContext db) : base(configuration, httpContextAccessor, logger, db)
        {
        }
        public async Task<IResult> GetItem1Async(Item1 postParams)
        {
            try
            {
                await Task.Delay(5000);
                //if (postParams.Data == null)
                //{
                //    var claims = await db.GetClaimsByUserIdAsync(UserId);
                //    postParams.Data = claims.GetClaim("Item1Params");
                //}
                //else
                //{
                //    var claims = await db.GetClaimsByUserIdAsync(UserId);
                //    claims.SetClaim("Item1Params", postParams.Data);
                //    _ = await db.SaveClaimsByUserIdAsync(claims,UserId);
                //}
                postParams.Renevue = new double[4];
                postParams.Renevue2 = new double[4];
                Random rnd = new Random();
                for (var i = 0; i < 4; i++)
                {
                    postParams.Renevue[i] = rnd.Next(10000, 30000);
                    postParams.Renevue2[i] = rnd.Next(10000, 30000);
                }
                return Results.Ok(postParams);
            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Item1 search failed.");
        }

        public async Task<IResult> GetItem2Async(Item2 postParams)
        {
            try
            {
                await Task.Delay(2000);
                if (string.IsNullOrEmpty(postParams.Data))
                {
                    var claims = await db.GetClaimsByUserIdAsync(UserId);
                    postParams.Data = claims.GetClaim("Item2Params");
                }
                else
                {
                    var claims = await db.GetClaimsByUserIdAsync(UserId);
                    var claim=claims.SetClaim("Item2Params", postParams.Data);
                    _ = await db.SaveClaimAsync(claim);
                }

                return Results.Ok(postParams);
            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Item2 search failed.");
        }

        public async Task<IResult> GetItem3Async(Item3 postParams)
        {
            try
            {
                await Task.Delay(3000);
                if (postParams.A==null || postParams.B==null ) return Results.Ok(postParams);
                postParams.Result = "";
                int a = 0;
                if (int.TryParse(postParams.A,out a))
                {
                    int b = 0;
                    if (int.TryParse(postParams.B, out b))
                    {
                        postParams.Result = (a+b).ToString();
                    }

                }
                return Results.Ok(postParams);
            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Item3 call failed. Invalid parameters.");
        }


        private Task<IResult> SaveDashboardItemAsync(DashboardItem s)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> GetDashboardItemsAsync()
        {

            try
            {
                List<DashboardItem>? items = await db.GetDasboardItemsAsync();
                return Results.Ok(items);
            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }

            return Results.Conflict("DasboardItemlist search failed.");

        }
    }
}
