using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start2.Shared.Model
{
    public class ReservationFilter : Reservation.Reservation
    {
        public DateTime? ReservationDateStart { get; set; }
        public DateTime? ReservationDateEnd { get; set; }
    }

}
