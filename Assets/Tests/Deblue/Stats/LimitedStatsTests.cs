using NUnit.Framework;
using FluentAssertions;

namespace Deblue.Stats.Tests
{
    public class LimitedStatsTests
    {
        private TestLimitedStatId _id = TestLimitedStatId.Stat1;

        [Test]
        public void WhenModifiableStatChanged_AndLimitedStatUpperLimitSubscribeOnModifiableStat_ThenUpperLimitEqualModifiableStat()
        {
            Setup.Refresh();

            //Arrange
            var modifiableStats = Setup.TestModifiableStatStorage;
            var limitedStats = Setup.TestLimitedStatStorage;
            var bindings = Setup.Stat1Binding;
            Setup.BindProperties(bindings);

            var stat = limitedStats.GetStatProperty(TestLimitedStatId.Stat1);
            var upperLimitStat = modifiableStats.GetStatProperty(TestModifiableStatId.Stat1);

            //Act
            modifiableStats.ChangeAmount(TestModifiableStatId.Stat1, 3);

            //Assert
            new {upperLimit = stat.UpperLimit}.Should().Be(new {upperLimit = upperLimitStat.Value});
        }
        
        [Test]
        public void WhenLimitedStatUpperLimitBoundWithModifiableStat_AndUpperLimitNotEqualModifiableStat_ThenUpperLimitEqualModifiableStat()
        {
            Setup.Refresh();

            //Arrange
            var modifiableStats = Setup.TestModifiableStatStorage;
            var limitedStats = Setup.TestLimitedStatStorage;
            var bindings = Setup.Stat1Binding;
            limitedStats.SetUpperLimit(TestLimitedStatId.Stat1, 50);
            
            //Act
            Setup.BindProperties(bindings);

            var stat = limitedStats.GetStatProperty(TestLimitedStatId.Stat1);
            var upperLimitStat = modifiableStats.GetStatProperty(TestModifiableStatId.Stat1);

            //Assert
            new {upperLimit = stat.UpperLimit}.Should().Be(new {upperLimit = upperLimitStat.Value});
        }

        [Test]
        public void WhenLimitedStatSetUpperLimitTo100_AndPercentIsHalf1_ThenValueIsEqual50()
        {
            Setup.Refresh();

            //Arrange
            var limitedStats = Setup.TestLimitedStatStorage;
            var stat = limitedStats.GetStatProperty(TestLimitedStatId.Stat1);

            //Act
            limitedStats.SetUpperLimit(_id, 100f);
            limitedStats.SetPercent(_id, 0.5f);

            //Assert
            new {statValue = stat.Value}.Should().Be(new {statValue = 50f});
        }

        [Test]
        public void WhenSetStatValueTo50_AndUpperLimitIs100_ThenPercentIsHalfOne()
        {
            Setup.Refresh();

            //Arrange
            var limitedStats = Setup.TestLimitedStatStorage;

            //Act
            limitedStats.SetUpperLimit(_id, 100f);
            limitedStats.SetAmount(_id, 50);

            //Assert
            var percent = limitedStats.GetStatPercent(_id);
            new {statPercent = percent}.Should().Be(new {statPercent = 0.5f});
        }
        
        [Test]
        public void WhenSetStatValueTo50_AndUpperLimitIs100_ThenValueIs50()
        {
            Setup.Refresh();

            //Arrange
            var limitedStats = Setup.TestLimitedStatStorage;
            var stat = limitedStats.GetStatProperty(TestLimitedStatId.Stat1);

            //Act
            limitedStats.SetUpperLimit(_id, 100f);
            limitedStats.SetAmount(_id, 50);

            //Assert
            new {statValue = stat.Value}.Should().Be(new {statValue = 50f});
        }
        
        [Test]
        public void WhenSetStatValueTo50_AndUpperLimitIs10_ThenValueIs10()
        {
            Setup.Refresh();

            //Arrange
            var limitedStats = Setup.TestLimitedStatStorage;
            var stat = limitedStats.GetStatProperty(TestLimitedStatId.Stat1);

            //Act
            limitedStats.SetUpperLimit(_id, 10f);
            limitedStats.SetAmount(_id, 50);

            //Assert
            new {statValue = stat.Value}.Should().Be(new {statValue = 10f});
        }
        
        
        [Test]
        public void WhenAddToStatPercentHalf1_AndUpperLimitIs100ValueIs70_ThenPercentIs1()
        {
            Setup.Refresh();

            //Arrange
            var limitedStats = Setup.TestLimitedStatStorage;

            //Act
            limitedStats.SetUpperLimit(_id, 10f);
            limitedStats.SetAmount(_id, 70f);
            limitedStats.ChangePercent(_id, 0.5f);

            //Assert
            var percent = limitedStats.GetStatPercent(_id);
            new {statPercent = percent}.Should().Be(new {statPercent = 1f});
        }
    }
}