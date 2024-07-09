using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SeleniumAutomation.PageObjects
{
    public class BasePage
    {
        public IWebDriver _driver;
        private string _baseUrl => ConfigurationSettingsUtility.BaseUrl;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string URL
        {
            get
            {
                try
                {
                    return _driver.Url.ToLower();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        public string PageTitle
        {
            get
            {
                try
                {
                    return _driver.Title.ToLower();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        public void Goto(string url, bool useBaseUrl = true)
        {
            string navigateUrl = useBaseUrl ? _baseUrl + "/" + url : url;
            _driver.Navigate().GoToUrl(navigateUrl);
        }

        protected void WaitForJS()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return (document.readyState == 'complete' && jQuery.active == 0)"));
        }
    }
}
