using AutoMapper;
using DiskontPica.Models;
using DiskontPica.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace DiskontPica.Controllers
{
	[Route("api/drinkStore")]
	[ApiController]
	public class DrinkStoreController : ControllerBase
	{

		private readonly IDrinkStoreRepository _drinkStoreRepository;
		private readonly IMapper _mapper;

		public DrinkStoreController(IDrinkStoreRepository drinkStoreRepository, IMapper mapper)
		{
			_drinkStoreRepository = drinkStoreRepository ?? throw new ArgumentNullException(nameof(drinkStoreRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}


		//Product

		[HttpGet("Product")]
		public ActionResult<IEnumerable<Product>> GetAllProducts()
		{
			var products = _drinkStoreRepository.GetAllProducts();
			return Ok(products);
		}

		[HttpGet("Product/{id}")]
		public ActionResult<Product> GetProductById(int id)
		{
			var obj = _drinkStoreRepository.GetProductById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("Product/{id}")]
		public ActionResult AddProduct([FromBody] Product product)
		{
			_drinkStoreRepository.AddProduct(product);

			return Ok();
		}

		[HttpPut("Product/{id}")]
		public ActionResult UpdateProduct(int id, [FromBody] Product product)
		{
			var existingProduct = _drinkStoreRepository.GetProductById(id);

			if (existingProduct == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.UpdateProduct(existingProduct);

			return Ok();
		}

		[HttpDelete("Product/{id}")]
		public ActionResult DeleteProduct(int id)
		{
			var existingProduct = _drinkStoreRepository.GetProductById(id);

			if (existingProduct == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteProduct(id);

			return NoContent();
		}

		// Country




	}
}
