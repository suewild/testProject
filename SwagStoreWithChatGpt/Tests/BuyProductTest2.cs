using System;
using SwagStoreWithChatGpt.Pages;

namespace SwagStoreWithChatGpt.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class BuyProductTest2 : BaseTest
    {
        [Test]
        public void AddProductToCart2()
        {
            Given("I've logged in and I'm on the proucts page", () =>
            {
                driver!.Navigate().GoToUrl("https://www.saucedemo.com");
                LoginPage login = new(driver);
                login.Login("standard_user", "secret_sauce");
                Assert.That(driver.Url, Is.EqualTo("https://www.saucedemo.com/inventory.html"));
            });

            ProductsViewSection productsView = new(driver!);
            And("I've added a product to the shopping cart", () =>
            {
                productsView.AddProductToCart("Sauce Labs Fleece Jacket");
            });

            CartInformationSection cartInfo = new(driver!);
            And("The shopping cart badge is displayed with the added quantity", () =>
            {
                cartInfo = new CartInformationSection(driver!);
                Assert.That(cartInfo.GetCartBadge().Text, Does.Contain("1"));
            });

            And("I've opened the shopping cart", () =>
            {
                cartInfo.OpenShoppingCart();
                Assert.That(driver!.Url, Is.EqualTo("https://www.saucedemo.com/cart.html"));
            });

            And("I've checked out", () =>
            {
                cartInfo.CheckOut("Sauce Labs Fleece Jacket");
                Assert.That(driver!.Url, Is.EqualTo("https://www.saucedemo.com/checkout-step-one.html"));
            });

            CheckoutInformationSection checkoutInformation = new(driver!);
            And("I've filled in my information", () =>
            {
                checkoutInformation.FillInYourInformation("Sue", "Wild", "ST10 4JS").Continue();

            });

            CheckoutSummarySection checkoutSummary = new(driver!);
            When("I finish my order", () =>
            {
                checkoutSummary.ValidateCheckoutItemExists("Sauce Labs Fleece Jacket").FinishBtn.Click();
                Assert.That(driver!.Url, Is.EqualTo("https://www.saucedemo.com/checkout-complete.html"));
            });

            CheckoutCompleteSection checkoutComplete = new(driver!);
            Then("the order is complete", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(checkoutComplete.GetPageHeader().Text, Does.Contain("Checkout: Complete!"));
                    Assert.That(checkoutComplete.GetCheckoutCompleteContainer().Text, Does.Contain("Thank you for your order!"));
                });
            });
        }
    }
}

