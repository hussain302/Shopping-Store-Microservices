﻿namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem() { }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}