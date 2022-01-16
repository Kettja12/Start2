namespace Start2.Shared.Model.Account;
public class Claim
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ClaimType { get; set; } = null!;
    public string ClaimValue { get; set; } = null!;
}
