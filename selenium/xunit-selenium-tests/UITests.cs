using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using OpenQA.Selenium.Firefox;

namespace xunit_selenium_tests;

public class UITests
{
    private readonly ITestOutputHelper output;

    public UITests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void Test1()
    {
        var chromeOptions = new ChromeOptions();
        // chromeOptions.BrowserVersion = "67";
        // chromeOptions.PlatformName = "Windows XP";
        IWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions);
        driver.Navigate().GoToUrl("http://www.google.com");
        Console.WriteLine("Prima della wait");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        output.WriteLine("Dopo la wait");
        driver.Quit();

    }
}