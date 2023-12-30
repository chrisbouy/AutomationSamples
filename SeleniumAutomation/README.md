	Selenium framework - This will be an explanation of my framework and why I've done things this
	way. 
	
	Locators - First, let's start with my locators.  I use a lambda expressions to define my
	findby's. This approach provides a way to lazily initialize or find elements when they're
	actually needed, rather than 	immediately when the class is instantiated. Doing this also 
	allows my find methods to be wrapped by helper 	methods that also define waits. This is an 
		example of a locator for a control in a page object:
	-----------------------------------------------------------------------------------------
	private IWebElement TBname => HelperMethods.FindElementWithWait_Clickable(_driver, By.Name("username"), System.TimeSpan.FromSeconds(10));
	-----------------------------------------------------------------------------------------
	This is the helper 'find' method:
	-----------------------------------------------------------------------------------------
	public static IWebElement FindElementWithWait_Clickable(IWebDriver webDriver, By by, TimeSpan timeOut)
	{
	    try
	    {
		WebDriverWait wait = new WebDriverWait(webDriver, timeOut);
		return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
	    }
	    catch(OpenQA.Selenium.NoSuchElementException)
	    {
		return null;
	    }
	}
	-----------------------------------------------------------------------------------------	
	TestBase - Tests are generic classes of type TWebDriver.  This type impliments IWebDriver.  
	TWebDriver must have a public parameterless constructor. In other words, it must be 
	possible to create an instance of TWebDriver using the new keyword. The idea is to create 
	a base class for tests that use Selenium WebDriver (IWebDriver), and allow the flexibility to 
	use different WebDriver implementations as long as they meet the constraints.
	-----------------------------------------------------------------------------------------
	    [TestFixture(typeof(ChromeDriver))]
	    [TestFixture(typeof(FirefoxDriver))]
	    public class TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
	-----------------------------------------------------------------------------------------  
	I then inherit from this base class
	-----------------------------------------------------------------------------------------
	public class TradingVolumeTests<TWebDriver> : TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
	-----------------------------------------------------------------------------------------
	Config - a config file is built dynamically from a json body in the output directory
	
	Cookies - cookies are saved to file and reloaded to bypass login. In the setup hook of the test
	base, cookies are loaded from this file.  
