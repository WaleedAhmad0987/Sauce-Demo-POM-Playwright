using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Pages
{
    public class ProductsPage : BasePage
    {
        private const string Title = ".title";
        private const string AddToCartButton = "[data-test='add-to-cart-{0}']";
        private const string ShoppingCartBadge = ".shopping_cart_badge";
        private const string ShoppingCartLink = ".shopping_cart_link";

        public ProductsPage(IPage page) : base(page) { }

        public async Task<string> GetTitle() =>
            await Page.InnerTextAsync(Title);

        public async Task AddItemToCart(string itemId)
        {
            await Page.ClickAsync(string.Format(AddToCartButton, itemId));
        }

        public async Task GoToCart()
        {
            await Page.ClickAsync(ShoppingCartLink);
            await WaitForPageLoad();
        }
    }
}
