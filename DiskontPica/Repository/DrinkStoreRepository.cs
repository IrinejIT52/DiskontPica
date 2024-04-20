using DiskontPica.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
			_dbContext.Administrator.Add(administrator);
			_dbContext.SaveChanges();
			administrator.password = HashPassword(administrator.password, administrator.salt);
			_dbContext.Administrator.Update(administrator);
			_dbContext.SaveChanges();
		}

		public void AddCategory(Category category)
		{
			_dbContext.Category.Add(category);
			_dbContext.SaveChanges();
		}

		public void AddCountry(Country country)
		{
			_dbContext.Country.Add(country);
			_dbContext.SaveChanges();
		}

		public void AddCustomers(Customer customer)
		{
			_dbContext.Customer.Add(customer);
			_dbContext.SaveChanges();
			customer.password = HashPassword(customer.password, customer.salt);
			_dbContext.Customer.Update(customer);
			_dbContext.SaveChanges();
		}

		public void AddOrder(Order order)
		{
			_dbContext.Orders.Add(order);
			_dbContext.SaveChanges();
		}

		public void AddOrderItem(OrderItem orderItem)
		{
			_dbContext.OrderItem.Add(orderItem);
			_dbContext.SaveChanges();
		}

		public void AddProduct(Product product)
		{
			_dbContext.Product.Add(product);
			_dbContext.SaveChanges();
		}

		public void DeleteAdministrator(int administratorId)
		{
			var obj = _dbContext.Administrator.Find(administratorId);
			if (obj != null)
			{
				_dbContext.Administrator.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteCategory(int categoryId)
		{
			var obj = _dbContext.Category.Find(categoryId);
			if (obj != null)
			{
				_dbContext.Category.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteCountry(int countryId)
		{
			var obj = _dbContext.Country.Find(countryId);
			if (obj != null)
			{
				_dbContext.Country.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteCustomers(int customerId)
		{
			var obj = _dbContext.Customer.Find(customerId);
			if (obj != null)
			{
				_dbContext.Customer.Remove(obj);
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
			var obj = _dbContext.OrderItem.Find(orderItemId);
			if (obj != null)
			{
				_dbContext.OrderItem.Remove(obj);
				_dbContext.SaveChanges();
			}
		}

		public void DeleteProduct(int productId)
		{
			var obj = _dbContext.Product.Find(productId);
			if (obj != null)
			{
				_dbContext.Product.Remove(obj);
				_dbContext.SaveChanges();
			}
		}


		public IEnumerable<Administrator> GetAllAdministrators()
		{
			return _dbContext.Administrator.ToList();
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _dbContext.Category.ToList();
		}

		public IEnumerable<Country> GetAllCountries()
		{
			return _dbContext.Country.ToList();
		}

		public IEnumerable<Customer> GetAllCustomers()
		{
			return _dbContext.Customer.ToList();
		}

		public IEnumerable<OrderItem> GetAllOrderItems()
		{
			return _dbContext.OrderItem.ToList();
		}

		public IEnumerable<Order> GetAllOrders()
		{
			return _dbContext.Orders.ToList();
		}

		public IEnumerable<Product> GetAllProducts()
		{
			return _dbContext.Product.ToList();
		}
		public Administrator GetAdministratorById(int administratorId)
		{
			return _dbContext.Administrator.Find(administratorId);
		}

		public Category GetCategoryById(int categoryId)
		{
			return _dbContext.Category.Find(categoryId);
		}

		public Country GetCountryById(int countryId)
		{
			return _dbContext.Country.Find(countryId);
		}

		public Customer GetCustomerById(int customerId)
		{
			return _dbContext.Customer.Find(customerId);
		}

		public Order GetOrderById(int orderId)
		{
			return _dbContext.Orders.Find(orderId);
		}

		public OrderItem GetOrderItemById(int orderItemId)
		{
			return _dbContext.OrderItem.Find(orderItemId);
		}

		public Product GetProductById(int productId)
		{
			return _dbContext.Product.Find(productId);
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
			return _dbContext.Product
		   .Where(p => p.countryId == id)
		   .ToList();
		}

		public IEnumerable<Product> GetProductsByCategory(int id)
		{
			return _dbContext.Product
		   .Where(p => p.categoryId == id)
		   .ToList();
		}
		public IEnumerable<Product> GetProductsByAdmin(int id)
		{
			return _dbContext.Product
		   .Where(p => p.adminId == id)
		   .ToList();
		}

		public IEnumerable<Order> GetOrdersByCustomer(int id)
		{
			return _dbContext.Orders
		   .Where(p => p.customerId == id)
		   .ToList();
		}

		public IEnumerable<OrderItem> GetOrderItemsByOrder(int id)
		{
			return _dbContext.OrderItem
		   .Where(p => p.orderId == id)
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

			var admin = _dbContext.Administrator.ToList().FirstOrDefault(u => u.name == name,defaultValue:null);
			if (admin != null)
			{
				var hashedPassword = HashPassword(password, admin.salt);

				return admin.password == hashedPassword ? admin : null;
			}

			return null;
		}

		public Customer GetCustomerWithCredentials(string name, string password)
		{

			var customer = _dbContext.Customer.ToList().FirstOrDefault(u => u.name == name, defaultValue: null);
			if (customer != null)
			{
				var hashedPassword = HashPassword(password, customer.salt);

				return customer.password == hashedPassword ? customer : null;
			}
			return null;
			
		}

		public IEnumerable<Product> GetProductsByQuery(string? search, string? sortColumn, string? sortOrder)
		{

			// pretrazivanje
			IQueryable<Product> productsQuery = _dbContext.Product;

			if (!string.IsNullOrWhiteSpace(search))
			{
				productsQuery = productsQuery.Where(p =>
					p.name.Contains(search.Trim()) || p.price.ToString().Contains(search.Trim()));
			}

			// sortiranje
			Expression<Func<Product, object>> keySelector = sortColumn?.ToLower().Trim() switch
			{
				"name" => product => product.name,
				"price" => product => product.price,
				_ => product => product.productId
			};

			if(sortOrder?.ToLower().Trim() == "desc")
			{
				productsQuery =	productsQuery.OrderByDescending(keySelector);
			}
			else
			{
				productsQuery = productsQuery.OrderBy(keySelector);
			}


			var products = productsQuery.ToList();

			return products;
		}


	}
}
