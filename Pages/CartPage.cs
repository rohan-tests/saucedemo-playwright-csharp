using Microsoft.Playwright;

namespace Pages.CartPage
{
    public class CartPage
    {
        private readonly IPage page = null!;
        public CartPage(IPage page) => this.page = page;
        private ILocator Cart_button => page.Locator("a.shopping_cart_link");
        private ILocator Item_name => page.Locator(".inventory_item_name");
        private ILocator Menu_button => page.Locator("#react-burger-menu-btn");
        private ILocator Logout_button => page.Locator("#logout_sidebar_link");
        private ILocator Checkout_button => page.Locator("#checkout");

        public async Task GotoCart()
        {
            await Cart_button.ClickAsync();
        }
        public async Task RemoveItem(int i)
        {
            var remove_buttons = await page.QuerySelectorAllAsync("//button[text()='Remove']");
            await remove_buttons[i].ClickAsync();
        }
        public async Task CheckOut()
        {
            //manual delay added for testing
            // await page.EvaluateAsync(@"var e1 = document.querySelector('#checkout');
            //  if(e1){
            //  e1.remove();
            //  setTimeout(()=>{
            //  document.querySelector('.cart_footer').appendChild(e1);
            //  }, 3000);
            //  }"); 
            await Checkout_button.ClickAsync();
        }
        public async Task<string> GetProductNameFromCart()
        {
            return await Item_name.InnerTextAsync();
        }
        public async Task LogOut()
        {
            await Menu_button.ClickAsync();
            await Logout_button.ClickAsync();
        }
    }
}