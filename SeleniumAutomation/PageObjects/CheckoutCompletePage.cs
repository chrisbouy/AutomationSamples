using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.PageObjects
{
    public class CheckoutCompletePage : BasePage
    {

        public CheckoutCompletePage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement LBL_message => HelperMethods.FindElementWithWait_Viewable(_driver, By.ClassName("complete-text"), TimeSpan.FromSeconds(10));

        public string GetMessage()
        {
            return LBL_message.Text;
        }


    }
}
