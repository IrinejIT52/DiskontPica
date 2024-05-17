using DiskontPica.Models;

namespace DiskontPica.Repository
{
	public interface IDrinkStoreRepository
	{
		// Country
		IEnumerable<Country> GetAllCountries();
		Country GetCountryById(int countryId);
		void AddCountry(Country country);
		void UpdateCountry(Country country);
		void DeleteCountry(int countryId);

		// Category
		IEnumerable<Category> GetAllCategories();
		Category GetCategoryById(int categoryId);
		void AddCategory(Category category);
		void UpdateCategory(Category category);
		void DeleteCategory(int categoryId);

		// Administator
		IEnumerable<Administrator> GetAllAdministrators();
		Administrator GetAdministratorById(int administratorId);
		void AddAdministrator(Administrator administrator);
		void UpdateAdministrator(Administrator administrator);
		void DeleteAdministrator(int administratorId);

		IQueryable<Administrator> GetAdministratorByEmail(string email);
		Administrator GetAdministratorWithCredentials(string name, string password);


		// Product
		IEnumerable<Product> GetAllProducts();
		Product GetProductById(int productId);
		void AddProduct(Product product);
		void UpdateProduct(Product product);
		void DeleteProduct(int productId);

		IEnumerable<Product> GetProductsByCountry(int id);
		IEnumerable<Product> GetProductsByCategory(int id);
		IEnumerable<Product> GetProductsByAdmin(int id);
		IEnumerable<Product> GetProductsByQuery(string? search,string? sortColumn,string? sortOrder);


		//Order
		IEnumerable<Order> GetAllOrders();
		Order GetOrderById(int orderId);
		void AddOrder(Order order);
		void UpdateOrder(Order order);
		void DeleteOrder(int orderId);
		IEnumerable<Order> GetOrdersByCustomer(int id);

		//Order Item
		IEnumerable<OrderItem> GetAllOrderItems();
		OrderItem GetOrderItemById(int orderItemId);
		void AddOrderItem(OrderItem orderItem);
		void UpdateOrderItem(OrderItem orderItem);
		void DeleteOrderItem(int orderItemId);

		IEnumerable<OrderItem> GetOrderItemsByOrder(int id);

		//Customer
		IEnumerable<Customer> GetAllCustomers();
		Customer GetCustomerById(int customerId);
		void AddCustomers(Customer customer);
		void UpdateCustomers(Customer customer);
		void DeleteCustomers(int customerId);
		Customer GetCustomerWithCredentials(string name, string password);

		IQueryable<Customer> GetCustomerByName(string name);
	}
}
