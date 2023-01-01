using Kuaffy.DataAccess.Abstract;
using Kuaffy.DataAccess.EntityFramework.Concrete;
using Kuaffy.Models;

namespace Kuaffy.DataAccess.Concrete
{
    public class CompanyTypeDal:EfEntityRepositoryBase<CompanyType,KuaffyDataContext>,ICompanyTypeDal
    {
    }
}
