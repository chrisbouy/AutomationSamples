using NUnit.Framework;
using OpenQA.Selenium;
using System.Xml.Linq;
using FluentAssertions;
using OpenQA.Selenium.DevTools.V118.Page;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation.PageObjects;

/*-----------------ATOMIC TESTS EXAMPLE---------------
INSTEAD OF ONE TEST THAT GOES THROUGH THE ENTIRE CHECKOUT PROCESS,
I'VE BROKEN IT UP INTO 3 TESTS.  THESE TESTS GO STRAIGHT TO THE
PAGE BEING TESTED WITH ALL NEEDED INFORMATION INJECTED WITH JAVASCRIPT.
THERE ARE MANY ADVANTAGES TO DOING IT THIS WAY. 
-SINGLE POINT OF FAILURE  (LESS MOVING PARTS)
-FAIL FAST  (MORE IMMEDIATE FEEDBACK)
-RUN FAST  (WHEN PARALLELIZED)
-DECREASE FLAKINESS (decreases the number of possible breaking points)
*/

namespace SeleniumAutomation.Tests
{
    public class ShoppingTests<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private ProductsPage _ProductsPage { get; set; }
        private HomePage _HomePage { get; set; }
        private CheckoutPage1 _CheckoutPage1 { get; set; }
        private CheckoutPage2 _CheckoutPage2 { get; set; }
        private CheckoutCompletePage _CheckoutCompletePage { get; set; }

        private static IEnumerable<TestCaseData> _Products()
        {
            yield return new TestCaseData(new List<string> { "Onesie" });
            yield return new TestCaseData(new List<string> { "Onesie", "Backpack" });
        }

        /*
         * Given: user is on products page
         * When: user adds items to cart
         * Then: counter icon reflects number of items being purchased
         */
        [Test, TestCaseSource("_Products")]
        public void AddProducts(List<string> products)
        {
            _ProductsPage = new ProductsPage(Driver);
            _HomePage = new HomePage(Driver);

            _HomePage.BypassLoginWithCookie();
            _ProductsPage.gotoProductsPage();
            _ProductsPage.AddAllToCart(products);
            //assert all products added
            _ProductsPage.GetNumberOfItemsInCart().Should().Be(products.Count);
            //remove products to reset state
            _ProductsPage.RemoveAllFromCart(products);
        }

        /*
         * Given: user is on checkout page 1
         * When: user enters form info and clicks continue
         * Then: user is directed to checkout page 2
         */
        [Test]
        public void EnterCheckoutInfo_HappyPath()
        {
            _HomePage = new HomePage(Driver);
            _ProductsPage = new ProductsPage(Driver);
            _CheckoutPage1 = new CheckoutPage1(Driver);

            _HomePage.BypassLoginWithCookie();
            _ProductsPage.InjectProductsIntoCartWithJavaScript();
            _CheckoutPage1.gotoCheckoutPage1();
            _CheckoutPage1.EnterInfo("Chris", "Bouy", "12345");
            //assert next page is loaded
            Assert.That(Driver.Url.Contains("checkout-step-two.html"));
        }

        /*
         * Given: user is on checkout page 2
         * When: user verifies info and clicks finish
         * 
         * Then: a 'success' message is displayed
         */
        [Test]
        public void ConfirmCheckoutInfo_HappyPath()
        {
            _HomePage = new HomePage(Driver);
            _ProductsPage = new ProductsPage(Driver);
            _CheckoutPage2 = new CheckoutPage2(Driver);
            _CheckoutCompletePage = new CheckoutCompletePage(Driver);

            _HomePage.BypassLoginWithCookie();
            _ProductsPage.InjectProductsIntoCartWithJavaScript();
            _CheckoutPage2.gotoCheckoutPage2();
            //todo:assert everything looks good
            _CheckoutPage2.ConfirmOrderInfo();
            //assert success message
            _CheckoutCompletePage.GetMessage().Should().Contain("Your order has been dispatched");
        }
    }
}