using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.PageObjects;
using SauceDemo.Tests;
using System.Xml.Linq;
using FluentAssertions;
using OpenQA.Selenium.DevTools.V118.Page;

namespace SauceDemo.Tests
{
    public class ShoppingTests<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private ProductsPage _ProductsPage { get; set; }
        private HomePage _HomePage { get; set; }

       private static IEnumerable<TestCaseData> _Products()
        {
            yield return new TestCaseData(new List<string> { "Onesie" });
            yield return new TestCaseData(new List<string> { "Onesie", "Backpack" }); 
        }

        [Test, TestCaseSource("_Products")]
        public void AddProducts(List<string> products)
        {
            int cnt = 0;
            _ProductsPage = new ProductsPage(Driver);
            _HomePage = new HomePage(Driver);
            //_HomePage.Goto("");
            _HomePage.BypassLogin();
            _ProductsPage.gotoProductsPage();
            var original_count = _ProductsPage.GetNumberOfItemsInCart();
            foreach (var product in products) 
            { 
                _ProductsPage.AddToCart(product);
                cnt++;
            }
            var new_count = _ProductsPage.GetNumberOfItemsInCart();
            new_count.Should().Be(cnt);
        }

        public void RemoveProducts()
        {
        }

        public void Checkout_HappyPath()
        {

        }
    }
}