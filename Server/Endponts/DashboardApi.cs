using Start2.Shared;

namespace Services.Endponts;

public static class DashboardApi
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {

        //_ = endpoints.MapGet(APIServices.DashboardGetDashboardItems,
        //    async (ApiServices services) =>
        // {
        //     return await services.GetDashboardItemsAsync();
        // });

        //_ = endpoints.MapPost(Api.Services.DashboardSaveDashboardItem,
        //    async (ApiServices services, DashboardItemModel postParams) =>
        //{
        //    if (postParams == null) return services.NoParamers();
        //    return await services.SaveDashboardItemAsync(postParams);
        //});

        //_ = endpoints.MapPost(Api.Services.DashboardGetItem1,
        //    async (ApiServices services, Item1Model postParams) =>
        //{
        //    if (postParams == null) return services.NoParamers();
        //    return await services.GetItem1Async(postParams);
        //});

        //_ = endpoints.MapPost(Api.Services.DashboardGetItem2,
        //    async (ApiServices services, Item2Model postParams) =>
        //{
        //    if (postParams == null) return services.NoParamers();
        //    return await services.GetItem2Async(postParams);
        //});

        //_ = endpoints.MapPost(Api.Services.DashboardGetItem3,
        //    async (ApiServices services, Item3Model postParams) =>
        //{
        //    if (postParams == null) return services.NoParamers();
        //    return await services.GetItem3Async(postParams);
        //});
    }
}
