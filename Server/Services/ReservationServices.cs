using Start2.Server.DBContext;
using Start2.Shared.Model.Reservation;

namespace Start2.Server.Services
{
    public class ReservationServices : ApiServices
    {
        public ReservationServices(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ApiServices> logger,
            StartContext db) : base(configuration, httpContextAccessor, logger, db)
        {
        }

        public async Task<IResult> GetReservationNodesAsync()
        {
            try
            {
                List<ReservationNode>? items = await db.GetReservationNodesAsync();
                return Results.Ok(items);

            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Reservationnodes search failed.");
        }

        public async Task<IResult> GetReservationAsync()
        {
            try
            {
                List<Reservation>? items = await db.GetReservationsAsync();
                return Results.Ok(items);

            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Reservations search failed.");
        }
        internal async Task<IResult> SaveReservationNodeAsync(ReservationNode postParams)
        {
            try
            {
                ReservationNode item = await db.SaveReservationNodeAsync(postParams);
                return Results.Ok(item);

            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Reservationnode save failed.");
        }
        internal async Task<IResult> SaveReservationAsync(Reservation postParams)
        {
            try
            {
                Reservation item = await db.SaveReservationAsync(postParams);
                return Results.Ok(item);

            }
            catch (Exception e)
            {
                logger.LogError("Error: ", e);
            }
            return Results.Conflict("Reservation save failed.");
        }
    }
}
