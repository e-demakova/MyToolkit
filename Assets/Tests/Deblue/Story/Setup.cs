using System.Reflection;
using Deblue.Story.Steps;
using UnityEngine;

namespace Deblue.Story.Tests
{
    internal class TestConditionSO : ChangeStepConditionSO
    {
        protected override void MyDeInit()
        {
        }
    }

    internal static class Setup
    {
        private static Storyteller _storyteller;
        private static StepSO _stepTwo;
        private static StepSO _stepOne;
        private static ChangeStepConditionSO _condition;

        public static Storyteller Storyteller => _storyteller ??= CreateStoryteller();
        public static StepSO StepOne => _stepOne ??= CreateStepOne();
        public static StepSO StepTwo => _stepTwo ??= ScriptableObject.CreateInstance<StepSO>();
        public static ChangeStepConditionSO Condition => _condition ??= ScriptableObject.CreateInstance<TestConditionSO>();


        public static void Refresh()
        {
            _storyteller = null;
            _stepOne = null;
            _stepTwo = null;
            _condition = null;
        }

        public static StepSO CurrentStep(this Storyteller storyteller)
        {
            var field = typeof(Storyteller).GetField("_currentStep", BindingFlags.Instance | BindingFlags.NonPublic);
            return (StepSO) field.GetValue(storyteller);
        }

        public static void IsDone(this ChangeStepConditionSO condition, bool value)
        {
            var property = typeof(ChangeStepConditionSO).GetProperty("IsDone", BindingFlags.Public | BindingFlags.Instance);
            property.GetSetMethod(true).Invoke(condition, new object[] {value});
        }

        private static Storyteller CreateStoryteller()
        {
            return new Storyteller(DialogSystem.Tests.Setup.CreateDialogRequester(), StepOne);
        }


        private static StepSO CreateStepOne()
        {
            var step = ScriptableObject.CreateInstance<StepSO>();
            step.Conditions = new[]
            {
                new StepSO.Condition()
                {
                    StepCondition = Condition,
                    NextStep = StepTwo,
                }
            };

            return step;
        }
    }
}