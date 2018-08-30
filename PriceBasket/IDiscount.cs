using System.Collections.Generic;
using System.Linq;

namespace FoodShop.Store
{
    //Each discount object implements the IDiscount interface
    public interface IDiscount
    {
        bool DiscountApplied { get; }
        string Description { get; }
        void ApplyDiscount(IList<CartItem> cartItems, string productName);
    }

    //Create the discount object with the type of discount passed into the object.
    //The apply discount method then executes the correct method when called.
    public class PercentOffDiscount : IDiscount
    {
        protected PercentOffDiscount() { }
        private bool _discountApplied = false;
        public int DiscountPercentage { get; private set; }
        private string _productname = null;
        private string _desc = null;

        public string Description
        {
            get => _desc;
        }

        //Discount percentage should be <= 100
        public PercentOffDiscount(int percentOff)
        {
            //Description = desc;
            DiscountPercentage = percentOff;
        }

        public bool DiscountApplied
        {
            get => _discountApplied;
        }

        public void ApplyDiscount(IList<CartItem> cartItems, string productName)
        {
            CartItem ci = cartItems.ToList().Find
              (p => p.Item.DescriptionName.Equals(productName));
            ci.DiscountAmount = (ci.Item.Price * (DiscountPercentage * 0.01m)) * ci.ItemQuantity;
            _discountApplied = true;
            _productname = productName;
            _desc = productName + " " + DiscountPercentage + "% off " + ci.DiscountAmount.ToString("C");
        }
    }

    public class BuyProductAGetDiscountOnProductY : IDiscount
    {
        protected BuyProductAGetDiscountOnProductY() { }

        public Product DiscountedProduct { get; set; }
        
        public int RequiredQuantity { get; private set; }
        //Discount percentage should be <= 100
        public int DiscountPercentage { get; private set; }
        public string Description
        {
            get => _desc;
        }

        private string _productName;
        private bool _discountApplied;
        private string _desc = null;

        public bool DiscountApplied
        {
            get => _discountApplied;
        }

        public BuyProductAGetDiscountOnProductY(string yproductName, int quantityOfA, int percentOffY)
        {
            //Description = desc;
            _productName = yproductName;
            RequiredQuantity = quantityOfA;
            DiscountPercentage = percentOffY;
        }

        public void ApplyDiscount(IList<CartItem> cartItems, string productName)
        {
            CartItem Aci = cartItems.ToList().Find
            (p => p.Item.DescriptionName.Equals(productName));
            CartItem Bci = cartItems.ToList().Find
            (p => p.Item.DescriptionName.Equals(_productName));

            int numItemsToDiscount = 0;

            if (Aci.ItemQuantity >= RequiredQuantity && Bci != null)
            {
                Bci.DiscountAmount = Bci.Item.Price * (DiscountPercentage * 0.01m);
                numItemsToDiscount = Aci.ItemQuantity / RequiredQuantity;
                if (Bci.ItemQuantity >= numItemsToDiscount)
                    Bci.DiscountAmount = Bci.DiscountAmount * numItemsToDiscount;
                else
                    Bci.DiscountAmount = Bci.DiscountAmount * Bci.ItemQuantity;

                _discountApplied = true;
                _desc = "Get " + DiscountPercentage + "% off " + _productName + ", when you buy "
                    + RequiredQuantity + " " + productName + ": " + Bci.DiscountAmount.ToString("C");
            }
           


        }

    }

}
