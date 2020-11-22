using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ihs.Test.Framework.Webdriver
{
    public class Driver : IDriver
    {
        private IWebDriver _browser;

        private string browserName;

        public IWebDriver Browser()
        {
            return this._browser;
        }
        public int DriverProcessId = 0;
        public void StartBrowser(string browser = "Chrome", string path = "")
        {
            if (!string.IsNullOrEmpty(path))
            {
                switch (browser.ToLower())
                {
                    case "firefox":
                        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(path, "geckodriver.exe");
                        service.Host = "::1";
                        service.HideCommandPromptWindow = true;
                        FirefoxOptions firefoxOptions = new FirefoxOptions
                        {
                            AcceptInsecureCertificates = true,
                        };

                        this._browser = new FirefoxDriver(service, firefoxOptions);
                        this._browser.Manage().Window.Maximize();
                        this.browserName = browser;
                        DriverProcessId = service.ProcessId;
                        break;
                    default:
                        var options = new ChromeOptions();
                        int trys = 0;
                        while (trys < 3 && this._browser == null)
                        {
                            try
                            {
                                var chromeDriverService = ChromeDriverService.CreateDefaultService(path);
                                chromeDriverService.HideCommandPromptWindow = true;
                                options.AddArgument("--start-maximized");
                                options.AddArgument("--disable-notifications");
                                this._browser = new ChromeDriver(chromeDriverService, options);
                                this.browserName = browser;
                                DriverProcessId = chromeDriverService.ProcessId;
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            System.Threading.Thread.Sleep(1000);
                        }

                        break;
                }
            }
        }

        public string BrowserName()
        {
            return this.browserName;
        }

        public void Click(string locator)
        {
            if (string.IsNullOrEmpty(locator))
            {
                throw new Exception("Element can not be null");
            }

            try
            {
                this.WaitForPageLoad();
                this.FindElementVisible(locator).Click();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClickAllVisible(string locator)
        {
            try
            {
                foreach (IWebElement e in this.GetAllWebElements(locator))
                {
                    if (e.Displayed)
                    {
                        e.Click();
                    }
                }
            }
            catch
            {
            }
        }

        public void ClickAllPresent(string locator)
        {
            try
            {
                foreach (IWebElement e in this.GetAllWebElements(locator))
                {
                    if(IsElementClickable(e))
                    {
                        e.Click();
                    }
                }
            }
            catch { }
        }

        public string GetUrl()
        {
            return Browser().Url;
        }

        public IWebElement FindElementVisible(string locator)
        {
            try
            {
                //// Added for Ie and Edge
                if (!string.IsNullOrEmpty(locator) && locator.Contains("//") && this.browserName.ToLower() != "chrome" && this.browserName.ToLower() != "firefox")
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed)
                        {
                            return el;
                        }
                    }

                    return null;
                }

                if (this.FindElement(By.Id(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Id(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.ClassName(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.CssSelector(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.LinkText(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Name(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.PartialLinkText(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Name(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return null;
        }

        public IWebElement FindElementVisible(IWebElement element)
        {
            try
            {
                if (element.Displayed && element.Enabled)
                {
                    return element;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public IWebElement FindElementEnabled(string locator)
        {
            try
            {
                if (this.FindElement(By.Id(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Id(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.ClassName(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.CssSelector(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.LinkText(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Name(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public IWebElement FindElementPresent(string locator)
        {
            try
            {
                if (this.FindElement(By.Id(locator)) != null)
                {
                    return this.FindElement(By.Id(locator));
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    return this.FindElement(By.XPath(locator));
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    this.FindElement(By.ClassName(locator));
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    this.FindElement(By.CssSelector(locator));
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    this.FindElement(By.LinkText(locator));
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    this.FindElement(By.Name(locator));
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<IWebElement> GetVisibleElements(string locator)
        {
            IList<IWebElement> elements = new List<IWebElement>();
            try
            {
                foreach (var e in this.GetAllWebElements(locator))
                {
                    if (e.Displayed)
                    {
                        elements.Add(e);
                    }
                }

                return elements;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return elements;
            }
        }

        public void MoveToElement(string locator)
        {
            Actions action = new Actions(this._browser);
            action.MoveToElement(this.FindElementVisible(locator));
            action.Perform();
        }

        public void MoveToElement(IWebElement el)
        {
            try
            {
                Actions action = new Actions(this._browser);
                action.MoveToElement(el);
                action.Perform();
            }
            catch { }
        }

        public void WaitForPageLoad(TimeSpan? timespan = null)
        {
            timespan = timespan == null ? TimeSpan.FromSeconds(60) : timespan;
            IWait<IWebDriver> wait = new WebDriverWait(this._browser, timespan.Value);
            wait.Until(a => ((IJavaScriptExecutor)this._browser).ExecuteScript("return document.readyState").Equals("complete"));
            this.WaitForJQueryLoad(timespan.Value);
        }

        public bool IsElementClickable(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(this.FindElementPresent(locator)));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool IsElementClickable(IWebElement element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool IsElementStale(IWebElement element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool IsPageSourcePresent()
        {
            try
            {
                return !string.IsNullOrEmpty(this._browser.PageSource);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public void Navigate(string url)
        {
            this._browser.Navigate().GoToUrl(url);
            this.WaitForPageLoad(TimeSpan.FromMinutes(1));
        }

        public bool IsElementVisible(string locator)
        {
            if (this.FindElementVisible(locator) != null)
            {
                return true;
            }

            return false;
        }

        public bool IsElementVisible(IWebElement element)
        {
            if (this.FindElementVisible(element) != null)
            {
                return true;
            }

            return false;
        }

        public bool IsElementPresent(string locator)
        {
            if (this.FindElementPresent(locator) != null)
            {
                return true;
            }

            return false;
        }

        public bool IsElementPresent(IWebElement element)
        {
            try
            {
                if (element.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }


        public void WaitForElementVisible(string locator, double timeInSeconds = 60)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (this.FindElementVisible(locator) == null && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail("Failed to find locator with locator expression " + locator + " after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForElementNotVisible(string locator, double timeInSeconds = 60)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementVisible(locator))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(locator + " was still visible after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForElementNotVisible(IWebElement element, double timeInSeconds = 60)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementVisible(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was still visible after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForElementPresent(string locator, double timeInSeconds = 60)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.FindElementPresent(locator) == null)
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(locator + " was still present after " + totalTime + " seconds");
                }
            }
        }
        public void WaitUntilElementTextChanges(string element, string expectedText, double timeInSeconds = 60)
        {
            bool isElementTextSet = false;
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (!isElementTextSet)
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail($"Could not find expected text in the element ( {element} ) after  { totalTime} seconds");
                }
                if (FindElementPresent(element)?.Text == expectedText)
                {
                    isElementTextSet = true;
                }
            }
        }
        public void Type(string locator, string text, bool attemptReEntry = true)
        {
            var element = this.FindElementVisible(locator);
            if (element != null)
            {
                element.Clear();
                if (this.browserName == "firefox")
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        element.SendKeys(text);
                    }
                    else
                    {
                        SendKeysFireFox(element, text);
                    }
                }
                else
                {
                    element.SendKeys(text);
                }

                if (attemptReEntry)
                {
                    VerifyInput(element, text);
                }
            }
            else
            {
                throw new Exception("Element " + locator + " is not found");
            }
        }

        public void SendKeyBoardKeys(string element, List<string> keys)
        {
            var el = this.FindElementPresent(element);
            Actions action = new Actions(this._browser);
            foreach (var key in keys)
            {
                action.SendKeys(el, key);
                action.Perform();
            }
        }

        public void SendKeyBoardKey(string element, string key)
        {
            var el = this.FindElementPresent(element);
            Actions action = new Actions(this._browser);
            action.SendKeys(el, key);
            action.Perform();
        }

        public void SendKeyBoardKey(string key)
        {
            Actions action = new Actions(this._browser);
            action.SendKeys(key);
            action.Perform();
        }

        public void SendKeyBoardKeys(string[] keys)
        {
            Actions action = new Actions(this._browser);
            foreach(var key in keys)
            {
                action.SendKeys(key);
            }
            
            action.Perform();
        }

        private void SendKeysFireFox(IWebElement element, string keys)
        {
            Actions action = new Actions(this._browser);
            action.SendKeys(element, keys);
            action.Perform();
        }

        private void VerifyInput(IWebElement element, string inputText)
        {
            int attempts = 0;
            while (element.GetAttribute("value") != inputText && attempts < 3)
            {
                element.Clear();
                if (this.browserName == "firefox")
                {
                    SendKeysFireFox(element, inputText);
                }
                else
                {
                    element.SendKeys(inputText);
                }
                WaitFor(0.5);
                attempts++;
            }
        }

        public void StopBrowser()
        {
            this.Browser().Dispose();
        }

        public bool IsTextPresent(string text)
        {
            var el = this.GetAllWebElements("//*[contains(text(),'" + text + "')]");
            if (el != null && el.Count > 0)
            {
                return true;
            }

            if (this.Browser().PageSource.Contains(text))
            {
                return true;
            }

            return false;
        }

        public bool IsTextVisible(string text)
        {
            try
            {
                var el = this.GetAllWebElements("//*[contains(text(),'" + text + "')]");
                if (el != null && el.Any(a => a.Displayed))
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        public void WaitForTextVisible(string text, double timeInSeconds = 60)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (!this.IsTextVisible(text) && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail(text + " was could not find visible text on page after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForTextNotVisible(string text, double timeInSeconds = 60)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (this.IsTextVisible(text) && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail(text + " was could not find visible text on page after " + totalTime + " seconds");
                }
            }
        }

        public string GetText(string locator)
        {
            WaitForJQueryLoad(TimeSpan.FromSeconds(30));
            var text = this.FindElementVisible(locator)?.Text;
            return text;
        }

        public string GetValue(string locator)
        {
            return FindElementPresent(locator)?.GetAttribute("value");
        }

        public void Dispose()
        {
            try
            {
                this._browser.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WaitFor(double timeInSec)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(timeInSec));
        }

        public void OpenNewTabInBrowser()
        {
            ((IJavaScriptExecutor)this._browser).ExecuteScript("window.open();");
            this._browser.SwitchTo().Window(this._browser.WindowHandles.Last());
        }

        public void WaitForElementClickable(string element, double timeInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (!this.IsElementClickable(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was not clickable after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForElementNotClickable(string element, double timeInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementClickable(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was clickable after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForElementNotClickable(IWebElement element, double timeInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementClickable(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was clickable after " + totalTime + " seconds");
                }
            }
        }

        private ICollection<IWebElement> GetAllWebElements(string locator)
        {
            ICollection<IWebElement> el = null;
            try
            {
                if (!string.IsNullOrEmpty(locator) && locator.Contains("//"))
                {
                    el = this.Browser().FindElements(By.XPath(locator));
                }
                else
                {
                    if (this.FindElement(By.Id(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.Id(locator));
                    }
                    else if (this.FindElement(By.XPath(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.XPath(locator));
                    }
                    else if (this.FindElement(By.Name(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.Name(locator));
                    }
                    else if (this.FindElement(By.LinkText(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.LinkText(locator));
                    }
                    else if (this.FindElement(By.TagName(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.TagName(locator));
                    }
                    else if (this.FindElement(By.ClassName(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.ClassName(locator));
                    }
                    else if (this.FindElement(By.CssSelector(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.CssSelector(locator));
                    }
                }

                return el;
            }
            catch
            {
                return el;
            }
        }

        private IWebElement FindElement(By by)
        {
            this._browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            try
            {
                return this._browser.FindElement(by);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private double ClockTime(TimeSpan timeSpan)
        {
            var ts2 = new TimeSpan(DateTime.Now.Ticks);
            var ts3 = timeSpan - ts2;
            return Math.Abs(ts3.TotalSeconds);
        }

        private void WaitForJQueryLoad(TimeSpan timespan)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)this._browser;
            var time = TimeSpan.FromSeconds(this.ClockTime(timespan));
            if ((bool)executor.ExecuteScript("return window.jQuery != undefined"))
            {
                while (!(bool)executor.ExecuteScript("return jQuery.active == 0"))
                {
                    if (time > timespan)
                    {
                        break;
                    }
                }
            }

            //// make sure page source is present
            while (!this.IsPageSourcePresent())
            {
                if (time > timespan)
                {
                    break;
                }
            }
        }
    }
}
