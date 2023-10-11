using eTickets.Models;

namespace eTickets.Data.Services
{
	public interface IOrdersService
	{
		Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);
		Task<List<Order>> GetOrdersByUserIdAsync(string userId);

	}
}
