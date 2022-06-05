using FinNkriApp.API.Entities;
using FinNkriApp.API.Mapping;

namespace FinNkriApp.API.Features.Posts.Queries
{
    public class PostDto : IMapFrom<Post>
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public double? Price { get; set; }
        public int TotalFloors { get; set; }
        public int TotalBathrooms { get; set; }
        public int TotalLivingrooms { get; set; }
        public int TotalKitchens { get; set; }
        public int TotalBedrooms { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
