using NUlid;
using Start2.Shared.Model.Account;

namespace Start2.Shared;
public static class Extensions
    {
        public static string? GetClaim(this List<Claim> claims, string key)
        {
            return claims.Where(x => x.ClaimType == key).Select(x => x.ClaimValue).FirstOrDefault();
        }
        public static Claim SetClaim(this List<Claim> claims, string key, string value)
        {
            foreach (var item in claims)
            {
                if (item.ClaimType == key)
                {
                    item.ClaimValue = value;
                    return item;
                }
            }
            return new Claim()
            {
                UserId = claims.First().UserId,
                ClaimType = key,
                ClaimValue = value
            };
        }
        public static bool IsAdmin(this List<Claim> claims)
        {
            var claim = claims
                .FirstOrDefault(x =>
                x.ClaimType == "UserGroups"
                && x.ClaimValue.Contains("Admin"));
            if (claim == null) return false;
            return true;
        }
        public static void SetAdmin(this List<Claim> claims,string userId)
        {
        if (claims == null)
        {
            claims=new List<Claim>();
            claims.Add(new Claim() { 
                Id = Ulid.NewUlid().ToString(),
                UserId = userId, 
                ClaimType = "UserGroups", 
                ClaimValue = "Admin" });
            return;
        }
        var claim = claims
            .FirstOrDefault(x =>
            x.ClaimType == "UserGroups");
        if (claim == null)
        {
            claims.Add(new Claim()
            {
                Id = Ulid.NewUlid().ToString(),
                UserId = userId,
                ClaimType = "UserGroups",
                ClaimValue = "Admin"
            });
            return;
        }
        if (claim.ClaimValue.Contains("Admin")) return;
        claim.ClaimValue+=";Admin";
    }
    public static void RemoveAdmin(this List<Claim> claims)
    {
        if (claims == null) return;
        var claim = claims
            .FirstOrDefault(x =>
            x.ClaimType == "UserGroups");
        if (claim == null) return;
        if (claim.ClaimValue.Contains(";Admin"))
        {
            claim.ClaimValue = claim.ClaimValue.Replace(";Admin", "");
        }
        if (claim.ClaimValue.Contains("Admin"))
        {
            claim.ClaimValue=claim.ClaimValue.Replace("Admin", "");
        }
    }


}

