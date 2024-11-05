using Saucedemo.Models;
using Saucedemo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Tests
{
    [TestFixture]
    public class PurchaseFlowTests : BaseTest
    {
        private const string Url = "https://www.saucedemo.com/";
        private const string Username = "standard_user";
        private const string Password = "secret_sauce";

        [Test]
        public async Task CompletePurchaseFlow_Success()
        {
            try
            {
                // Navigate to the page and wait for it to be ready
                await Page.GotoAsync(Url);
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                // Login
                await Page.FillAsync("#user-name", Username);
                await Page.FillAsync("#password", Password);
                await Page.ClickAsync("#login-button");

                // Wait for inventory page to load
                await Page.WaitForSelectorAsync(".inventory_list", new() { State = WaitForSelectorState.Visible });

                // Add items to cart
                var addToCartButtons = new[]
                {
                "[data-test='add-to-cart-sauce-labs-backpack']",
                "[data-test='add-to-cart-sauce-labs-bike-light']"
            };

                foreach (var button in addToCartButtons)
                {
                    await Page.WaitForSelectorAsync(button, new() { State = WaitForSelectorState.Visible });
                    await Page.ClickAsync(button);
                }

                // Go to cart
                await Page.WaitForSelectorAsync(".shopping_cart_link");
                await Page.ClickAsync(".shopping_cart_link");
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                // Proceed to checkout
                await Page.WaitForSelectorAsync("[data-test='checkout']");
                await Page.ClickAsync("[data-test='checkout']");
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                // Fill checkout information
                await Page.WaitForSelectorAsync("[data-test='firstName']");
                await Page.FillAsync("[data-test='firstName']", "John");
                await Page.FillAsync("[data-test='lastName']", "Doe");
                await Page.FillAsync("[data-test='postalCode']", "12345");

                // Complete checkout
                await Page.ClickAsync("[data-test='continue']");
                await Page.WaitForSelectorAsync(".checkout_summary_container");

                await Page.ClickAsync("[data-test='finish']");
                await Page.WaitForSelectorAsync(".complete-header");

                // Verify order completion
                var confirmationMessage = await Page.TextContentAsync(".complete-header");
                Assert.That(confirmationMessage, Is.EqualTo("Thank you for your order!"));
            }
            catch (Exception ex)
            {
                // Take screenshot on failure
                if (Page != null)
                {
                    var screenshotPath = $"error-screenshot-{DateTime.Now:yyyyMMddHHmmss}.png";
                    await Page.ScreenshotAsync(new() { Path = screenshotPath });
                    TestContext.AddTestAttachment(screenshotPath);
                }
                throw; // Rethrow the exception to fail the test
            }
        }
    }
}
