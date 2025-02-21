using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    public class BugsinPage
    {
        private IPlaywright _playwright;
        private IBrowser _browser;

        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        }

        public async Task IncorrectSpelling()
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync("https://qa-practice.netlify.app/bugs-form");

            var phoneLabel = await page.QuerySelectorAsync("label[for='Phone']");
            Assert.IsNotNull(phoneLabel, "Phone label not found!");
            var phoneLabelText = await phoneLabel.InnerTextAsync();
            Assert.AreEqual("Phone Number", phoneLabelText, "Phone label has incorrect spelling. Found: " + phoneLabelText);
        }

       public async Task MandatoryFields()
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync("https://qa-practice.netlify.app/bugs-form");

            var firstNameLabel = await page.QuerySelectorAsync("label[for='First name']");
            Assert.IsNotNull(firstNameLabel, "'First name' label not found!");

            var firstNameRequired = await firstNameLabel.InnerTextAsync();
            Assert.IsTrue(firstNameRequired.Contains("First Name *"), "'First Name' field is not marked as required.");
   
        }

        public async Task DisabledCheckbox()
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync("https://qa-practice.netlify.app/bugs-form");

            var agreeCheckbox = await page.QuerySelectorAsync("input[type='checkbox'][name='I agree with the terms and conditions']");
            Assert.IsNotNull(agreeCheckbox, "Checkbox for terms and conditions not found!");

            var isCheckboxDisabled = await agreeCheckbox.GetAttributeAsync("disabled");
            Assert.IsNull(isCheckboxDisabled, "Checkbox is disabled, it should be enabled.");
        }

        public async Task Cleanup()
        {
            await _browser.CloseAsync();
        }
    }
}
