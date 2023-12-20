using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.PageObjects;
using SauceDemo.Tests;
using System.Xml.Linq;
using FluentAssertions;
using OpenQA.Selenium.DevTools.V118.Page;

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

namespace SauceDemo.Tests
{
    public class ShoppingTests<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private ProductsPage _ProductsPage { get; set; }
        private HomePage _HomePage { get; set; }
        private CheckoutPage1 _CheckoutPage1 { get; set; }
        private CheckoutPage2 _CheckoutPage2 { get; set; }

        private static IEnumerable<TestCaseData> _Products()
        {
            yield return new TestCaseData(new List<string> { "Onesie" });
            yield return new TestCaseData(new List<string> { "Onesie", "Backpack" });
        }

        [Test, TestCaseSource("_Products")]
        public void AddProducts(List<string> products)
        {
            _ProductsPage = new ProductsPage(Driver);
            _HomePage = new HomePage(Driver);            

            _HomePage.BypassLogin();
            _ProductsPage.gotoProductsPage();            
            int cnt = 0;
            foreach (var product in products)
            {
                _ProductsPage.AddToCart(product);
                cnt++;
            }
            _ProductsPage.GetNumberOfItemsInCart().Should().Be(cnt);

            //remove products to reset state
            _ProductsPage.RemoveAllFromCart(products);
        }

        [Test]
        public void EnterCheckoutInfo_HappyPath()
        {
            _HomePage = new HomePage(Driver);
            _ProductsPage = new ProductsPage(Driver);
            _CheckoutPage1 = new CheckoutPage1(Driver);

            _HomePage.BypassLogin();
            _ProductsPage.InjectProductsIntoCart();
            _CheckoutPage1.gotoCheckoutPage1();
            _CheckoutPage1.EnterInfo("Chris", "Bouy", "12345");
        }

        [Test]
        public void ConfirmCheckoutInfo_HappyPath()
        {
            _HomePage = new HomePage(Driver);
            _ProductsPage = new ProductsPage(Driver);
            _CheckoutPage2 = new CheckoutPage2(Driver);

            _HomePage.BypassLogin(); 
            _ProductsPage.InjectProductsIntoCart();
            _CheckoutPage2.gotoCheckoutPage2();
        }
    }
}