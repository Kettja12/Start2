namespace Start2.Shared.Model.Account;
public class Claim
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ClaimType { get; set; } = null!;
    public string ClaimValue { get; set; } = null!;
    public DateTime Modified { get; set; }
}
