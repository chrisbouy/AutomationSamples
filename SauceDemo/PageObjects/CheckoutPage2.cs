using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.PageObjects
{
    public class CheckoutPage2 : BasePage
    {

        public CheckoutPage2(IWebDriver driver) : base(driver)
        {
        }

        // I'm doing it this way to show a quick way to get all the inputs as webelements.
        private IWebElement BUT_finish => HelperMethods.FindElementWithWait_Clickable(_driver, By.XPath("//*[@data-test='finish']"), TimeSpan.FromSeconds(10));

        //I'm then going to populate the inputs from a list of data in the test
        public void ConfirmOrderInfo()
        {
            BUT_finish.Click();
        }

        public void gotoCheckoutPage2()
        {
            Goto("checkout-step-two.html");
        }
    }
}
