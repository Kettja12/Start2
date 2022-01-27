using Start2.Server.DBContext;
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

        public async Task<IResult> RunService(string service, object postParams)
        {
            while (true)
            {

                if (service == "GetDashboardItems")
                {
                    return await GetDashboardItemsAsync();

                }

                if (postParams == null) break;
                string? p = postParams.ToString();
                if (p == null) break;

                if (service == "SaveDashboardItem")
                {
                    var s = JsonSerializer.Deserialize<DashboardItem>(p, jsonSerializerOptions);
                    if (s == null) break;
                    return await SaveDashboardItemAsync(s);
                }

                if (service == "GetItem1")
                {
                    var s = JsonSerializer.Deserialize<Item1>(p, jsonSerializerOptions);
                    if (s == null) break;
                    return await GetItem1Async(s);
                }

                if (service == "GetItem2")
                {
                    var s = JsonSerializer.Deserialize<Item2>(p, jsonSerializerOptions);
                    if (s == null) break;
                    return await GetItem2Async(s);
                }
                if (service == "GetItem3")
                {
                    var s = JsonSerializer.Deserialize<Item3>(p, jsonSerializerOptions);
                    return await GetItem3Async(s);
                }
            }
            return InvalidParameters();
        }

        private Task<IResult> GetItem3Async(Item3? s)
        {
            throw new NotImplementedException();
        }

        private Task<IResult> GetItem2Async(Item2 s)
        {
            throw new NotImplementedException();
        }

        private Task<IResult> GetItem1Async(Item1 s)
        {
            throw new NotImplementedException();
        }

        private Task<IResult> SaveDashboardItemAsync(DashboardItem s)
        {
            throw new NotImplementedException();
        }

        private async Task<IResult> GetDashboardItemsAsync()
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
