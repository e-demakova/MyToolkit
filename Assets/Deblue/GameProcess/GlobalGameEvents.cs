using System;
using System.Collections.Generic;

using Deblue.ObservingSystem;

namespace Deblue.GameProcess
{
    public interface IGlobalGameEvents
    {
        IObserver SubscribeOnGameOver(Action<GameOver> action, List<IObserver> observers = null);
        IObserver SubscribeOnNewGame(Action<NewGame> action, List<IObserver> observers = null);
        (IObserver, IObserver) SubscribeOnNewSession(Action<NewGameSession> newAction, Action<EndGameSession> endAction);
        (IObserver, IObserver) SubscribeOnNewSession(Action<NewGameSession> newAction, List<IObserver> newObservers, Action<EndGameSession> endAction, List<IObserver> endObservers);

        void UnsubscribeOnGameOver(Action<GameOver> action);
        void UnsubscribeOnNewGame(Action<NewGame> action);
        void UnsubscribeOnNewSession(Action<NewGameSession> newAction, Action<EndGameSession> endAction);
    }

    public class GlobalGameEvents : EventSender, IGlobalGameEvents
    {
        #region Subscribing
        public IObserver SubscribeOnGameOver(Action<GameOver> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        public IObserver SubscribeOnNewGame(Action<NewGame> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        public (IObserver, IObserver) SubscribeOnNewSession(Action<NewGameSession> newAction, Action<EndGameSession> endAction)
        {
            var a = Subscribe(newAction);
            var b = Subscribe(endAction);
            return (a, b);
        }
        
        public (IObserver, IObserver) SubscribeOnNewSession(Action<NewGameSession> newAction, List<IObserver> newObservers, Action<EndGameSession> endAction, List<IObserver> endObservers)
        {
            var a = Subscribe(newAction, newObservers);
            var b = Subscribe(endAction, endObservers);
            return (a, b);
        }
        #endregion

        #region Unsubscribing
        public void UnsubscribeOnGameOver(Action<GameOver> action)
        {
            Unsubscribe(action);
        }

        public void UnsubscribeOnNewGame(Action<NewGame> action)
        {
            Unsubscribe(action);
        }

        public void UnsubscribeOnNewSession(Action<NewGameSession> newAction, Action<EndGameSession> endAction)
        {
            Unsubscribe(newAction);
            Unsubscribe(endAction);
        }
        #endregion
    }
}