using FinNkriApp.API.Interfaces;

namespace FinNkriApp.API.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
