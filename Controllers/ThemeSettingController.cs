using Microsoft.AspNetCore.Mvc;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeSettingController : Controller
    {
        [HttpPost("SetTheme")]
        public async Task<IActionResult> SetTheme([FromBody] ThemeSettings setting)
        {
            HttpContext.Session.SetString("Theme", setting.Theme);
            return Ok();
        }

        public class ThemeSettings
        {
            public string Theme { get; set; } = "Dark";
        }
    }
}
