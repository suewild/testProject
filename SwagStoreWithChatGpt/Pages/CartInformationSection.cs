using OpenQA.Selenium;

namespace SwagStoreWithChatGpt.Pages
{
    public class CartInformationSection : WebPage
    {
        private IWebElement ShoppingCartBadge => WaitAndFindElement(By.CssSelector("#shopping_cart_container .shopping_cart_badge"));
        private IWebElement ShoppingCartLink => WaitAndFindElement(By.CssSelector("#shopping_cart_container .shopping_cart_link"));
        private IList<IWebElement> CartListItems => WaitAndFindElements(By.CssSelector("#cart_contents_container .cart_list .cart_item"));
        private IWebElement CheckoutBtn => WaitAndFindElement(By.CssSelector("[data-test='checkout']"));

        public CartInformationSection(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Gets card item based on its name.
        /// </summary>
        /// <param name="searchPhrase"> The name of the cart item to find. </param>
        /// <returns> The matching cart item element, or null if not found. </returns>
        private IWebElement? GetCartItem(string searchPhrase)
        {
            var cartItem = CartListItems.FirstOrDefault(item => item.Text.Contains(searchPhrase));
            if(cartItem == null)
            {
                throw new InvalidOperationException($"Cart item '{searchPhrase}' not found");
            }
            return cartItem;
        }

        /// <summary>
        /// Gets ths badge on the shopping cart
        /// </summary>
        /// <returns> The shopping cart badge element. </returns>
        public IWebElement GetCartBadge()
        {
            return ShoppingCartBadge;
        }

        /// <summary>
        /// Clicks on the shopping cart link.
        /// </summary>
        public void OpenShoppingCart()
        {
            try
            {
                ShoppingCartLink.Click();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to open shopping cart: {ex.Message}", ex);
            }
            
        }

        /// <summary>
        /// Checks out a cart item by its name
        /// </summary>
        /// <param name="searchPhrase"> The name of the cart item. </param>
        public void CheckOut(string searchPhrase)
        {
            IWebElement? cartItem = GetCartItem(searchPhrase);
            if (cartItem != null)
            {
                try
                {
                    CheckoutBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to checkout product: '{searchPhrase}': {ex.Message}", ex);
                }
            }
            else
            {
                throw new InvalidOperationException($"Product '{searchPhrase}' not found in cart");
            }
        }

    }
}

