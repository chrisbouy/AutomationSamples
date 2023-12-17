using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.PageObjects;
using SauceDemo.Tests;
using System.Xml.Linq;
using FluentAssertions;

namespace SauceDemo.Tests
{
    public class ShoppingTests<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private ProductsPage _ProductsPage { get; set; }
        private HomePage _HomePage { get; set; }
        [TestCase]
        public void AddProducts()
        {
            _ProductsPage = new ProductsPage(Driver); 
            _HomePage = new HomePage(Driver);
            _HomePage.Goto("");
            _HomePage.BypassLogin();
            _ProductsPage.gotoProductsPage();
            var original_count = _ProductsPage.GetNumberOfItemsInCart();
            _ProductsPage.AddToCart("Onesie");
            //assert cart icon increased by one
            var new_count = _ProductsPage.GetNumberOfItemsInCart();
            new_count.Should().Be(++original_count);


            //assert item's 'add' button now says 'remove'


        }

        public void RemoveProducts()
        {
        }
    }
}