using FinNkriApp.API.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinNkriApp.API.Entities
{
    public class Post : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public int TotalFloors { get; set; } = 0;
        public int TotalBathrooms { get; set; } = 0;
        public int TotalLivingrooms { get; set; } = 0;
        public int TotalKitchens { get; set; } = 0;
        public int TotalBedrooms { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
