using Microsoft.Playwright;
using Saucedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Pages
{
    public class CartPage : BasePage
    {
        private const string CheckoutButton = "[data-test='checkout']";
        private const string CartItem = ".cart_item";

        public CartPage(IPage page) : base(page) { }

        public async Task<int> GetItemCount() =>
            await Page.Locator(CartItem).CountAsync();

        public async Task Checkout()
        {
            await Page.ClickAsync(CheckoutButton);
            await WaitForPageLoad();
        }
    }
}
