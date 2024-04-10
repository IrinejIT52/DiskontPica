﻿using AutoMapper;
using DiskontPica.DTO;
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

		[HttpGet("product")]
		public ActionResult<IEnumerable<Product>> GetAllProducts()
		{
			var products = _drinkStoreRepository.GetAllProducts();
			return Ok(products);
		}

		[HttpGet("product/{id}")]
		public ActionResult<Product> GetProductById(int id)
		{
			var obj = _drinkStoreRepository.GetProductById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("product")]
		public ActionResult AddProduct([FromBody] Product product)
		{
			_drinkStoreRepository.AddProduct(product);

			return Ok();
		}

		[HttpPut("product/{id}")]
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

		[HttpDelete("product/{id}")]
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

		[HttpGet("product/by-county/{id}")]
		public ActionResult<IEnumerable<Product>> GetProductByCountry(int id)
		{
			var products = _drinkStoreRepository.GetProductsByCountry(id);

			if (products == null || !products.Any())
			{
				return NotFound();
			}

			return Ok(products);
		}

		[HttpGet("product/by-category/{id}")]
		public ActionResult<IEnumerable<Category>> GetProductByCategory(int id)
		{
			var products = _drinkStoreRepository.GetProductsByCategory(id);

			if (products == null || !products.Any())
			{
				return NotFound();
			}

			return Ok(products);
		}

		[HttpGet("product/by-admin/{id}")]
		public ActionResult<IEnumerable<Category>> GetProductByAdmin(int id)
		{
			var products = _drinkStoreRepository.GetProductsByAdmin(id);

			if (products == null || !products.Any())
			{
				return NotFound();
			}

			return Ok(products);
		}


		// Country

		[HttpGet("country")]
		public ActionResult<IEnumerable<Country>> GetAllCountries()
		{
			var obj = _drinkStoreRepository.GetAllCountries();
			return Ok(obj);
		}

		[HttpGet("country/{id}")]
		public ActionResult<Country> GetCountryById(int id)
		{
			var obj = _drinkStoreRepository.GetCountryById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("country")]
		public ActionResult AddCountry([FromBody] Country country)
		{
			_drinkStoreRepository.AddCountry(country);

			return Ok();
		}

		[HttpPut("country/{id}")]
		public ActionResult UpdateCountry(int id, [FromBody] Country country)
		{
			var existingCountry = _drinkStoreRepository.GetCountryById(id);

			if (existingCountry == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.UpdateCountry(existingCountry);

			return Ok();
		}

		[HttpDelete("country/{id}")]
		public ActionResult DeleteCountry(int id)
		{
			var existingCountry = _drinkStoreRepository.GetCountryById(id);

			if (existingCountry == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteCountry(id);

			return NoContent();
		}

		// Category

		[HttpGet("category")]
		public ActionResult<IEnumerable<Category>> GetAllCategories()
		{
			var obj = _drinkStoreRepository.GetAllCategories();
			return Ok(obj);
		}

		[HttpGet("category/{id}")]
		public ActionResult<Category> GetCategoryById(int id)
		{
			var obj = _drinkStoreRepository.GetCategoryById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("category")]
		public ActionResult AddCategory([FromBody] Category category)
		{
			_drinkStoreRepository.AddCategory(category);

			return Ok();
		}

		[HttpPut("category/{id}")]
		public ActionResult UpdateCategory(int id, [FromBody] Category category)
		{
			var existingCategory = _drinkStoreRepository.GetCategoryById(id);

			if (existingCategory == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.UpdateCategory(existingCategory);

			return Ok();
		}

		[HttpDelete("category/{id}")]
		public ActionResult DeleteCategory(int id)
		{
			var existingCategory = _drinkStoreRepository.GetCategoryById(id);

			if (existingCategory == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteCategory(id);

			return NoContent();
		}

		// Order

		[HttpGet("order")]
		public ActionResult<IEnumerable<Order>> GetAllOrders()
		{
			var obj = _drinkStoreRepository.GetAllOrders();
			return Ok(obj);
		}

		[HttpGet("order/{id}")]
		public ActionResult<Order> GetOrderById(int id)
		{
			var obj = _drinkStoreRepository.GetOrderById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("order")]
		public ActionResult AddOrder([FromBody] Order order)
		{
			_drinkStoreRepository.AddOrder(order);

			return Ok();
		}

		[HttpPut("order/{id}")]
		public ActionResult UpdateOrder(int id, [FromBody] Order order)
		{
			var existingOrder = _drinkStoreRepository.GetOrderById(id);

			if (existingOrder == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.UpdateOrder(existingOrder);

			return Ok();
		}

		[HttpDelete("order/{id}")]
		public ActionResult DeleteOrder(int id)
		{
			var existingOrder = _drinkStoreRepository.GetOrderById(id);

			if (existingOrder == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteOrder(id);

			return NoContent();
		}

		[HttpGet("order/by-customer/{id}")]
		public ActionResult<IEnumerable<Order>> GetOrderByCustomer(int id)
		{
			var orders = _drinkStoreRepository.GetOrdersByCustomer(id);

			if (orders == null || !orders.Any())
			{
				return NotFound();
			}

			return Ok(orders);
		}

		// OrderItem

		[HttpGet("orderItem")]
		public ActionResult<IEnumerable<OrderItem>> GetAllOrderItems()
		{
			var obj = _drinkStoreRepository.GetAllOrderItems();
			return Ok(obj);
		}

		[HttpGet("orderItem/{id}")]
		public ActionResult<OrderItem> GetOrderItemById(int id)
		{
			var obj = _drinkStoreRepository.GetOrderItemById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("orderItem")]
		public ActionResult AddOrderItem([FromBody] OrderItem orderItem)
		{
			_drinkStoreRepository.AddOrderItem(orderItem);

			return Ok();
		}

		[HttpPut("orderItem/{id}")]
		public ActionResult UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
		{
			var existingOrderItem = _drinkStoreRepository.GetOrderItemById(id);

			if (existingOrderItem == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.UpdateOrderItem(existingOrderItem);

			return Ok();
		}

		[HttpDelete("orderItem/{id}")]
		public ActionResult DeleteOrderItem(int id)
		{
			var existingOrderItem = _drinkStoreRepository.GetOrderById(id);

			if (existingOrderItem == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteOrderItem(id);

			return NoContent();
		}

		[HttpGet("orderItem/by-order/{id}")]
		public ActionResult<IEnumerable<OrderItem>> GetOrderItemByOrder(int id)
		{
			var orderItems = _drinkStoreRepository.GetOrderItemsByOrder(id);

			if (orderItems == null || !orderItems.Any())
			{
				return NotFound();
			}

			return Ok(orderItems);
		}

		// Administrator

		[HttpGet("administrator")]
		public ActionResult<IEnumerable<Administrator>> GetAllAdministrators()
		{
			var obj = _drinkStoreRepository.GetAllAdministrators();
			return Ok(obj);
		}

		[HttpGet("administrator/{id}")]
		public ActionResult<Administrator> GetAdminById(int id)
		{
			var obj = _drinkStoreRepository.GetAdministratorById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("administrator")]
		public ActionResult AddAdministrator([FromBody] AdministratorCreateDTO adminCreateDTO)
		{
			var obj = _mapper.Map<Administrator>(adminCreateDTO);
			_drinkStoreRepository.AddAdministrator(obj);

			return Ok();
		}

		[HttpPut("administrator/{id}")]
		public ActionResult UpdateAdministrator(int id, [FromBody] AdministratorUpdateDTO adminUpdateDTO)
		{
			var existingAdmin = _drinkStoreRepository.GetAdministratorById(id);
			if (existingAdmin == null)
			{
				return NotFound();
			}

			_mapper.Map(adminUpdateDTO, existingAdmin);
			_drinkStoreRepository.UpdateAdministrator(existingAdmin);

			return Ok();
		}

		[HttpDelete("administrator/{id}")]
		public ActionResult DeleteAdministrator(int id)
		{
			var existingAdmin = _drinkStoreRepository.GetAdministratorById(id);
			if (existingAdmin == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteAdministrator(id);

			return NoContent();
		}



		// Customer

		[HttpGet("customer")]
		public ActionResult<IEnumerable<Customer>> GetAllCustomers()
		{
			var obj = _drinkStoreRepository.GetAllCustomers();
			return Ok(obj);
		}

		[HttpGet("customer/{id}")]
		public ActionResult<Customer> GetCustomerById(int id)
		{
			var obj = _drinkStoreRepository.GetCustomerById(id);
			if (obj == null)
			{
				return NotFound();
			}

			return Ok(obj);
		}

		[HttpPost("customer")]
		public ActionResult AddCustomer([FromBody] CustomerCreateDTO customerCreateDTO)
		{
			var obj = _mapper.Map<Customer>(customerCreateDTO);
			_drinkStoreRepository.AddCustomers(obj);

			return Ok();
		}

		[HttpPut("customer/{id}")]
		public ActionResult UpdateCustomer(int id, [FromBody] CustomerUpdateDTO customerUpdateDTO)
		{
			var existingCustomer = _drinkStoreRepository.GetCustomerById(id);
			if (existingCustomer == null)
			{
				return NotFound();
			}

			_mapper.Map(customerUpdateDTO, existingCustomer);
			_drinkStoreRepository.UpdateCustomers(existingCustomer);

			return Ok();
		}

		[HttpDelete("customer/{id}")]
		public ActionResult DeleteCustomer(int id)
		{
			var existingCustomer = _drinkStoreRepository.GetCustomerById(id);
			if (existingCustomer == null)
			{
				return NotFound();
			}

			_drinkStoreRepository.DeleteCustomers(id);

			return NoContent();
		}



	}
}
