using System.Collections.Generic;
using System.Linq;

namespace FoodShop.Store
{
    public class CartItem
    {
        protected CartItem() { }

        public CartItem(Product p)
        {
            this.Item = p;
            this.ItemQuantity = 1;
            this.DiscountAmount = 0.0m;
        }

        public Product Item { get; private set; }
        public int ItemQuantity { get; set; }

        public decimal DiscountAmount { get; set; }
    }

    public class ShoppingCart
    {
        private List<CartItem> _items = null;
        private decimal _st = 0.0m;

        //Constructor initialises cart properties to default values
        public ShoppingCart()
        {
            this.SubTotal = 0.00m;
            //this.NumberOfItems = 0;
            _items = new List<CartItem>();
        }

        public int NumberOfItems
        {
            get => _items.Count();
        }

        public decimal SubTotal
        {
            get
            {
                
                foreach (CartItem _sc in _items)
                {
                    _st = (_sc.ItemQuantity * _sc.Item.Price) + _st;
                }
                return _st;
            }
           private set => _st = value;
        }

      
        public IList<CartItem> CartItems { get => _items; }

        //
        // Summary:
        //     adds an item to the cart or
        //     increases the quatity of an item already in the cart.
        //
        // Parameters:
        //   aProduct:
        //     The product to add to the cart.
        public void AddItemToCart(Product aProduct)
        {
            CartItem ci = _items.Find
                (p => p.Item.DescriptionName.Equals(aProduct.DescriptionName));

            if (ci != null)
                ci.ItemQuantity++;
            else
                _items.Add(new CartItem(aProduct));
            
        }

        public ShoppingInvoice ProcessCart()
        {
            IDictionary<string, IDiscount> discounts = TheStore.AvailableDiscounts;
            ShoppingInvoice si = new ShoppingInvoice(this);
            
            foreach (CartItem ci in _items)
            {
                IDiscount discount = null;
                if(discounts.TryGetValue(ci.Item.DescriptionName, out discount))
                {
                    discount.ApplyDiscount(_items, ci.Item.DescriptionName);
                    si.DiscountMessages.Add(discount.Description);
                }
            }
            si.ProduceInvoice();
            return si;
        }

    }
}
