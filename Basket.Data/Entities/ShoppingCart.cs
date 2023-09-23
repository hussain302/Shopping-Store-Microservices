
namespace Basket.Data.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItem { get; set; } = new List<ShoppingCartItem>();
      
        public ShoppingCart() {  }

        public ShoppingCart(string userName) { UserName = userName;  }

        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (ShoppingCartItem item in ShoppingCartItem)
                {
                    totalPrice += (item.Price * item.Quantity);
                }
                return totalPrice;
            }
        }

    }
}