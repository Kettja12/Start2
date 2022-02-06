using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start2.Shared.Model.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ReservationNodeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? ReservationStartDate { get; set; }
        public DateTime? ReservationEndDate { get; set; }
        public string? ReservationTag { get; set; }
    }

}
