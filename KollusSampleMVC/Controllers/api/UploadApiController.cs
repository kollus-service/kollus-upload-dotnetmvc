
using KollusSampleMVC.Data;
using KollusSampleMVC.Models;
using KollusSampleMVC.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace KollusSampleMVC.Controllers.api
{
    [ApiController]
    public class UploadApiController : ControllerBase
    {
        private readonly KollusSampleMVCContext _context;

        public UploadApiController(KollusSampleMVCContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public ActionResult<string> GetFirst()
        {
            return Ok("Hello, world!");
        }

        [HttpPost]
        [Route("api/add-content")]
        public async Task<IActionResult> AddNewContentAsync(Content content)
        {
            if (ModelState.IsValid)
            {
                _context.Add(content);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/get-medialink")]
        public ActionResult<string> GetMediaLink(string? mediaContentKey)
        {
            string destination = KollusPlayer.getSrUrlWithJWT(mediaContentKey);
            string redirectUrl = GetRedirectUrl(destination);
            var result = new GetMediaLinkModel() { url = redirectUrl };
            string json = JsonConvert.SerializeObject(result);
            return Ok(json);
        }

        public class GetMediaLinkModel
        {
            public string url { get; set; }
        }

        public static string GetRedirectUrl(string originUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(originUrl);
                request.AllowAutoRedirect = false; // Disable automatic redirection

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.Redirect ||
                        response.StatusCode == HttpStatusCode.Moved ||
                        response.StatusCode == HttpStatusCode.RedirectMethod)
                    {
                        string redirectUrl = response.Headers["Location"];
                        return redirectUrl;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    if (errorResponse.StatusCode == HttpStatusCode.Redirect ||
                        errorResponse.StatusCode == HttpStatusCode.Moved ||
                        errorResponse.StatusCode == HttpStatusCode.RedirectMethod)
                    {
                        string redirectUrl = errorResponse.Headers["Location"];
                        return redirectUrl;
                    }
                }

                Console.WriteLine("Error: " + ex.Message);
                // Handle exceptions
            }

            return null;
        }
    }
}
