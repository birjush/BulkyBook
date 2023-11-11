using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.DataAccess.Data;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepo category { get; private set; }
        public IProductRepo product { get; private set; }
        public ICompanyRepo company { get; private set; }
        public IShoppingCartRepo ShoppingCart { get; private set; }
        public IApplicationUserRepo applicationUser { get; private set; }
        public IOrderHeaderRepo orderHeader { get; private set; }
        public IOrderDetailRepo orderdetail { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            applicationUser = new ApplicationUserRepo(_db);
            ShoppingCart = new ShoppingCartRepo(_db);
            category = new CategoryRepo(_db);
            product = new ProductRepo(_db);
            company = new CompanyRepo(_db);
            orderHeader = new OrderHeaderRepo(_db);
            orderdetail = new OrderDetailRepo(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
