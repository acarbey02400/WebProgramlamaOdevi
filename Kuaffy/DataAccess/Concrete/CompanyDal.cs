using Kuaffy.DataAccess.Abstract;
using Kuaffy.DataAccess.EntityFramework.Concrete;
using Kuaffy.Dtos;
using Kuaffy.Models;

namespace Kuaffy.DataAccess.Concrete
{
    public class CompanyDal : EfEntityRepositoryBase<Company, KuaffyDataContext>, ICompanyDal
    {
        public List<CompanyDto> GetCompaniesDetail()
        {
            using (KuaffyDataContext context = new KuaffyDataContext())
            {
                var result = from co in context.Companies
                             join ct in context.CompanyTypes
                             on co.CompanyType equals ct.Id
                             select new CompanyDto
                             {
                                 Address = co.Address,
                                 City = co.City,
                                 CompanyTypeName = ct.Name,
                                 CompanyId = co.Id
                             ,
                                 Name = co.Name,
                                 Description = co.Description,
                                 Point = co.Point,
                                 Status = co.Status
                                 
                             };
                return result.ToList();
            }
        }
    }
}
