using System;
using System.Collections.Generic;
using Deblue.ObservingSystem;

namespace Deblue.Stats
{
    public struct LimitStatBindings<TMaxValue, TStat>
        where TMaxValue : Enum where TStat : Enum
    {
        public ModifiableStatsStorage<TMaxValue> ModifiableStats;
        public LimitedStatsStorage<TStat> LimitedStats;
        public TMaxValue MaxStatValueId;
        public TStat StatId;

        public static void SubscribeStatLimit(LimitStatBindings<TMaxValue, TStat> bindings, List<IObserver> observers = null)
        {
            var maxStatValue = bindings.ModifiableStats.GetStatProperty(bindings.MaxStatValueId);
            maxStatValue.SubscribeOnChanging(SetUpperLimit, observers);
            bindings.LimitedStats.SetUpperLimit(bindings.StatId, maxStatValue.Value);

            void SetUpperLimit(LimitedPropertyChanged<float> context)
            {
                bindings.LimitedStats.SetUpperLimit(bindings.StatId, context.NewValue);
            }
        }
    }
}