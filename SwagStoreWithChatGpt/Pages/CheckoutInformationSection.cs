using OpenQA.Selenium;

namespace SwagStoreWithChatGpt.Pages
{
    public class CheckoutInformationSection : WebPage
	{
        private IWebElement FirstNameField => WaitAndFindElement(By.CssSelector("[data-test='firstName']"));
        private IWebElement LastNameField => WaitAndFindElement(By.CssSelector("[data-test='lastName']"));
        private IWebElement ZipCodeField => WaitAndFindElement(By.CssSelector("[data-test='postalCode']"));
        private IWebElement ContinueBtn => WaitAndFindElement(By.CssSelector("[data-test='continue']"));

        public CheckoutInformationSection(IWebDriver driver): base(driver)
		{
		}

        /// <summary>
        /// Fills in the user's details and returns the CheckoutInformationSection instance for chaining.
        /// </summary>
        /// <param name="firstName"> The first name of the user. </param>
        /// <param name="lastName"> The surname of the user. </param>
        /// <param name="postCode"> The users's postcode </param>
        /// <returns> CheckoutInformationSection instance. </returns>
        public CheckoutInformationSection FillInYourInformation(string firstName, string lastName, string postCode)
        {
            try
            {
                FirstNameField.SendKeys(firstName);
                LastNameField.SendKeys(lastName);
                ZipCodeField.SendKeys(postCode);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to fill in user details: {ex.Message}", ex);
            }
            return this;
         
        }

        /// <summary>
        /// Clicks on continue button to proceed  with the checkout process
        /// </summary>
        public void Continue()
        {
            try
            {
                ContinueBtn.Click();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to proceed with checkout: {ex.Message}", ex);
            }
           
        }
    }
}

