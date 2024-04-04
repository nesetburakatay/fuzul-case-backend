using Core.Entities;
using Core.Repositories;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EstateRepository : GenericRepository<Estate>, IEstateRepository
    {
        public EstateRepository(AppDbContext context) : base(context){}
    }
}
