using AutoMapper;
using Azure.Messaging;
using DiskontPica.DTO;
using DiskontPica.Models;
using DiskontPica.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("product")]
		public ActionResult<IEnumerable<Product>> GetAllProducts()
		{
			var products = _drinkStoreRepository.GetAllProducts();
			if(products.IsNullOrEmpty())
			{
				return NotFound("No products");
			}
			return Ok(products);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
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

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPost("product")]
		public ActionResult AddProduct([FromBody] Product product)
		{
			_drinkStoreRepository.AddProduct(product);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("product/{id}")]
		public ActionResult UpdateProduct(int id, [FromBody] Product product)
		{
			var existingProduct = _drinkStoreRepository.GetProductById(id);

			if (existingProduct == null)
			{
				return NotFound("Product not found");
			}

			_drinkStoreRepository.UpdateProduct(existingProduct);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("product/{id}")]
		public ActionResult DeleteProduct(int id)
		{
			var existingProduct = _drinkStoreRepository.GetProductById(id);

			if (existingProduct == null)
			{
				return NotFound("Product not found");
			}

			_drinkStoreRepository.DeleteProduct(id);

			return Ok();
		}

		[Authorize(Policy = IdentityData.CustomerPolicy)]
		[HttpGet("product/by-county/{id}")]
		public ActionResult<IEnumerable<Product>> GetProductByCountry(int id)
		{
			var products = _drinkStoreRepository.GetProductsByCountry(id);

			if (products == null || !products.Any())
			{
				return NotFound("No products found for that country");
			}

			return Ok(products);
		}

		[Authorize(Policy = IdentityData.CustomerPolicy)]
		[HttpGet("product/by-category/{id}")]
		public ActionResult<IEnumerable<Category>> GetProductByCategory(int id)
		{
			var products = _drinkStoreRepository.GetProductsByCategory(id);

			if (products == null || !products.Any())
			{
				return NotFound("No products found for that category");
			}

			return Ok(products);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("product/by-admin/{id}")]
		public ActionResult<IEnumerable<Category>> GetProductByAdmin(int id)
		{
			var products = _drinkStoreRepository.GetProductsByAdmin(id);

			if (products == null || !products.Any())
			{
				return NotFound("Admin has no products");
			}

			return Ok(products);
		}

		[AllowAnonymous]
		[HttpGet("product/{search?}/{sortColumn?}/{sortOrder?}")]
		public ActionResult<IEnumerable<Product>> GetProductByQuery(string? search=" ",string? sortColumn=" ",string? sortOrder="asc")
		{
			var products = _drinkStoreRepository.GetProductsByQuery(search,sortColumn,sortOrder);

			if (products == null || !products.Any())
			{
				return NotFound("No products found");
			}

			return Ok(products);
		}


		// Country
		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("country")]
		public ActionResult<IEnumerable<Country>> GetAllCountries()
		{
			var obj = _drinkStoreRepository.GetAllCountries();
			if (obj.IsNullOrEmpty())
			{
				return NotFound("No countries");
			}
		
			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("country/{id}")]
		public ActionResult<Country> GetCountryById(int id)
		{
			var obj = _drinkStoreRepository.GetCountryById(id);
			if (obj == null)
			{
				return NotFound("No country found");
			}

			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPost("country")]
		public ActionResult AddCountry([FromBody] Country country)
		{
			_drinkStoreRepository.AddCountry(country);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("country/{id}")]
		public ActionResult UpdateCountry(int id, [FromBody] Country country)
		{
			var existingCountry = _drinkStoreRepository.GetCountryById(id);

			if (existingCountry == null)
			{
				return NotFound("No country found");
			}

			_drinkStoreRepository.UpdateCountry(existingCountry);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("country/{id}")]
		public ActionResult DeleteCountry(int id)
		{
			var existingCountry = _drinkStoreRepository.GetCountryById(id);

			if (existingCountry == null)
			{
				return NotFound("No country found");
			}

			_drinkStoreRepository.DeleteCountry(id);

			return Ok();
		}

		// Category

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("category")]
		public ActionResult<IEnumerable<Category>> GetAllCategories()
		{
			var obj = _drinkStoreRepository.GetAllCategories();
			if (obj.IsNullOrEmpty())
			{
				return NotFound("No categories found");
			}
			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("category/{id}")]
		public ActionResult<Category> GetCategoryById(int id)
		{
			var obj = _drinkStoreRepository.GetCategoryById(id);
			if (obj == null)
			{
				return NotFound("No category found");
			}

			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPost("category")]
		public ActionResult AddCategory([FromBody] Category category)
		{
			_drinkStoreRepository.AddCategory(category);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("category/{id}")]
		public ActionResult UpdateCategory(int id, [FromBody] Category category)
		{
			var existingCategory = _drinkStoreRepository.GetCategoryById(id);

			if (existingCategory == null)
			{
				return NotFound("No category found");
			}

			_drinkStoreRepository.UpdateCategory(existingCategory);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("category/{id}")]
		public ActionResult DeleteCategory(int id)
		{
			var existingCategory = _drinkStoreRepository.GetCategoryById(id);

			if (existingCategory == null)
			{
				return NotFound("No category found");
			}

			_drinkStoreRepository.DeleteCategory(id);

			return Ok();
		}

		// Order
		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("order")]
		public ActionResult<IEnumerable<Order>> GetAllOrders()
		{
			var obj = _drinkStoreRepository.GetAllOrders();
			if (obj.IsNullOrEmpty())
			{
				return NotFound("No orders found");
			}
			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("order/{id}")]
		public ActionResult<Order> GetOrderById(int id)
		{
			var obj = _drinkStoreRepository.GetOrderById(id);
			if (obj == null)
			{
				return NotFound("No order found");
			}

			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.CustomerPolicy)]
		[HttpPost("order")]
		public ActionResult AddOrder([FromBody] Order order)
		{

			_drinkStoreRepository.AddOrder(order);

			foreach (var item in order.orderItems)
			{
				Product product = _drinkStoreRepository.GetProductById(item.productId);
				if (product.stock < item.quantity)
				{
					return NotFound("Not enough stock of product: "+ product.name);
				}


				item.orderId = order.orderId;
				item.orderItemId = (order.orderItems.IndexOf(item))+1;
				AddOrderItem(item);
			}

			IEnumerable<OrderItem> orderItems = _drinkStoreRepository.GetOrderItemsByOrder(order.orderId);

			decimal total = 0;
			foreach (var item in orderItems)
			{
				total = item.priceQuantity + total;
			}
			order.finalPrice = total;

			UpdateOrder(order.orderId, order);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("order/{id}")]
		public ActionResult UpdateOrder(int id, [FromBody] Order order)
		{
			var existingOrder = _drinkStoreRepository.GetOrderById(id);

			if (existingOrder == null)
			{
				return NotFound("No order found");
			}

		
			_drinkStoreRepository.UpdateOrder(existingOrder);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("order/{id}")]
		public ActionResult DeleteOrder(int id)
		{
			var existingOrder = _drinkStoreRepository.GetOrderById(id);

			if (existingOrder == null)
			{
				return NotFound("No order found");
			}

			_drinkStoreRepository.DeleteOrder(id);

			return NoContent();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("order/by-customer/{id}")]
		public ActionResult<IEnumerable<Order>> GetOrderByCustomer(int id)
		{
			var orders = _drinkStoreRepository.GetOrdersByCustomer(id);

			if (orders == null || !orders.Any())
			{
				return NotFound("No orders for customer found");
			}

			return Ok(orders);
		}

		// OrderItem

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("orderItem")]
		public ActionResult<IEnumerable<OrderItem>> GetAllOrderItems()
		{
			var obj = _drinkStoreRepository.GetAllOrderItems();
			if (obj.IsNullOrEmpty())
			{
				return NotFound("No order items found");
			}
			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("orderItem/{id}")]
		public ActionResult<OrderItem> GetOrderItemById(int id)
		{
			var obj = _drinkStoreRepository.GetOrderItemById(id);
			if (obj == null)
			{
				return NotFound("No order item found");
			}

			return Ok(obj);
		}


		[Authorize(Policy = IdentityData.CustomerPolicy)]
		[HttpPost("orderItem")]
		public ActionResult AddOrderItem([FromBody] OrderItem orderItem)
		{
			Product product = _drinkStoreRepository.GetProductById(orderItem.productId);

			orderItem.priceQuantity = orderItem.quantity * product.price;

			_drinkStoreRepository.AddOrderItem(orderItem);	

			return Ok();
			
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("orderItem/{id}")]
		public ActionResult UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
		{
			var existingOrderItem = _drinkStoreRepository.GetOrderItemById(id);

			if (existingOrderItem == null)
			{
				return NotFound("No order item found");
			}

			_drinkStoreRepository.UpdateOrderItem(existingOrderItem);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("orderItem/{id}")]
		public ActionResult DeleteOrderItem(int id)
		{
			var existingOrderItem = _drinkStoreRepository.GetOrderById(id);

			if (existingOrderItem == null)
			{
				return NotFound("No order item found");
			}

			_drinkStoreRepository.DeleteOrderItem(id);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("orderItem/by-order/{id}")]
		public ActionResult<IEnumerable<OrderItem>> GetOrderItemByOrder(int id)
		{
			var orderItems = _drinkStoreRepository.GetOrderItemsByOrder(id);

			if (orderItems == null || !orderItems.Any())
			{
				return NotFound("No order items for that order");
			}

			return Ok(orderItems);
		}

		// Administrator

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("administrator")]
		public ActionResult<IEnumerable<Administrator>> GetAllAdministrators()
		{
			var obj = _drinkStoreRepository.GetAllAdministrators();
			if (obj.IsNullOrEmpty())
			{
				return NotFound("No administrator found");
			}
			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("administrator/{id}")]
		public ActionResult<Administrator> GetAdminById(int id)
		{
			var obj = _drinkStoreRepository.GetAdministratorById(id);
			if (obj == null)
			{
				return NotFound("No administrator found");
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

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("administrator/{id}")]
		public ActionResult UpdateAdministrator(int id, [FromBody] AdministratorUpdateDTO adminUpdateDTO)
		{
			var existingAdmin = _drinkStoreRepository.GetAdministratorById(id);
			if (existingAdmin == null)
			{
				return NotFound("No administrator found");
			}

			_mapper.Map(adminUpdateDTO, existingAdmin);
			_drinkStoreRepository.UpdateAdministrator(existingAdmin);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("administrator/{id}")]
		public ActionResult DeleteAdministrator(int id)
		{
			var existingAdmin = _drinkStoreRepository.GetAdministratorById(id);
			if (existingAdmin == null)
			{
				return NotFound("No administrator found");
			}

			_drinkStoreRepository.DeleteAdministrator(id);

			return Ok();
		}



		// Customer

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("customer")]
		public ActionResult<IEnumerable<Customer>> GetAllCustomers()
		{
			var obj = _drinkStoreRepository.GetAllCustomers();
			if (obj.IsNullOrEmpty())
			{
				return NotFound("No customers found");
			}
			return Ok(obj);
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpGet("customer/{id}")]
		public ActionResult<Customer> GetCustomerById(int id)
		{
			var obj = _drinkStoreRepository.GetCustomerById(id);
			if (obj == null)
			{
				return NotFound("No custmore found");
			}

			return Ok(obj);
		}

		[AllowAnonymous]
		[HttpPost("customer")]
		public ActionResult AddCustomer([FromBody] CustomerCreateDTO customerCreateDTO)
		{
			var obj = _mapper.Map<Customer>(customerCreateDTO);
			_drinkStoreRepository.AddCustomers(obj);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpPut("customer/{id}")]
		public ActionResult UpdateCustomer(int id, [FromBody] CustomerUpdateDTO customerUpdateDTO)
		{
			var existingCustomer = _drinkStoreRepository.GetCustomerById(id);
			if (existingCustomer == null)
			{
				return NotFound("No customer found");
			}

			_mapper.Map(customerUpdateDTO, existingCustomer);
			_drinkStoreRepository.UpdateCustomers(existingCustomer);

			return Ok();
		}

		[Authorize(Policy = IdentityData.AdminPolicy)]
		[HttpDelete("customer/{id}")]
		public ActionResult DeleteCustomer(int id)
		{
			var existingCustomer = _drinkStoreRepository.GetCustomerById(id);
			if (existingCustomer == null)
			{
				return NotFound("No customer found");
			}

			_drinkStoreRepository.DeleteCustomers(id);

			return Ok();
		}

		

	}
}
