using Microsoft.EntityFrameworkCore;
using NUlid;
using Start2.Shared.Model.Reservation;

namespace Start2.DBContext
{
    public partial class StartContext
    {
        public async Task<List<ReservationNode>> GetReservationNodesAsync()
        {
            List<ReservationNode>? reservationNodes = await ReservationNodes.ToListAsync();
            return reservationNodes;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            List<Reservation>? reservations = await Reservations.ToListAsync();
            return reservations;
        }

        public async Task<ReservationNode> SaveReservationNodeAsync(ReservationNode node)
        {

            if (node != null)
            {
                Entry(node).State = EntityState.Detached;
                if (node.Id == "")
                {
                    node.Id = Ulid.NewUlid().ToString();
                    Entry(node).State = EntityState.Added; 
                }
                else
                {
                    Entry(node).State = EntityState.Modified;
                }
                await SaveChangesAsync();
            }
            return node;
        }

        public async Task<Reservation> SaveReservationAsync(Reservation node)
        {

            if (node != null)
            {
                if (node.Id == "")
                {
                    node.Id = Ulid.NewUlid().ToString();
                    Entry(node).State = EntityState.Added;
                }
                else
                {
                    Entry(node).State = EntityState.Modified;
                }
                await SaveChangesAsync();
            }
            return node;
        }

    }
}