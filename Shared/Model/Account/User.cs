namespace Start2.Shared.Model.Account;
public class User
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public DateTime Modified { get; set; }

    public virtual List<Claim>? Claims { get; set; }

}
