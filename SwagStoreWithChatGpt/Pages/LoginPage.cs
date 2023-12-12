using OpenQA.Selenium;

namespace SwagStoreWithChatGpt.Pages
{

    public class LoginPage : WebPage
    {
        private IWebElement UsernameField => WaitAndFindElement(By.CssSelector("[data-test=username]"));
        private IWebElement PasswordField => WaitAndFindElement(By.CssSelector("[data-test=password]"));
        private IWebElement BtnLogin => WaitAndFindElement(By.CssSelector("#login-button"));
        private IWebElement ErrorMessage => WaitAndFindElement(By.CssSelector("[data-test='error']"));

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Attempts to log in using the provided username and password.
        /// </summary>
        /// <param name="username">The username for login.</param>
        /// <param name="password">The password for login.</param>
        public void Login(string username, string password)
        {
            try
            {
                UsernameField.SendKeys(username);
                PasswordField.SendKeys(password);
                BtnLogin.Click();
            }
            catch (Exception ex)
            {
                // Handle exceptions related to element interactions here
                // Logging the exception can be helpful
                throw new Exception($"Login failed: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves the error message if present.
        /// </summary>
        /// <returns>The error message element.</returns>
        public IWebElement GetErrorMessage()
        {
            return ErrorMessage;
        }
    }
}

