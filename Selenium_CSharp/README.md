Selenium Automation Framework (C#)  
This test project was created against https://www.saucedemo.com/, a testing website developed by Sauce Labs.  This is a Selenium-based test automation framework I built in C#. It demonstrates flexible, maintainable, and reusable test structures using page objects, lazy locators, cookie handling, and parameterized WebDriver support.  
  
🔧 Getting Started  
  
Prerequisites  
.NET SDK  
ChromeDriver or FirefoxDriver installed and on your system path  
  
Running the Tests  
Clone the repo:  
git clone https://github.com/chrisbouy/AutomationSamples.git  
cd AutomationSamples/SeleniumProject  
    To clone just this `Selenium_CSharp` folder without downloading the entire repository:
    ```bash
    git clone --filter=blob:none --no-checkout https://github.com/chrisbouy/AutomationSamples.git
    cd AutomationSamples
    git sparse-checkout init --cone
    git sparse-checkout set Selenium_CSharp

Restore packages:  
dotnet restore  
  
Run the tests:  
dotnet test  
  
✅ Features  
Generic TestBase: Easily run tests against different browsers using generics and [TestFixture] attributes.  
Lazy-loaded Locators: Elements are located only when needed, reducing flakiness and improving performance.  
Built-in Wait Helpers: Wrapped locator logic includes WebDriverWait for reliability.  
Persistent Cookies: Login cookies are saved and reloaded to bypass repeated authentication.  
Configurable via JSON: A dynamic config file is built from a JSON body in the output directory.  
  
  
  
🧠 Technical Overview  
🔹 Locators with Lambda Expressions  
This pattern defers element lookup until the element is actually needed:  
private IWebElement TBname => HelperMethods.FindElementWithWait_Clickable(_driver, By.Name("username"), TimeSpan.FromSeconds(10));
Wrapped in a helper method with wait logic:  
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
🔹 Reusable TestBase with Generic WebDriver  
Allows tests to be written once and run against multiple browsers:  
[TestFixture(typeof(ChromeDriver))]  
[TestFixture(typeof(FirefoxDriver))]  
public class TestBase<TWebDriver> where TWebDriver : IWebDriver, new()  
Example test class:  
public class TradingVolumeTests<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()  
  
📁 Project Structure  
SeleniumProject/  
├── Tests/  
├── Pages/  
├── Helpers/  
├── Config/  
├── cookies.json  
└── testsettings.json  
Let me know if you want me to update this on GitHub directly, or if you’d like help reworking another section (e.g. Cookies, Config, etc.).
