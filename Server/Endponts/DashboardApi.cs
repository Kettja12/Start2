using Start2.Server.Services;
using Start2.Shared;
using Start2.Shared.Model.Dashboard;

namespace Start2.Server.Endponts;
public static partial class Endpoints
{
    public static void MapDashboardApi(IEndpointRouteBuilder endpoints)
    {

        _ = endpoints.MapGet(APIServices.DashboardGetDashboardItems,
            async (DashboardServices services) =>
         {
             return await services.GetDashboardItemsAsync();
         });

        _ = endpoints.MapPost(APIServices.DashboardSaveDashboardItem,
            async (DashboardServices services, DashboardItem postParams) =>
        {
            return await services.SaveDashboardItemAsync(postParams);
        });

        _ = endpoints.MapPost(APIServices.DashboardGetItem1,
            async (DashboardServices services, Item1 postParams) =>
        {
            if (CheckObject(postParams) == false) return services.UnacceptedContent();
            return await services.GetItem1Async(postParams);
        });
        _ = endpoints.MapPost(APIServices.DashboardGetItem2,
            async (DashboardServices services, Item2 postParams) =>
            {
                if (CheckObject(postParams) == false) return services.UnacceptedContent();
                return await services.GetItem2Async(postParams);
            });
        _ = endpoints.MapPost(APIServices.DashboardGetItem3,
            async (DashboardServices services, Item3 postParams) =>
            {
                if (CheckObject(postParams) == false) return services.UnacceptedContent();
                return await services.GetItem3Async(postParams);
            });

    }
}
