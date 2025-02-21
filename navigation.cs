using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    public class Navigation
    {
        private IPlaywright _playwright;
        private IBrowser _browser;

        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        }

        public async Task FormInputs()
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync("https://qa-practice.netlify.app/bugs-form");

            await page.FillAsync("input[name='First Name']", "Code");
            await page.FillAsync("input[name='Last Name']", "Test");
            await page.FillAsync("input[name='Phone nunber']", "1234567890");
            await page.FillAsync("input[name='Email address']", "codetest@gmail.com");            
            await page.FillAsync("input[name='Password']", "Password123");

            var submitButton = await page.QuerySelectorAsync("button[type='submit']");
            Assert.IsNotNull(submitButton, "Submit button not found!");
            await submitButton.ClickAsync();

            var successMessage = await page.QuerySelectorAsync(".success-message");
            Assert.IsNotNull(successMessage, "Success message not found!");
        }

        public async Task Cleanup()
        {
            await _browser.CloseAsync();
        }
    }
}
