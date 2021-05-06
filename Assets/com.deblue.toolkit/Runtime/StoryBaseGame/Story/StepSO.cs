using UnityEngine;

using Deblue.DialogSystem;

namespace Deblue.Story
{
    [CreateAssetMenu(fileName = "Step_New", menuName = "Story/Step")]
    public class StepSO : ScriptableObject
    {
        [System.Serializable]
        public struct Condition
        {
            public ChangeStepConditionSO StepCondition;
            public StepSO                NextStep;
        }

        public Condition[]               Conditions;
        public AvalibleCharacterOnStep[] AvalibleCharacters;

        [SerializeField, HideInInspector] private StepSO _nextStep;

        public bool IsDone { get; private set; }
        public StepSO NextStep => _nextStep;

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                Conditions[i].StepCondition.CheckCondition(deltaTime);
                if (Conditions[i].StepCondition.IsDone)
                {
                    IsDone = true;
                    _nextStep = Conditions[i].NextStep;
                    for (int j = 0; j < Conditions.Length; j++)
                    {
                        Conditions[j].StepCondition.DeInit();
                    }
                    return;
                }
            }
        }
    }
}