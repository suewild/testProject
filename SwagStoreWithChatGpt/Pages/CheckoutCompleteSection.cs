using OpenQA.Selenium;

namespace SwagStoreWithChatGpt.Pages
{
    public class CheckoutCompleteSection : WebPage
	{
        private IWebElement PageHeader => WaitAndFindElement(By.CssSelector("#header_container"));
        private IWebElement CheckoutCompleteContainer => WaitAndFindElement(By.CssSelector("#checkout_complete_container"));

        public CheckoutCompleteSection(IWebDriver driver): base(driver)
		{
		}

        public IWebElement GetPageHeader()
        {
            return PageHeader;
        }

        public IWebElement GetCheckoutCompleteContainer()
        {
            return CheckoutCompleteContainer;
        }
    }
}

