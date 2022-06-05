using FinNkriApp.API.Common;

namespace FinNkriApp.API.Entities
{
    public class Rating : BaseAuditableEntity
    {
        public double NbrStars { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
