using KollusSampleMVC.Data;
using KollusSampleMVC.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Entity.Infrastructure;

namespace KollusSampleMVC.Controllers.callback
{
    [ApiController]
    public class ChannelCallbackController : ControllerBase
    {
        private readonly KollusSampleMVCContext _context;

        public ChannelCallbackController(KollusSampleMVCContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("callback/channel/add-content")]
        public async Task<IActionResult> AddNewContentAsync([FromForm] CallbackChannelAdd callbackChannelAdd)
        {
            var content = _context.Content
                .FirstOrDefault(m => m.UploadFileKey == callbackChannelAdd.upload_file_key);

            if (content == null)
            {
                return NotFound();
            }

            content.MediaContentKey = callbackChannelAdd.media_content_key;
            content.ChannelKey = callbackChannelAdd.channel_key;
            _context.Update(content);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
