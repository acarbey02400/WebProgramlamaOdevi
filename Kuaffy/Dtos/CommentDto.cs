using Kuaffy.DataAccess.Entity;

namespace Kuaffy.Dtos
{
    public class CommentDto:IDto
    {
       
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AppointmentName { get; set; }
        public DateTime dateTime { get; set; }
    }
}
