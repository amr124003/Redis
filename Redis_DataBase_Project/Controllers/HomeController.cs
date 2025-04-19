using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis_DataBase_Project.Models;

namespace Redis_DataBase_Project.Controllers
{
    public class HomeController : Controller
    {
        private IDistributedCache cache;

        public HomeController ( IDistributedCache Cache )
        {
            cache = Cache;
        }
        public async Task<IActionResult> SaveRedisCache ()
        {
            var dashboard = new Dashboard
            {
                TotalCustomerCount = 20000,
                TotalRevenue = 2000,
                TopSellingCountryName = "Egypt",
                TopSellingProductName = "Mobile"
            };

            var CacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(DateTime.Now.AddDays(1).Subtract(DateTime.Now).TotalSeconds),
                SlidingExpiration = null
            };

            var jsonData = JsonConvert.SerializeObject(dashboard);
            await cache.SetStringAsync("Key1", jsonData, CacheOptions);
            return View();
        }
        public async Task<IActionResult> GetData ()
        {
            var jsonData = await cache.GetStringAsync("Key1");
            if (!string.IsNullOrEmpty(jsonData))
            {
                var dashboardData = JsonConvert.DeserializeObject<Dashboard>(jsonData!);

                return View(dashboardData);
            }
            return View();
        }
    }
}
