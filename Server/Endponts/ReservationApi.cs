
using Start2.Server.Services;
using Start2.Shared;
using Start2.Shared.Model.Reservation;

namespace Start2.Server.Endponts;
public static partial class Endpoints
{
    public static void MapReservationApi(IEndpointRouteBuilder endpoints)
    {

        _ = endpoints.MapGet(APIServices.ReservationGetReservationNodes,
            async (ReservationServices services) =>
            {
                return await services.GetReservationNodesAsync();
            });

        _ = endpoints.MapPost(APIServices.ReservationSaveReservationNode,
            async (ReservationServices services, ReservationNode postParams) =>
            {
                if (CheckObject(postParams) == false) return services.UnacceptedContent();
                return await services.SaveReservationNodeAsync(postParams);
            });

        _ = endpoints.MapGet(APIServices.ReservationGetReservations,
            async (ReservationServices services) =>
            {
                return await services.GetReservationAsync();
            });

        _ = endpoints.MapPost(APIServices.ReservationSaveReservation,
            async (ReservationServices services, Reservation postParams) =>
            {
                if (CheckObject(postParams) == false) return services.UnacceptedContent();
                return await services.SaveReservationAsync(postParams);
            });


    }
}
