using System.Collections.Generic;

namespace FoodShop.Store
{
    //Static store class
    public class ShoppingInvoice
    {
        protected ShoppingInvoice()
        { }

        private ShoppingCart _sc = null;

        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }
        private IList<string> _discountMessages = null; 

        private const string _defaultMessage = "(no offers available)";

        public IList<string> DiscountMessages
        {
            get
            {
                return _discountMessages;
            }
        }

        public ShoppingInvoice(ShoppingCart sc)
        {
            _sc = sc;
            _discountMessages = new List<string>();
        }

        //Produces an invoice with the updated total for the products in the shopping cart
        public void ProduceInvoice()
        {
            //No shopping cart to produce an invoice for
            if (_sc == null)
                return;

            Subtotal = _sc.SubTotal;
            decimal total = 0.0m;
            foreach (CartItem ci in _sc.CartItems)
            {
                total = ((ci.ItemQuantity * ci.Item.Price) - ci.DiscountAmount)+ total;
            }
            Total = total;

            //No offers were bought
            if (total == Subtotal)
            {
                //_discountMessages = new List<string>();
                _discountMessages.Add(_defaultMessage);
            }

        }

    }


    public static class TheStore
    {


        private static readonly IList<Product> _products = new List<Product>
        {
            new Product() { DescriptionName = "soup", Price = 0.65m},
            new Product() { DescriptionName = "bread", Price = 0.8m},
            new Product() { DescriptionName = "milk", Price = 1.3m},
            new Product() { DescriptionName = "apples", Price = 1m}
        };

        private static readonly IDictionary<string, IDiscount> _discounts = new Dictionary<string, IDiscount>
        {
            {"apples", new PercentOffDiscount( 10) },
            {"soup", new BuyProductAGetDiscountOnProductY("bread", 2, 50) }
        };

        public static IList<Product> AvailableProducts { get => _products; }
        public static IDictionary<string, IDiscount> AvailableDiscounts { get => _discounts; }

    }

}
