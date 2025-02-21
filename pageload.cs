using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    public class PageLoading
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        }
        public async Task PageLoad()
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync("https://qa-practice.netlify.app/bugs-form");

            var title = await page.TitleAsync();
            Assert.AreEqual("QA Practice | Learn with RV", title);

        }

        public async Task Cleanup()
        {
            await _browser.CloseAsync();
        }
    }
}
