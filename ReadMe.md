About the Framework

* The Solution consists of 4 projects
	1. Ihs.Test.Framework.Test (Entry point for the UI test execution, contains features and stepDescription files)
	2. Ihs.Test.Framework.WebDriver (Contains all Driver implementation methods)
	3. Ihs.Test.Framework.Common (Contains all common methods, fields and / utilities shared amongst the rest of the projects)
	4. Ihs.Test.Framework.Pages (Contains all pages under test as well as page objects)
	
* Ihs.Test.Framework.WebDriver requires downloading & reference to / importing the following libraries
    1 Selenium.WebDriver 
	2. Selenium.Chrome.WebDriver
	3. DotNetSeleniumExtras.WaitHelp
	4. Selenium.Support
	5. NUnit
The webdriver project contains all the driver methods/ actions. 
In order to add a new Driver method in the Driver.cs, please make sure the method is also added to the interface class IDriver.cs otherwise the method won’t be accessible.

* Ihs.Test.Framework.Test requires downloading & reference to / importing the following libraries
    1. SpecFlow
	2. SpecFlow.NUnit
	3. SpecFlow.Plus.Excel (Not used, couldnot find a library that supports current Specflow version
	4. SpecFlow.Tools.MsBuild.Generation
	5. Microsoft.Extensions.Configuration
	6. Microsoft.Extensions.Configurartion.Json
	7. ExtentReports.Core Microsoft.Extensions.Config
	The Test project is the startup project/ solution entry point. It contains all the tests, test setup (Hooks) and test data source (in form of SpecFlow scenarios)
	I have used Visual studio as an IDE, tests can be executed from within the IDE, under test explorer window. Make sure NunitTestAdopter is installed

* Ihs.Test.Framework.Common
    This Project contains all methods and classes that could be shared across all the solution. The common Project is referenced by all other projects in the solution
	
* Ihs.Test.Framework.Pages
    This Project Contains all Page Objects and all Page actions. It is consumed by the Test project, yet it consumes the WebDriver actions. There are no driver actions /reference within the Test project.

 TEST EXECUTION:
 I have used Visual Studio native test explorer with the help of NunitTestAdopter libraries and Specflow.Tools.MsBuild.Generation (Generates tests from Gherkin scenarios)
 Also, for each scenario, the tests randomly runs on either chrome or firefox. see below
  Hooks1.cs
    Random rnd = new Random();
	_driver.StartBrowser(rnd.Next(1, 3) < 2 ? CommonConstants.Chrome : CommonConstants.Firefox, 
	path: Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Drivers");
	
 REPORTING:
 I have used ExtentReports library and configured the tests to generate test results and save them at (pathToYourDevDirectory + \Ihs.Test.Framework\Reports\yyyy\MMM\dd\time.