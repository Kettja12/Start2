using Microsoft.Extensions.Localization;
using Start2.Client.Localize;

namespace Start2.Client;
public static class ClientExtensions
{

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

}
