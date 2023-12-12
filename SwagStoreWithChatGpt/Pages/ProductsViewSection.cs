using OpenQA.Selenium;

namespace SwagStoreWithChatGpt.Pages
{
    public class ProductsViewSection : WebPage
    {

        private IList<IWebElement> ProductListItems => WaitAndFindElements(By.CssSelector(".inventory_list .inventory_item"));
        private By BtnAddToCart => By.CssSelector("[data-test^='add-to-cart']");

        public ProductsViewSection(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Gets a product using the name of a product
        /// </summary>
        /// <param name="searchPhrase"> The name of the product to search for. </param>
        /// <returns> The first item that matches the name of the product. </returns>
        private IWebElement? GetSingleProduct(string searchPhrase)
        {
            return ProductListItems.FirstOrDefault(item => item.Text.Contains(searchPhrase));
        }

        /// <summary>
        /// Finds the 'add to cart' button for the given parent element.
        /// </summary>
        /// <param name="parentElement"> The parent element containing the 'add to cart' button. </param>
        /// <returns> The 'add to cart' button element. </returns>
        private IWebElement GetAddToCartBtn(IWebElement parentElement)
        {
            return parentElement.FindElement(BtnAddToCart);
        }

      
        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        /// <param name="searchPhrase">The name of the product to add to the cart.</param>
        public void AddProductToCart(string searchPhrase)
        {
            IWebElement? product = GetSingleProduct(searchPhrase);
            if (product != null)
            {
                try
                {
                    GetAddToCartBtn(product).Click();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to add product '{searchPhrase}' to cart: {ex.Message}", ex);
                }
            }
            else
            {
                throw new InvalidOperationException($"Product '{searchPhrase}' not found.");
            }
        }

    }
}

