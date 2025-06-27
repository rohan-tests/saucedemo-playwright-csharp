using System.Diagnostics;
using Microsoft.Playwright;
using Pages.CartPage;
using Pages.CheckoutPage;
using Pages.ProductsPage;
using Saucedemo.LoginPage;

namespace PlaywrightTests.BaseTest
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class BaseTest
    {
        private Stopwatch watch = null!;
        private IBrowser browser = null!;
        private IPage page = null!;
        private IPlaywright playwright = null!;
        private LoginPage loginPage = null!;
        private ProductsPage productsPage = null!;
        private CartPage cartPage = null!;
        private CheckoutPage checkoutPage = null!;

        [SetUp]
        public async Task Setup()
        {
            watch = Stopwatch.StartNew();
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync(
                new BrowserNewContextOptions
                {
                    ViewportSize = null
                });
            page = await context.NewPageAsync();
            loginPage = new LoginPage(page);
            productsPage = new ProductsPage(page);
            cartPage = new CartPage(page);
            checkoutPage = new CheckoutPage(page);
        }
        [Test]
        public async Task BasicCheckoutFlow()
        {
            await loginPage.PerformLogin("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            await productsPage.AddToCart(1);
            await productsPage.AddToCart(2);
            string cur_cart_value = await productsPage.GetCurCartValue();
            Assert.That(cur_cart_value, Is.EqualTo("2"));
            await cartPage.GotoCart();
            await cartPage.RemoveItem(1);
            cur_cart_value = await productsPage.GetCurCartValue();
            Assert.That(cur_cart_value, Is.EqualTo("1"));
            Console.WriteLine(await cartPage.GetProductNameFromCart());
            await cartPage.CheckOut();
            await checkoutPage.FillInfo("John", "Doe", "874857");
            await page.ClickAsync("#continue");
            Console.WriteLine(await checkoutPage.GetItemNameOnCheckout());
            await page.ClickAsync("#finish");
            Assert.That(await page.InnerTextAsync("h2.complete-header"), Is.EqualTo("Thank you for your order!"));
        }
        [Test]
        public async Task CheckoutWithDiffItems()
        {
            await loginPage.PerformLogin("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            await productsPage.AddToCart(0);
            await productsPage.AddToCart(1);
            await productsPage.AddToCart(2);
            string cur_cart_value = await productsPage.GetCurCartValue();
            Assert.That(cur_cart_value, Is.EqualTo("3"));
            await cartPage.GotoCart();
            await cartPage.CheckOut();
            await checkoutPage.FillInfo("John", "Doe", "874857");
            await page.ClickAsync("#continue");
            await page.ClickAsync("#finish");
            Assert.That(await page.InnerTextAsync("h2.complete-header"), Is.EqualTo("Thank you for your order!"));

        }
        [Test]
        public async Task CartAbandonment()
        {
            await loginPage.PerformLogin("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            await productsPage.AddToCart(0);
            await productsPage.AddToCart(1);
            string cur_cart_value = await productsPage.GetCurCartValue();
            Assert.That(cur_cart_value, Is.EqualTo("2"));
            await cartPage.GotoCart();
            await cartPage.RemoveItem(0);
            await cartPage.RemoveItem(0);
            cur_cart_value = await productsPage.GetCurCartValue();
            Assert.That(cur_cart_value, Is.EqualTo(""));
        }
        [TearDown]
        public async Task Teardown()
        {
            await browser.CloseAsync();
            playwright.Dispose();
            watch.Stop();
            Console.WriteLine($"[Playwright] Execution Time : {watch.ElapsedMilliseconds} ms");
        }
    }
}