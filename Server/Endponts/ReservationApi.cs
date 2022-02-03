
using Start2.Server.Services;
using Start2.Shared;
using Start2.Shared.Model.Reservation;

namespace Start2.Server.Endponts;
public static class ReservationApi
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {

        _ = endpoints.MapGet(APIServices.ReservationGetReservationNodes,
            async (ReservationServices services) =>
         {
             return await services.GetReservationNodesAsync();
        });

        _ = endpoints.MapPost(APIServices.ReservationSaveReservationNode,
            async (ReservationServices services, ReservationNode postParams) =>
        {
            if (postParams == null) return services.InvalidParameters();
            return await services.SaveReservationNodeAsync(postParams);
        });
    }
}
