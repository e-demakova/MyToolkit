using UnityEngine;

namespace Deblue.Story
{
    [CreateAssetMenu(fileName = "ChangeStepCondition_Empty", menuName = "Story/Change Step Conditions/Empty")]
    public class ChangeStepConditionSO : ScriptableObject
    {
        [SerializeField] private StepSO _nextLine;

        public StepSO NextLine => _nextLine;
        public bool   IsDone { get; private set; }

        public void Init()
        {
            IsDone = false;
            MyInit();
        }

        protected virtual void MyInit()
        {
        }

        protected virtual void DeInit()
        {
        }

        public virtual void CheckCondition(float deltaTime)
        {
        }

        protected void OnDone()
        {
            IsDone = true;
            DeInit();
        }
    }
}