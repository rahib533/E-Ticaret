using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts() => new()
        {
            new(){Id = Guid.NewGuid(), Name = "Phone", Price = 2000, Stock = 20, CreatedDate = DateTime.Now},
            new(){Id = Guid.NewGuid(), Name = "Phone2", Price = 1000, Stock = 520, CreatedDate = DateTime.Now},
            new(){Id = Guid.NewGuid(), Name = "Phone3", Price = 700, Stock = 820, CreatedDate = DateTime.Now}
        };  
    }
}
