using System;

namespace FoodShop.Store
{
    public partial class Product
    {
        private decimal _price = 0.0m;

        public Product()
        {
        }

        //nullable property 
        public decimal Price { get => _price; set => _price = value; }
        public string DescriptionName { get; set; }
    }
}
