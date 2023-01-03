using Kuaffy.DataAccess.Abstract;
using Kuaffy.DataAccess.EntityFramework.Concrete;
using Kuaffy.Dtos;
using Kuaffy.Models;
using Microsoft.AspNetCore.Identity;

namespace Kuaffy.DataAccess.Concrete
{
    public class AppointmentDal : EfEntityRepositoryBase<Appointment, KuaffyDataContext>, IAppointmentDal
    {
        public List<AppointmentDto> GetAppointmentDetails()
        {
           
            using (KuaffyDataContext context = new KuaffyDataContext() )
            {
                var result = from ap in context.Appointments
                             join co in context.Companies
                             on ap.CompanyId equals co.Id
                             select new AppointmentDto { CompanyName = co.Name, AppointmentId = ap.Id, dateTime = ap.dateTime };
                return result.ToList();
            }
        }
        public List<AppointmentDto> GetAppointmentDetails(string UserId)
        {

            using (KuaffyDataContext context = new KuaffyDataContext())
            {
                var result = from ap in context.Appointments
                             join co in context.Companies
                             on ap.CompanyId equals co.Id
                             where ap.UserId== UserId
                             select new AppointmentDto { CompanyName = co.Name, AppointmentId = ap.Id, dateTime = ap.dateTime };
                return result.ToList();
            }
        }
    }
}
