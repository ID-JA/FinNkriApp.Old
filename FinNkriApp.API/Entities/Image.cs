using FinNkriApp.API.Common;

namespace FinNkriApp.API.Entities
{
    public class Image : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Url { get; set; }
    }
}