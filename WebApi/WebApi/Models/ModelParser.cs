using System.Text.Json;

namespace WebApi.Models
{
    public static class ModelParser
    {
        public static IFigureModel GetModel(JsonElement data)
        {
            if (data.TryGetProperty("circle", out var circle))
            {
                return circle.ToObject<CircleModel>();
            }

            if (data.TryGetProperty("triangle", out var triangle))
            {
                return triangle.ToObject<TriangleModel>();
            }

            return null;
        }

        private static T ToObject<T>(this JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
