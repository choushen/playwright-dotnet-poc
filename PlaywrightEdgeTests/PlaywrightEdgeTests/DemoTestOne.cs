using System.Security.Cryptography.X509Certificates;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightEdgeTests
{

    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class DemoTestOne
    {

        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;
        private string _itemsXPathFormat = "//h4[text()=\"{0}\"]/following-sibling::div";


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


        [Test]
        public async Task DemoTest()
        {
            // Go to Page
            await _page!.GotoAsync("https://coffee-cart.app/");

            // Select Items
            // Get the siblings of the h2 element that contains the text 'Espresso'
            string espressoXPath = string.Format(_itemsXPathFormat, "Espresso ");
            await _page!.Locator(espressoXPath).ClickAsync();

            string espressoMacchiatoXPath = string.Format(_itemsXPathFormat, "Espresso Macchiato ");
            await _page!.Locator(espressoMacchiatoXPath).ClickAsync();

            string cappuccinoXPath = string.Format(_itemsXPathFormat, "Cappuccino ");
            await _page!.Locator(cappuccinoXPath).ClickAsync();


            // Click the checkout button
            await _page!.Locator("[data-test=\"checkout\"]").ClickAsync();


            // Fill in payment details and submit
            await _page.Locator("#name").FillAsync("Bob", new LocatorFillOptions{Timeout = 3000});
            await _page.Locator("#email").FillAsync("Bob@Smith.com");
            await _page.Locator("#submit-payment").ClickAsync();


            // Verify
            string verificationString = "Thanks for your purchase. Please check your email for payment.";

            // Find the div with the success class and get its text content
            string? completionBannerText = await _page.Locator("div.snackbar.success").TextContentAsync();

            // Assert that the text content matches the expected verification string
            Assert.AreEqual(verificationString, completionBannerText);
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