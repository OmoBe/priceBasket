csc /target:library /out:FoodShop.DLL Product.cs ShoppingCart.cs TheStore.cs IDiscount.cs
csc /out:pricebasket.exe /reference:FoodShop.DLL PriceBasket.cs 