namespace Deblue.Interactive
{
    public interface IReactionObject
    {
        bool CanReact { get; }

        void React();
    }

    public interface ITakebleObject
    {
        string Id { get; }
        bool CanPut { get; }

        bool CanTake { get; }

        TakebleObject Take();

        void Put();
    }

    public interface ITakebleObjectContainer
    {
        bool CanReturn { get; }

        bool CanTake { get; }

        TakebleObject Take();

        void Return();
        bool TryReturn(ICanTakeObject taker);
    }
}
