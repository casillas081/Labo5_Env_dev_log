using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Labo5WeatherApp.Model
{
    class WeatherService
    {
        public async Task<IEnumerable<WeatherForecast>> GetForecast()
        {
            var wc = new HttpClient();
            var weather = await wc.GetStringAsync(new Uri("http://api.openweathermap.org/data/2.5/forecast/forecast?q=Namur&lang=fr&appid=XXXXXXXXXXXXXXXXXXXXXX"));
            var rawWeather = JObject.Parse(weather);
            var forecast = rawWeather["list"].Children().Select(d => new WeatherForecast()
                {
                    Date = d["dt_txt"].Value<DateTime>(),
                    MinTemp = d["main"]["temp_min"].Value<double>(),
                    MaxTemp = d["main"]["temp_max"].Value<double>(),
                    WeatherDescription = d["weather"].First["description"].Value<string>(),
                    WindSpeed = d["wind"]["speed"].Value<double>()
                });
            return forecast;
        }
    }
}
