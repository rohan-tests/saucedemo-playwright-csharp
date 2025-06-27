using Microsoft.Playwright;

namespace Saucedemo.LoginPage
{
    public class LoginPage
    {
        private readonly IPage page = null!;     
        public LoginPage(IPage page)
        {
            this.page = page;
        }
        private ILocator Username_field => page.Locator("#user-name");
        private ILocator Password_field => page.Locator("#password");
        private ILocator Login_button => page.Locator("#login-button");

        public async Task NavigateToUrl(string url)
        {
            await page.GotoAsync(url);
        }

        public async Task FillLoginForm(string username , string password)
        {
            await Username_field.FillAsync(username);
            await Password_field.FillAsync(password);
            await Login_button.ClickAsync();
        }
        public async Task PerformLogin(string url , string username , string password)
        {
            await NavigateToUrl(url);
            await FillLoginForm(username, password);
        }
    }
}