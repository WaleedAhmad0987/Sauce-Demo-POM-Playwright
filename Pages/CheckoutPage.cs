using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Pages
{
    public class CheckoutPage : BasePage
    {
        private const string FirstNameInput = "[data-test='firstName']";
        private const string LastNameInput = "[data-test='lastName']";
        private const string PostalCodeInput = "[data-test='postalCode']";
        private const string ContinueButton = "[data-test='continue']";
        private const string FinishButton = "[data-test='finish']";
        private const string CompleteHeader = ".complete-header";

        public CheckoutPage(IPage page) : base(page) { }

        public async Task FillShippingInfo(string firstName, string lastName, string postalCode)
        {
            await Page.FillAsync(FirstNameInput, firstName);
            await Page.FillAsync(LastNameInput, lastName);
            await Page.FillAsync(PostalCodeInput, postalCode);
            await Page.ClickAsync(ContinueButton);
            await WaitForPageLoad();
        }

        public async Task CompleteOrder()
        {
            await Page.ClickAsync(FinishButton);
            await WaitForPageLoad();
        }

        public async Task<string> GetConfirmationMessage() =>
            await Page.InnerTextAsync(CompleteHeader);
    }
}
