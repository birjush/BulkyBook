using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CompanyRepo : Repository<CompanyTable>, ICompanyRepo
    {

        private readonly ApplicationDbContext _db;
        public CompanyRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CompanyTable obj)
        {
            throw new NotImplementedException();
        }
    }
}
