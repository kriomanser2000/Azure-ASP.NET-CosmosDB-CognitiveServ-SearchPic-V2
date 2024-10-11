using Azure;
using System.Collections.Generic;

namespace SearchPic_V2.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public string BlobUrl { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
