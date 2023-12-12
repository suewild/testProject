using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SwagStoreWithChatGpt.Pages
{
    public class WebPage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        public WebPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Waits for and finds a web element, ensuring it's ready for interaction.
        /// </summary>
        /// <param name="bySelector">The selector used to find the element.</param>
        /// <returns>The found IWebElement.</returns>
        protected IWebElement WaitAndFindElement(By bySelector)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(bySelector));
        }

        /// <summary>
        /// Waits for and finds multiple web elements, ensuring they are ready for interaction.
        /// </summary>
        /// <param name="bySelector">The selector used to find the elements.</param>
        /// <returns>A list of found IWebElements.</returns>
        protected IList<IWebElement> WaitAndFindElements(By bySelector)
        {
            return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(bySelector));
        }
    }
}

