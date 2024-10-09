using SearchPic_V2.Services;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public class ModerationResult
    {
        public bool IsApproved { get; set; }
        public string Message { get; set; }
    }
}
