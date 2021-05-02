using UnityEngine;

using Deblue.DialogSystem;

namespace Deblue.Story
{
    [CreateAssetMenu(fileName = "ChangeStepCondition_Empty", menuName = "Story/Story Step Line")]
    public class StepSO : ScriptableObject
    {
        public ChangeStepConditionSO[]   StepConditions;
        public AvalibleCharacterOnStep[] AvalibleCharacters; 

        [SerializeField] private StepSO _nextLine;

        public bool   IsDone { get; private set; }
        public StepSO NextStep => _nextLine;

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < StepConditions.Length; i++)
            {
                StepConditions[i].CheckCondition(deltaTime);
                if (StepConditions[i].IsDone)
                {
                    IsDone = true;
                    _nextLine = StepConditions[i].NextLine;
                    return;
                }
            }
        }
    }
}