using Kuaffy.DataAccess.Entity;

namespace Kuaffy.Dtos
{
    public class AppointmentDto:IDto
    {

        public int AppointmentId { get; set; }
        public string? CompanyName { get; set; }
        public DateTime dateTime { get; set; }
    }
}
