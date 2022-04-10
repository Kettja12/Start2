namespace Start2.Shared.Model.Account;
public class LoginToken
{
    public string Id { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime Modified { get; set; }

    public virtual User? User { get; set; }

}

