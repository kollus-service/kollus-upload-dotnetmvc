using Org.BouncyCastle.Utilities;
using System.ComponentModel.DataAnnotations;

namespace KollusSampleMVC.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? UploadFileKey { get; set; }
        public string? MediaContentKey { get; set; }
        public string? ChannelKey { get; set; }
        public int? UploadDate { get; set; }
        public int? TranscodingDate { get; set; }
        public int? ChannelAddedDate { get; set; }
    }
}
