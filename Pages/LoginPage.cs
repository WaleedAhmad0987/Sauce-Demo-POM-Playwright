using Microsoft.Playwright;
using Saucedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Pages
{
    public class LoginPage : BasePage
    {
        private const string UserNameInput = "#user-name";
        private const string PasswordInput = "#password";
        private const string LoginButton = "#login-button";
        private const string ErrorMessage = "[data-test='error']";

        public LoginPage(IPage page) : base(page) { }

        public async Task Login(User user)
        {
            await Page.FillAsync(UserNameInput, user.Username);
            await Page.FillAsync(PasswordInput, user.Password);
            await Page.ClickAsync(LoginButton);
            await WaitForPageLoad();
        }

        public async Task<string> GetErrorMessage() =>
            await Page.TextContentAsync(ErrorMessage);
    }
}
