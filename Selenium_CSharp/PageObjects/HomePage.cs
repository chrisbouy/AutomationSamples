using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SeleniumAutomation.PageObjects
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement TB_name => HelperMethods.FindElementWithWait_Clickable(_driver, By.Name("username"), TimeSpan.FromSeconds(10));
        private IWebElement TB_pw => HelperMethods.FindElementWithWait_Clickable(_driver, By.Name("password"), TimeSpan.FromSeconds(10));
        private IWebElement BUT_bill_pay => HelperMethods.FindElementWithWait_Clickable(_driver, By.XPath("//li[a[text()='Bill Pay']]"), TimeSpan.FromSeconds(10));
        private IWebElement BUT_register => HelperMethods.FindElementWithWait_Clickable(_driver, By.LinkText("Register"), TimeSpan.FromSeconds(10));

        public void gotoHomePage()
        {
            Goto("");
            Thread.Sleep(3000);
        }

        public void Login(string name, string pw)
        {
            TB_name.SendKeys(name);
            TB_pw.SendKeys(pw);
            TB_pw.Submit();
        }

        public void BypassLoginWithCookie()
        {
            Cookie ck = new Cookie("session-username", "standard_user");
            _driver.Manage().Cookies.AddCookie(ck);
        }
        public void GoToRegistrationPage()
        {
            BUT_register.Click();
        }
    }
}
