using Deblue.Story.Steps;
using FluentAssertions;
using NUnit.Framework;

namespace Deblue.Story.Tests
{
    public class StoryTests
    {
        [Test]
        public void WhenStepConditionIsDone_AndNextStepIsStepTwo_ThenStepOneChangedToStepTwo()
        {
            Setup.Refresh();
            
            //Arrange
            Storyteller storyteller = Setup.Storyteller;

            //Act
            var conditions = Setup.StepOne.Conditions;
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].StepCondition.IsDone(true);
            }

            storyteller.FixedTick();

            //Assert
            storyteller.CurrentStep().Should().Be(Setup.StepTwo);
        }
    }
}