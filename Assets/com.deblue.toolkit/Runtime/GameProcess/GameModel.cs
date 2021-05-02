namespace Deblue.GameProcess
{
    public abstract class GameModel : PersistentMono<GameModel>
    {
        public static GlobalGameEvents _events = new GlobalGameEvents();
        public static IGlobalGameEvents Events => _events;
    }
}