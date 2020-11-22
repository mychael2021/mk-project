using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using Ihs.Test.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Ihs.Test.Framework.Tests
{
    public class SpecflowHelper
    {
        public static void InsertScenarioSteps(ScenarioContext scenarioContext, ExtentTest Scenario)
        {
            var stepDefinition = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepName = scenarioContext.StepContext.StepInfo.Text;
            var performedActions = scenarioContext.ContainsKey(CommonConstants.Step) ? 
                scenarioContext[CommonConstants.Step] : string.Empty;
            stepName = stepName + performedActions;
            if (scenarioContext.TestError == null)
            {
                if (stepDefinition == "Given")
                {
                    Scenario.CreateNode<Given>(stepName, stepDefinition);
                }
                else if (stepDefinition == "When")
                {
                    Scenario.CreateNode<When>(stepName, stepDefinition);
                }
                else if (stepDefinition == "And")
                {
                    Scenario.CreateNode<And>(stepName, stepDefinition);
                }
                else if (stepDefinition == "Then")
                {
                    Scenario.CreateNode<Then>(stepName, stepDefinition);
                }
            }
            else
            {
                if (stepDefinition == "Given")
                {
                    Scenario.CreateNode<Given>(stepName, stepDefinition).Fail(scenarioContext.TestError.Message);
                }
                else if (stepDefinition == "When")
                {
                    Scenario.CreateNode<When>(stepName, stepDefinition).Fail(scenarioContext.TestError.Message);
                }
                else if (stepDefinition == "And")
                {
                    Scenario.CreateNode<And>(stepName, stepDefinition).Fail(scenarioContext.TestError.Message);
                }
                else if (stepDefinition == "Then")
                {
                    Scenario.CreateNode<Then>(stepName, stepDefinition).Fail(scenarioContext.TestError.Message);
                }
            }
        }
    }
}
