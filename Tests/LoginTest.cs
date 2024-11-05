using Microsoft.Playwright;

namespace Saucedemo.Tests
{
    public class LoginTest : BaseTest
    {
       
        [Test]
        public async Task LoginTests()
        {
            var page = await Browser.NewPageAsync();
            await page.GotoAsync("https://www.saucedemo.com/");

            // Interact with the login form
            await page.FillAsync("#user-name", "standard_user");
            await page.FillAsync("#password", "secret_sauce");
            await page.ClickAsync("#login-button");

            // Assertion to verify successful login
            Assert.That(await page.InnerTextAsync(".title"), Is.EqualTo("Products"));

            await page.CloseAsync();
        }
    }
}