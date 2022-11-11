using ETicaretAPI.Application;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
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

        [HttpGet("Pagination")]
        public IActionResult GetAllProducts([FromQuery]Pagination pagination)
        {
            return Ok(_productReadRepository.GetAllWithPagination(pagination));
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

        // [HttpPost("update")]
        // public async Task<IActionResult> Update(Product product)
        // {
        //     _productWriteRepository.Update(product);
        //     return Ok(await _productWriteRepository.SaveAsync());
        // }

        [HttpPost]
        public async Task<IActionResult> Create(VM_Create_Product product){
            if (ModelState.IsValid)
            {
                System.Console.WriteLine("Success");
                Product p = new Product{
                    Name = product.Name,
                    Stock = product.Stock,
                    Price = product.Price
                };
                await _productWriteRepository.AddAsync(p);
                return Ok(await _productWriteRepository.SaveAsync());
            }
            else{
                System.Console.WriteLine("Error");
                return BadRequest("Error");
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product){
            _productWriteRepository.Update(product);
            return Ok(await _productWriteRepository.SaveAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id){
            await _productWriteRepository.Remove(id);
            return Ok(await _productWriteRepository.SaveAsync());
        }
    }
}
