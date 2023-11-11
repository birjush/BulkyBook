using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        ICategoryRepo category { get; }
        IProductRepo product { get; }
        ICompanyRepo company { get; }
        IShoppingCartRepo ShoppingCart { get; }
        IApplicationUserRepo applicationUser { get; }
        IOrderHeaderRepo orderHeader { get; }
        IOrderDetailRepo orderdetail { get; }
        void Save();
    }
}
