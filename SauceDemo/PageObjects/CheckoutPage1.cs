using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.PageObjects
{
    public class CheckoutPage1 : BasePage
    {

        public CheckoutPage1(IWebDriver driver) : base(driver)
        {
        }

        // I'm doing it this way to show a quick way to get all the inputs as webelements.
        IList<IWebElement> inputElements => HelperMethods.FindElementsWithWait(_driver,By.ClassName("form_input"),System.TimeSpan.FromSeconds(10));
        private IWebElement BUT_continue => HelperMethods.FindElementWithWait_Clickable(_driver, By.XPath("//*[@data-test='continue']"), System.TimeSpan.FromSeconds(10));

        //I'm then going to populate the inputs from a list of data in the test
        public void EnterInfo(string f, string l, string zip)
        {
            List<string> inputValues = new List<string> { f, l, zip };
            for (int i = 0; i < Math.Min(inputElements.Count, inputValues.Count); i++)
                inputElements[i].SendKeys(inputValues[i]);
            BUT_continue.Click();
        }

        public void gotoCheckoutPage1()
        {
            Goto("checkout-step-one.html");
        }

    }
}
