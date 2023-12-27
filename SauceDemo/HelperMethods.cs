using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumAutomation
{
    public static class HelperMethods
    {
        public static void HighlighElement(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].style.border='3px solid red'", element);
        }

        public static string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                //Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net5.0", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Defect_Screenshots\\");
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Defect_Screenshots\\" + screenShotName + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw e;
            }
            return localpath;
        }



        public static bool ElementIsPresent(IWebElement el)
        {
            try
            {
                return el.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement FindElementWithWait_Clickable(IWebDriver webDriver, By by, TimeSpan timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, timeOut);
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public static IWebElement FindElementWithWait_Viewable(IWebDriver webDriver, By by, TimeSpan timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, timeOut);
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        public static IWebElement FindElementWithWait_Exists(IWebDriver webDriver, By by, TimeSpan timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, timeOut);
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            }
            catch
            {
                return null;
            }
        }
        public static IWebElement FindElement(IWebDriver webDriver, By by)
        {
            try
            {
                return webDriver.FindElement(by);
            }
            catch
            {
                return null;
            }
        }

        public static ReadOnlyCollection<IWebElement> FindElements(IWebDriver webDriver, By by)
        {
            try
            {
                return webDriver.FindElements(by);
            }
            catch
            {
                return null;
            }
        }

        public static ReadOnlyCollection<IWebElement> FindElementsWithWait(IWebDriver webDriver, By by, TimeSpan timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, timeOut);
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch
            {
                return null;
            }
        }

        public static string GetElementValue(IWebElement el)
        {
            if (ElementIsPresent(el))
                return el.GetAttribute("value");

            return string.Empty;
        }

        public static string GetElementValue(SelectElement el) => GetElementValue(el.SelectedOption);

    }
}
