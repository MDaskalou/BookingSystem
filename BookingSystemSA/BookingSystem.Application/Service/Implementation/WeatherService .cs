//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace BookingSystem.Application.Services
//{
//    public interface IWeatherService
//    {
//        Task<WeatherData> GetWeatherAsync(string city);
//    }

//    public class WeatherService : IWeatherService
//    {
//        private readonly HttpClient _httpClient;

//        public WeatherService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<WeatherData> GetWeatherAsync(string city)
//        {
//            // Exempel-URL; byt ut med ett riktigt API-anrop och API-nyckel om det behövs
//            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=YOUR_API_KEY&units=metric";
//            var weatherData = await _httpClient.GetFromJsonAsync<WeatherData>(url);
//            return weatherData;
//        }
//    }

//    // Exempel på modell för väderdata
//    public class WeatherData
//    {
//        public string Name { get; set; }  // Stadens namn
//        public MainWeather Main { get; set; }
//        public WeatherDescription[] Weather { get; set; }
//    }

//    public class MainWeather
//    {
//        public double Temp { get; set; }
//        public double Humidity { get; set; }
//    }

//    public class WeatherDescription
//    {
//        public string Main { get; set; }
//        public string Description { get; set; }
//    }
//}
