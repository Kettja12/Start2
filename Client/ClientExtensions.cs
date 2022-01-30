using Microsoft.Extensions.Localization;
using Start2.Client.Localize;
using Start2.Shared.Model.Account;
using System.Text;

namespace Start2.Client;
public static class ClientExtensions
{
    public static List<int> ParseToIntList(this string s)
    {
        var result = new List<int>();
        int index = 0;
        int value = 0;
        while (index < s.Length)
        {
            switch (s[index])
            {
                case ',': result.Add(value); value = 0; break;
                case '0': value *= 10; break;
                case '1': value = value * 10 + 1; break;
                case '2': value = value * 10 + 2; break;
                case '3': value = value * 10 + 3; break;
                case '4': value = value * 10 + 4; break;
                case '5': value = value * 10 + 5; break;
                case '6': value = value * 10 + 6; break;
                case '7': value = value * 10 + 7; break;
                case '8': value = value * 10 + 8; break;
                case '9': value = value * 10 + 9; break;
            }
            index++;
        }
        if (value > 0) result.Add(value);
        return result;
    }

    public static string Translate(this IStringLocalizer<Resource> r, string s)
    {
        var s2 = s.Split('.');
        var result = string.Empty;
        foreach (var m in s2)
        {
            if (string.IsNullOrEmpty(m) == false)
            {
                var r2 = m.Trim() + ".";
                result += r[r2].Value + " ";
            }
        }
        return result;
    }
    public static List<int> DashboardItemsOrder(this User user)
    {

        if (user.Claims != null)
        {

            foreach (var item in user.Claims)
            {
                if (item.ClaimType == "DashboardItems")
                    return item.ClaimValue.ParseToIntList();
            }
        }
        return new List<int>();
    }
    public static void DashboardItemsOrder(this User user, List<int> value)
    {
        var s = new StringBuilder();
        for (var i = 0; i < value.Count; i++)
        {
            s.Append(value[i]);
            if (i < value.Count - 1)
                s.Append(',');
        }

        foreach (var item in user.Claims)
        {
            if (item.ClaimType == "DashboardItems")
            {
                item.ClaimValue = s.ToString();
                return;
            }
        }
        user.Claims.Add(new Claim()
        {
            ClaimType = "DashboardItems",
            ClaimValue = s.ToString(),
            UserId = user.Id
        });
    }

}
