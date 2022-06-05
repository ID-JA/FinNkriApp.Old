using FinNkriApp.API.Common;

namespace FinNkriApp.API.Entities
{
    public class Favourite : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
