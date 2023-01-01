using Kuaffy.DataAccess.EntityFramework.Abstract;
using Kuaffy.Dtos;
using Kuaffy.Models;

namespace Kuaffy.DataAccess.Abstract
{
    public interface ICompanyDal:IEntityRepository<Company>
    {

        List<CompanyDto> GetCompaniesDetail();
    }
}
