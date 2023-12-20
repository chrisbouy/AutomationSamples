using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.PageObjects
{
    public class CheckoutCompletePage : BasePage
    {

        public CheckoutCompletePage(IWebDriver driver) : base(driver)
        {
        }

        // I'm doing it this way to show a quick way to get all the inputs as webelements.
        private IWebElement LBL_message => HelperMethods.FindElementWithWait_Viewable(_driver, By.ClassName("complete-text"), System.TimeSpan.FromSeconds(10));

        //I'm then going to populate the inputs from a list of data in the test
        public string GetMessage()
        {
            return LBL_message.Text;
        }


    }
}
