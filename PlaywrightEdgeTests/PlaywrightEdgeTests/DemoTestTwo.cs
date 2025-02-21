using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightEdgeTests
{

    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class Demo
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Channel = "msedge",
                Headless = false
            });
            _page = await _browser.NewPageAsync();
        }


        // given blalbalbalba
        [Test]
        public async Task DemoTest()
        {

            // Go to Page
            await _page!.GotoAsync("https://ppm2uat.leedsth.nhs.uk/");

            // Sign in
            await _page!.Locator("#Username").FillAsync("testcat5");
            await _page!.Locator("#Password").FillAsync("Yellow14");
            await _page!.Locator("#logIn").ClickAsync();


            // Navigate to patient
            await _page!.GotoAsync("https://ppm2uat.leedsth.nhs.uk/patients/63ddbcf0-b8d1-ef11-a7fc-00224840d1ea/ltht/summary");

            // Launch form
            await _page!.Locator("button:has-text('Add')").ClickAsync(); // Contains match but more specific selector
            await _page!.Locator("#btn-add-xform").ClickAsync();
            await _page!.Locator("input[placeholder='Filter']").FillAsync("stool");
            await _page!.Locator("li.result-item-row").Filter(new LocatorFilterOptions { HasText = "Stool Record Chart" }).ClickAsync();


            // Complete 


            // Verify


            //var title = await _page.TitleAsync();
            Assert.True(true);
        }


        [Test]
        public async Task DemoTest2()
        {

            // Go to Page
            await _page!.GotoAsync("https://ppm2uat.leedsth.nhs.uk/");

            // Sign in
            await _page!.Locator("#Username").FillAsync("testcat5");
            await _page!.Locator("#Password").FillAsync("Yellow14");
            await _page!.Locator("#logIn").ClickAsync();


            // Navigate to patient
            await _page!.GotoAsync("https://ppm2uat.leedsth.nhs.uk/patients/63ddbcf0-b8d1-ef11-a7fc-00224840d1ea/ltht/summary");

            // Launch form
            await _page!.Locator("button:has-text('Add')").ClickAsync(); // Contains match but more specific selector
            await _page!.Locator("#btn-add-xform").ClickAsync();
            await _page!.Locator("input[placeholder='Filter']").FillAsync("stool");
            await _page!.Locator("li.result-item-row").Filter(new LocatorFilterOptions { HasText = "Stool Record Chart" }).ClickAsync();


            // Complete 


            // Verify


            //var title = await _page.TitleAsync();
            Assert.True(true);
        }

        [TearDown]
        public async Task Teardown()
        {
            // Comment this out while debugging:
             await _browser!.CloseAsync();
            _playwright?.Dispose();
        }
    }
}