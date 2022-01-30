using Start2.Server.Services;
using Start2.Shared;
using Start2.Shared.Model.Dashboard;

namespace Start2.Server.Endponts;
public static class DashboardApi
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {

        _ = endpoints.MapGet(APIServices.DashboardGetDashboardItems,
            async (DashboardServices services) =>
         {
             return await services.GetDashboardItemsAsync();
         });

        //_ = endpoints.MapPost(APIServices.DashboardSaveDashboardItem,
        //    async (ApiServices services, DashboardItemModel postParams) =>
        //{
        //    if (postParams == null) return services.NoParamers();
        //    return await services.SaveDashboardItemAsync(postParams);
        //});

        _ = endpoints.MapPost(APIServices.DashboardGetItem1,
            async (DashboardServices services, Item1 postParams) =>
        {
            if (postParams == null) return services.InvalidParameters(); 
            return await services.GetItem1Async(postParams);
        });
        _ = endpoints.MapPost(APIServices.DashboardGetItem2,
            async (DashboardServices services, Item2 postParams) =>
            {
                if (postParams == null) return services.InvalidParameters();
                return await services.GetItem2Async(postParams);
            });
        _ = endpoints.MapPost(APIServices.DashboardGetItem3,
            async (DashboardServices services, Item3 postParams) =>
            {
                if (postParams == null) return services.InvalidParameters();
                return await services.GetItem3Async(postParams);
            });

    }
}
