using Kuaffy.DataAccess.EntityFramework.Abstract;
using Kuaffy.Dtos;
using Kuaffy.Models;

namespace Kuaffy.DataAccess.Abstract
{
    public interface IAppointmentDal:IEntityRepository<Appointment>
    {
        List<AppointmentDto> GetAppointmentDetails();
         List<AppointmentDto> GetAppointmentDetails(string UserId);
    }
}
