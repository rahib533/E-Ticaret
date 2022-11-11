using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDBContext _context;

        public ReadRepository(ETicaretAPIDBContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public IQueryable<T> GetAllWithPagination(Pagination pagination, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query.Skip(pagination.Page * pagination.Size).Take(pagination.Size);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            Guid guidId = Guid.Parse(id);
            return await query.FirstOrDefaultAsync(x => x.Id == guidId);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> exception, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(exception);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> exception, bool tracking = true)
        {
            var query = Table.Where(exception);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
    }
}
