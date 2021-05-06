using System.Collections.Generic;

using Deblue.InputSystem;

namespace Deblue.Interactive
{
    public interface ICanTakeObject
    {
        TakebleObject TakenObject { get; }
        bool IsHoldObject { get; }

        void ReturnObject();
    }

    class InteractiveObjectsInScene
    {
        private List<InteractiveObject> _nearObjects = new List<InteractiveObject>(7);
        private ICanTakeObject          _player;

        private void TryHilight(On_Button_Down context)
        {
            if (_nearObjects.Count == 0)
            {
                //TryPutObject();
                return;
            }

            TakebleObjectContainer objContainer;

            for (int i = 0; i < _nearObjects.Count; i++)
            {
                objContainer = _nearObjects[i] as TakebleObjectContainer;

                if (objContainer != null)
                {
                    if (objContainer.Can)
                    {
                    }
                }
            }

            TakebleObject takebleObj;
            IReactionObject reactObj;

            for (int i = 0; i < _nearObjects.Count; i++)
            {
                reactObj = _nearObjects[i] as IReactionObject;

                if (reactObj != null)
                {
                    if (reactObj.CanReact)
                    {
                        reactObj.React();
                        return;
                    }
                }
            }

            for (int i = 0; i < _nearObjects.Count; i++)
            {
                takebleObj = _nearObjects[i] as TakebleObject;
                objContainer = _nearObjects[i] as TakebleObjectContainer;

                TryPutObject();
                if (IsHoldObject)
                {
                    return;
                }

                if (objContainer != null)
                {
                    if (TryReciveObject(objContainer))
                    {
                        return;
                    }
                }
                else if (takebleObj != null)
                {
                    if (TryTakeObject(takebleObj))
                    {
                        _nearObjects.Remove(takebleObj);
                        return;
                    }
                }
            }
        }
}
