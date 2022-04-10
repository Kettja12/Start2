namespace Start2.Shared.Model.Reservation;
public class ReservationNode
{
    public string Id { get; set; } = null!;
    public string? ParentId { get; set; }
    public string? Name { get; set; }
    public DateTime Modified { get; set; }


}
