using FoodShop.Store;
using System;
using System.Linq;

class PriceBasket
{
    static void Main(string[] args)
    {
        ShoppingCart _cart = null;

        /*Check that there are products added 
        in the command line arguments*/
        if (args.Length > 0)
        {
            _cart = new ShoppingCart();

            //check each item in the command line arguments is a valid product
            foreach (string sItem in args)
            {

                Product anItem = TheStore.AvailableProducts.ToList().Find
                    (ap => ap.DescriptionName.Equals(sItem.ToLower()));

                if (anItem != null)
                    _cart.AddItemToCart(anItem);
                else
                {
                    Console.WriteLine("\n***************************************");
                    Console.WriteLine("\tPLEASE ENTER A VALID ITEM");
                    Console.WriteLine("***************************************");
                    Console.WriteLine("{0} is not a valid item", sItem);
                    Console.WriteLine("Valid items include: Apples, Bread, Milk, Soup");
                    break;
                }

            }
            //Produce an invoice for the item(s)
            ShoppingInvoice si = _cart.ProcessCart();
            Console.WriteLine("\n***************************************");
            Console.WriteLine("SubTotal: " + si.Subtotal.ToString("C"));
            foreach (string s in si.DiscountMessages)
                Console.WriteLine(s);
            Console.WriteLine("Total: " + si.Total.ToString("C"));
            Console.WriteLine("***************************************");


            //Console.WriteLine(_cart.CartItems[0].DiscountAmount);

        }
        //User did not enter any products on the command line
        else
        {
            
            Console.WriteLine("Please list some items to add");
            Console.WriteLine("Valid items include: Apples, Bread, Milk, Soup");
            Console.WriteLine(args.Length);
        }
    }
}