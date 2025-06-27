
using Microsoft.Playwright;

namespace Pages.ProductsPage
{
    public class ProductsPage
    {
        private readonly IPage page;
        public ProductsPage(IPage page)
        {
            this.page = page;
        }
        private ILocator Cart_value => page.Locator(".shopping_cart_badge");

        public async Task AddToCart(int i)
        {
            var add_to_cart_button = await page.QuerySelectorAllAsync("button.btn_inventory");
            await add_to_cart_button[i].ClickAsync();
            //await page.ClickAsync($"(//button[text()='Add to cart'])[{i}]");
        }

        public async Task<string> GetCurCartValue()
        {
            if (await Cart_value.IsVisibleAsync())
            {
                return await Cart_value.InnerTextAsync();
            }
            return "";
        }
    }
}