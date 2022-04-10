namespace Start2.Shared.Model.Reservation;
public class Reservation
{
    public string Id { get; set; } = null!;
    public string ReservationNodeId { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
    public DateTime? ReservationStartDate { get; set; }
    public DateTime? ReservationEndDate { get; set; }
    public string? ReservationTag { get; set; }
    public DateTime Modified { get; set; }

}

