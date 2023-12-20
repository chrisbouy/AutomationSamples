using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.PageObjects
{
    public class ProductsPage : BasePage
    {

        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        private List<IWebElement> DIV_products => HelperMethods.FindElementsWithWait(_driver, By.CssSelector(".inventory_item"), System.TimeSpan.FromSeconds(10)).ToList();
        private IWebElement DIV_cartIcon => HelperMethods.FindElementWithWait_Clickable(_driver, By.ClassName("shopping_cart_link"), System.TimeSpan.FromSeconds(10));
        private List<IWebElement> DIV_cartProductCounter => DIV_cartIcon.FindElements(By.ClassName("shopping_cart_badge")).ToList();

        public void gotoProductsPage()
        {
            Goto("inventory.html");
        }

        public void AddToCart(string product)
        {
            foreach (var item in DIV_products)
            {
                var productName = item.FindElement(By.ClassName("inventory_item_name")).Text;
                if (productName.Contains(product))
                {
                    var addToCartButton = item.FindElement(By.CssSelector(".btn_inventory"));
                    addToCartButton.Click();
                }
            }
            //if found, click 'add'
        }
       
        public int AddAllToCart(List<string> products)
        {
            int cnt = 0;
            foreach (var p in products)
            {
                foreach (var item in DIV_products)
                {
                    var productName = item.FindElement(By.ClassName("inventory_item_name")).Text;
                    if (productName.Contains(p))
                    {
                        var addToCartButton = item.FindElement(By.CssSelector(".btn_inventory"));
                        addToCartButton.Click();
                        cnt++;
                    }
                }
            }
            return cnt;
        }
      
        public void RemoveAllFromCart(List<string> products)
        {
            foreach (var p in products)
            {
                foreach (var item in DIV_products)
                {
                    var productName = item.FindElement(By.ClassName("inventory_item_name")).Text;
                    if (productName.Contains(p))
                    {
                        var removeFromCartButton = item.FindElement(By.CssSelector(".btn_inventory"));
                        removeFromCartButton.Click();
                    }
                }
            }
        }

        public int GetNumberOfItemsInCart()
        {
            if (DIV_cartProductCounter.Count == 0)
                return 0;
            else
                return int.Parse(DIV_cartProductCounter[0].Text);
        }

        public void InjectProductsIntoCartWithJavaScript()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "window.sessionStorage.setItem('session-username', 'standard-user')");
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.localStorage.setItem('cart-contents', '[4]')");
            _driver.Navigate().Refresh();
            return;
        }
    }
}

