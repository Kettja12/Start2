using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start2.Shared.Model.Reservation
{
    public class ReservationNode
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }

    }
}
