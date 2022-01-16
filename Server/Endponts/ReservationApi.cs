//using Api.Reservation;

//namespace Services;
//public static class ReservationApi
//{
//    public static void Map(IEndpointRouteBuilder endpoints)
//    {

//        _ = endpoints.MapGet(Api.Services.ReservationGetReservationNodes,
//            async (ApiServices services) =>
//         {
//             return await services.GetReservationNodesAsync();
//         });

//        _ = endpoints.MapPost(Api.Services.ReservationSaveReservationNode,
//            async (ApiServices services, ReservationNodeModel postParams) =>
//        {
//            if (postParams == null) return services.NoParamers();
//            return await services.SaveReservationNodeAsync(postParams);
//        });
//    }
//}
