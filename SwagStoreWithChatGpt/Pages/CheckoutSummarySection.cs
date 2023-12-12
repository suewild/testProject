using OpenQA.Selenium;

namespace SwagStoreWithChatGpt.Pages
{
    public class CheckoutSummarySection : WebPage
	{
        public IWebElement FinishBtn => WaitAndFindElement(By.CssSelector("[data-test='finish']"));
        private IList<IWebElement> CheckoutItems => WaitAndFindElements(By.CssSelector("#checkout_summary_container .cart_list .cart_item"));

        public CheckoutSummarySection(IWebDriver driver): base(driver)
		{
		}

        /// <summary>
        /// Checks checkout item exists by name
        /// </summary>
        /// <param name="searchPhrase"> The name of the checkoed out item. </param>
        /// <returns> The checked out item element or null. </returns>
        public CheckoutSummarySection ValidateCheckoutItemExists(string searchPhrase)
        {
            var checkoutItem = CheckoutItems.FirstOrDefault(item => item.Text.Contains(searchPhrase));
            if (checkoutItem == null)
            {
                throw new InvalidOperationException($"Checkout item '{searchPhrase}' not found.");
            }
            return this;
        }

    }
}



