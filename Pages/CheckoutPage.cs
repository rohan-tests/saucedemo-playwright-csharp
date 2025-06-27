using Microsoft.Playwright;

namespace Pages.CheckoutPage
{
    public class CheckoutPage
    {
        private readonly IPage page = null!;  
        public CheckoutPage(IPage page)
        {
            this.page = page;
        }
        private ILocator First_name => page.Locator("#first-name");
        private ILocator Last_name => page.Locator("#last-name");
        private ILocator Zip_code => page.Locator("#postal-code");

        public async Task FillInfo(string firstName, string lastName, string zipCode)
        {
            await First_name.FillAsync(firstName);
            await Last_name.FillAsync(lastName);
            await Zip_code.FillAsync(zipCode);
        }
        public async Task<string> GetItemNameOnCheckout()
        {
            return await page.InnerTextAsync(".inventory_item_name");
        }
    }
}