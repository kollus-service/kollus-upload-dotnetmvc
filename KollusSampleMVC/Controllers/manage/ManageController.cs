using KollusSampleMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace KollusSampleMVC.Controllers.manage
{
    public class ManageController : Controller
    {
        private string AccessToken = "";    // YOUR ACCESS_TOEKN
        private readonly ILogger<HomeController> _logger;

        public ManageController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(ManageFormModel formData)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["name"] = formData.Name;

            using (var httpClient = new HttpClient())
            {
                string url = "https://c-api-kr.kollus.com/api/categories?access_token=" + AccessToken;
                StringContent content = new StringContent(JsonConvert.SerializeObject(dic), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return Ok(apiResponse);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddChannel(ManageFormModel formData)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["name"] = formData.Name;

            using (var httpClient = new HttpClient())
            {
                string url = "https://c-api-kr.kollus.com/api/channels?access_token=" + AccessToken;
                StringContent content = new StringContent(JsonConvert.SerializeObject(dic), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return Ok(apiResponse);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AttachChannelToCategory(ManageFormModel formData)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            using (var httpClient = new HttpClient())
            {
                string url = "https://c-api-kr.kollus.com/api/categories/" + formData.CategoryKey + "/channels/" + formData.ChannelKey + "/attach?access_token=" + AccessToken;
                StringContent content = new StringContent(JsonConvert.SerializeObject(dic), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return Ok(apiResponse);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CallbackChannelAdded(ManageFormModel formData)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["use_pingback"] = 1;
            dic["pingback_url"] = formData.CallbackUrl;

            using (var httpClient = new HttpClient())
            {
                string url = "https://c-api-kr.kollus.com/api/channels/" + formData.ChannelKey + "/callback?access_token=7kpam3opxtk84m3r";
                StringContent content = new StringContent(JsonConvert.SerializeObject(dic), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return Ok(apiResponse);
                }
            }
        }
    }
}
