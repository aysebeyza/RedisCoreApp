using Microsoft.AspNetCore.Mvc;
using RedisCoreApp.Services;

namespace RedisCoreApp.Controllers
{
    public class RedisCacheController : Controller
    {
        private readonly RedisCacheService _redisCacheService;

        public RedisCacheController(RedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        // Sayfayı açan metod
        public IActionResult Anasayfa()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetCache(string key, string value)
        {
            await _redisCacheService.SetAsync(key, value, TimeSpan.FromMinutes(5));
            ViewBag.Message = $"'{key}' anahtarı ile veri kaydedildi!";
            return View("Anasayfa");
        }

        // Redis'ten veri okuma
        [HttpGet]
        public async Task<IActionResult> GetCache(string key)
        {
            var value = await _redisCacheService.GetAsync(key);
            ViewBag.Message = value != null ? $"Bulunan Değer: {value}" : "Veri bulunamadı!";
            return View("Anasayfa");
        }

        // Redis'ten veri silme
        [HttpPost]
        public async Task<IActionResult> RemoveCache(string key)
        {
            await _redisCacheService.RemoveAsync(key);
            ViewBag.Message = $"'{key}' anahtarı ile Redis'teki veri silindi.";
            return View("Anasayfa");
        }
    }
}
