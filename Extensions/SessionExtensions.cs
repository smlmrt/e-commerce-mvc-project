using System.Text.Json;

namespace ECommerceWeb.Extensions
{
    public static class SessionExtensions
    {
        // Nesneyi JSON string'e çevirip Session'a kaydeder
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Session'daki JSON string'i okuyup tekrar nesneye (C# sınıfına) çevirir
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}