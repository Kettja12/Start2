namespace Start2.Server.Endponts;
using System.Reflection;
public static partial class Endpoints
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        MapUserApi(endpoints);
        //SystemApi.Map(endpoints);
        MapDashboardApi(endpoints);
        MapReservationApi(endpoints);
    }
    public static bool CheckScript(string uncheckedString)
    {
        if (uncheckedString.Contains('<')) return false;
        return true;
    }
    public static bool CheckObject(object uncheckedOnject)
    {
        if (uncheckedOnject == null) return false;  
        Type type = uncheckedOnject.GetType();
        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
        PropertyInfo[] properties = type.GetProperties(flags);
        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType.Name == "String")
            {
                object value = property.GetValue(uncheckedOnject, null);
                if (value!= null)
                {
                    var s = value.ToString();
                    if (s.IndexOf('<') > -1
                      || s.IndexOf(';') > -1)
                        return false;
                }
            }
        }
        return true;
    }

}
