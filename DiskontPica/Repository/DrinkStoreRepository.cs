using DiskontPica.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace DiskontPica.Repository
{
	public class DrinkStoreRepository : IDrinkStoreRepository
	{
		private readonly DrinkStoreContext _dbContext;

		public DrinkStoreRepository(DrinkStoreContext dbContext)
		{
			_dbContext = dbContext;
		}



		public void AddAdministrator(Administrator administrator)
		{
			_dbContext.Administrators.Add(administrator);
			_dbContext.SaveChanges();
			administrator.password = HashPassword(administrator.password, administrator.salt);
			_dbContext.Administrators.Update(administrator);
			_dbContext.SaveChanges();
		}

		public void AddCategory(Category category)
		{
			_dbContext.Categories.Add(category);
			_dbContext.SaveChanges();
		}

		public void AddCountry(Country country)
		{
			_dbContext.Countries.Add(country);
			_dbContext.SaveChanges();
		}

		public void AddCustomers(Customer customer)
		{
			_dbContext.Customers.Add(customer);
			_dbContext.SaveChanges();
			customer.password = HashPassword(customer.password, customer.salt);
			_dbContext.Customers.Update(customer);
			_dbContext.SaveChanges();
		}

		public void AddOrder(Order order)
		{
			_dbContext.Orders.Add(order);
			_dbContext.SaveChanges();
		}

		public void AddOrderItem(OrderItem orderItem)
		{
			_dbContext.OrderItems.Add(orderItem);
			_dbContext.SaveChanges();
		}

		public void AddProduct(Product product)
		{
			_dbContext.Products.Add(product);
			_dbContext.SaveChanges();
		}

		public void DeleteAdministrator(int administratorId)
		{
			var obj = _dbContext.Administrators.Find(administratorId);
			if (obj != null)
			{
				_dbContext.Administrators.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteCategory(int categoryId)
		{
			var obj = _dbContext.Categories.Find(categoryId);
			if (obj != null)
			{
				_dbContext.Categories.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteCountry(int countryId)
		{
			var obj = _dbContext.Countries.Find(countryId);
			if (obj != null)
			{
				_dbContext.Countries.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteCustomers(int customerId)
		{
			var obj = _dbContext.Customers.Find(customerId);
			if (obj != null)
			{
				_dbContext.Customers.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteOrder(int orderId)
		{
			var obj = _dbContext.Orders.Find(orderId);
			if (obj != null)
			{
				_dbContext.Orders.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteOrderItem(int orderItemId)
		{
			var obj = _dbContext.OrderItems.Find(orderItemId);
			if (obj != null)
			{
				_dbContext.OrderItems.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteProduct(int productId)
		{
			var obj = _dbContext.Products.Find(productId);
			if (obj != null)
			{
				_dbContext.Products.Remove(obj);
				_dbContext.SaveChanges();
			}
		}


		public IEnumerable<Administrator> GetAllAdministrators()
		{
			return _dbContext.Administrators.ToList();
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _dbContext.Categories.ToList();
		}

		public IEnumerable<Country> GetAllCountries()
		{
			return _dbContext.Countries.ToList();
		}

		public IEnumerable<Customer> GetAllCustomers()
		{
			return _dbContext.Customers.ToList();
		}

		public IEnumerable<OrderItem> GetAllOrderItems()
		{
			return _dbContext.OrderItems.ToList();
		}

		public IEnumerable<Order> GetAllOrders()
		{
			return _dbContext.Orders.ToList();
		}

		public IEnumerable<Product> GetAllProducts()
		{
			return _dbContext.Products.ToList();
		}
		public Administrator GetAdministratorById(int administratorId)
		{
			return _dbContext.Administrators.Find(administratorId);
		}

		public Category GetCategoryById(int categoryId)
		{
			return _dbContext.Categories.Find(categoryId);
		}

		public Country GetCountryById(int countryId)
		{
			return _dbContext.Countries.Find(countryId);
		}

		public Customer GetCustomerById(int customerId)
		{
			return _dbContext.Customers.Find(customerId);
		}

		public Order GetOrderById(int orderId)
		{
			return _dbContext.Orders.Find(orderId);
		}

		public OrderItem GetOrderItemById(int orderItemId)
		{
			return _dbContext.OrderItems.Find(orderItemId);
		}

		public Product GetProductById(int productId)
		{
			return _dbContext.Products.Find(productId);
		}

		public void UpdateAdministrator(Administrator administrator)
		{
			_dbContext.Entry(administrator).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void UpdateCategory(Category category)
		{
			_dbContext.Entry(category).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void UpdateCountry(Country country)
		{
			_dbContext.Entry(country).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void UpdateCustomers(Customer customer)
		{
			_dbContext.Entry(customer).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void UpdateOrder(Order order)
		{
			_dbContext.Entry(order).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void UpdateOrderItem(OrderItem orderItem)
		{
			_dbContext.Entry(orderItem).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void UpdateProduct(Product product)
		{
			_dbContext.Entry(product).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}




		public IEnumerable<Product> GetProductsByCountry(int id)
		{
			return _dbContext.Products
		   .Where(p => p.country.countryId == id)
		   .ToList();
		}

		public IEnumerable<Product> GetProjectsByCategory(int id)
		{
			return _dbContext.Products
		   .Where(p => p.category.categoryId == id)
		   .ToList();
		}

		public IEnumerable<Order> GetOrdersByCustomer(int id)
		{
			return _dbContext.Orders
		   .Where(p => p.customer.customerld == id)
		   .ToList();
		}

		public IEnumerable<OrderItem> GetOrderItemsByOrder(int id)
		{
			return _dbContext.OrderItems
		   .Where(p => p.order.orderId == id)
		   .ToList();
		}

		private string HashPassword(string password, string salt)
		{
			var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: Convert.FromBase64String(salt),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return hashed;
		}

		public Administrator GetAdministratorWithCredentials(string name, string password)
		{
			var admin = _dbContext.Administrators.First(u => u.name == name) ?? throw new Exception("User not found");

			var hashedPassword = HashPassword(password, admin.salt);

			return admin.password == hashedPassword ? admin : null;
		}

		public Customer GetCustomerWithCredentials(string name, string password)
		{
			var customer = _dbContext.Customers.First(u => u.name == name) ?? throw new Exception("User not found");

			var hashedPassword = HashPassword(password, customer.salt);

			return customer.password == hashedPassword ? customer : null;
		}
	}
}
