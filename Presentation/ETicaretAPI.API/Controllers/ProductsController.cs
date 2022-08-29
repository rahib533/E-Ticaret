using ETicaretAPI.Application;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_productReadRepository.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id, bool tracking = true)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, tracking));
        }

        [HttpPost("post")]
        public async Task<IActionResult> Test(Product product)
        {
            await _productWriteRepository.AddAsync(product);
            return Ok(await _productWriteRepository.SaveAsync());
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Product product)
        {
            _productWriteRepository.Update(product);
            return Ok(await _productWriteRepository.SaveAsync());
        }
    }
}
