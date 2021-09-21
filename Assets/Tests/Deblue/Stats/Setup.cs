using System;
using System.Collections.Generic;
using Deblue.ObservingSystem;
using UnityEngine;

namespace Deblue.Stats.Tests
{
    public enum TestLimitedStatId
    {
        Stat1,
        Stat2,
        Stat3
    }

    public enum TestModifiableStatId
    {
        Stat1,
        Stat2,
        Stat3
    }

    public static class Setup
    {
        private static ModifiableStatsStorage<TestModifiableStatId> _testModifiableStatStorage;
        private static LimitedStatsStorage<TestLimitedStatId> _testLimitedStatStorage;
        private static List<IObserver> _observers = new List<IObserver>(3);

        public static ModifiableStatsStorage<TestModifiableStatId> TestModifiableStatStorage => _testModifiableStatStorage ??= ModifiableStatsStorage(3, 0);
        public static LimitedStatsStorage<TestLimitedStatId> TestLimitedStatStorage => _testLimitedStatStorage ??= LimitedStatsStorage(3, 0);

        public static LimitStatBindings<TestModifiableStatId, TestLimitedStatId> Stat1Binding => new LimitStatBindings<TestModifiableStatId, TestLimitedStatId>()
        {
            ModifiableStats = TestModifiableStatStorage,
            LimitedStats = TestLimitedStatStorage,
            StatId = TestLimitedStatId.Stat1,
            MaxStatValueId = TestModifiableStatId.Stat1
        };
        
        public static LimitStatBindings<TestModifiableStatId, TestLimitedStatId> Stat2Binding => new LimitStatBindings<TestModifiableStatId, TestLimitedStatId>()
        {
            ModifiableStats = TestModifiableStatStorage,
            LimitedStats = TestLimitedStatStorage,
            StatId = TestLimitedStatId.Stat2,
            MaxStatValueId = TestModifiableStatId.Stat2
        };
        
        public static LimitStatBindings<TestModifiableStatId, TestLimitedStatId> Stat3Binding => new LimitStatBindings<TestModifiableStatId, TestLimitedStatId>()
        {
            ModifiableStats = TestModifiableStatStorage,
            LimitedStats = TestLimitedStatStorage,
            StatId = TestLimitedStatId.Stat3,
            MaxStatValueId = TestModifiableStatId.Stat3
        };

        public static void Refresh()
        {
            _testModifiableStatStorage = null;
            _testLimitedStatStorage = null;
            _observers.ClearObservers();
        }

        public static void BindProperties(LimitStatBindings<TestModifiableStatId, TestLimitedStatId> bindings)
        {
            LimitStatBindings<TestModifiableStatId, TestLimitedStatId>.SubscribeStatLimit(bindings, _observers);
        }
        
        private static ModifiableStatsStorage<TestModifiableStatId> ModifiableStatsStorage(int statsCount = 0, float value = 0)
        {
            var statsStorage = StatsStorage(new ModifiableStatsStorage<TestModifiableStatId>(), statsCount, value);
            return (ModifiableStatsStorage<TestModifiableStatId>) statsStorage;
        }

        private static LimitedStatsStorage<TestLimitedStatId> LimitedStatsStorage(int statsCount = 0, float value = 0)
        {
            var statsStorage = StatsStorage(new LimitedStatsStorage<TestLimitedStatId>(), statsCount, value);
            return (LimitedStatsStorage<TestLimitedStatId>) statsStorage;
        }

        private static BaseStatsStorage<TEnum> StatsStorage<TEnum>(BaseStatsStorage<TEnum> statsStorage, int statsCount = 0, float value = 0) where TEnum : Enum
        {
            var allStats = Enum.GetValues(typeof(TEnum));
            var allStatsLength = allStats.Length;

            var length = statsCount < allStatsLength ? statsCount : allStatsLength;

            for (int i = 0; i < length; i++)
            {
                statsStorage.AddStat((TEnum) allStats.GetValue(i), value);
            }

            return statsStorage;
        }
    }
}