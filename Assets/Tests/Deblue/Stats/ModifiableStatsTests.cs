using NUnit.Framework;
using FluentAssertions;

namespace Deblue.Stats.Tests
{
    public class ModifiableStatsTests
    {
        private TestModifiableStatId _id = TestModifiableStatId.Stat1;

        [Test]
        public void WhenAddModifierForStat_AndStatModifiersCountIs0_ThenModifiersCountShouldBe1()
        {
            Setup.Refresh();
            //Arrange
            var stats = Setup.TestModifiableStatStorage;
            //Act
            stats.AddModifier(new AdditionStatModifier<TestModifiableStatId>(_id));
            //Assert
            new {modifiersCount = stats.GetModifiers(_id).Length}.Should().Be(new {modifiersCount = 1});
        }

        [Test]
        public void WhenAddAdditionOn2AndMultiplierOn3Modifiers_AndStartValueIs0_ThenResultShouldBe6()
        {
            Setup.Refresh();
            //Arrange
            var stats = Setup.TestModifiableStatStorage;
            //Act
            stats.AddModifier(new AdditionStatModifier<TestModifiableStatId>(_id, 2));
            stats.AddModifier(new MultipliengStatModifier<TestModifiableStatId>(_id, 3));
            //Assert
            new {statValue = (int) stats.GetStatValue(_id)}.Should().Be(new {statValue = (int) 6});
        }

        [Test]
        public void WhenAddMultiplierOn3AndAdditionOn2Modifiers_AndStartValueIs0_ThenResultShouldBe6()
        {
            Setup.Refresh();
            //Arrange
            var stats = Setup.TestModifiableStatStorage;
            //Act
            stats.AddModifier(new MultipliengStatModifier<TestModifiableStatId>(_id, 3));
            stats.AddModifier(new AdditionStatModifier<TestModifiableStatId>(_id, 2));
            //Assert
            new {statValue = (int) stats.GetStatValue(_id)}.Should().Be(new {statValue = (int) 6});
        }

        [Test]
        public void WhenAddMultiplierOn3AndAdditionOn2Modifiers_AndStartValueIs1_ThenResultShouldBe9()
        {
            Setup.Refresh();
            //Arrange
            var stats = Setup.TestModifiableStatStorage;
            stats.SetAmount(_id, 1);
            //Act
            stats.AddModifier(new MultipliengStatModifier<TestModifiableStatId>(_id, 3));
            stats.AddModifier(new AdditionStatModifier<TestModifiableStatId>(_id, 2));
            //Assert
            new {statValue = (int) stats.GetStatValue(_id)}.Should().Be(new {statValue = (int) 9});
        }

        [Test]
        public void WhenAddMultiplierOn3AndAdditionOn2ModifiersAndAdded2ToValue_AndStartValueIs1_ThenResultShouldBe15()
        {
            Setup.Refresh();
            //Arrange
            var stats = Setup.TestModifiableStatStorage;
            stats.SetAmount(_id, 1);
            //Act
            stats.AddModifier(new MultipliengStatModifier<TestModifiableStatId>(_id, 3));
            stats.AddModifier(new AdditionStatModifier<TestModifiableStatId>(_id, 2));
            stats.ChangeAmount(_id, 2);
            //Assert
            new {statValue = (int) stats.GetStatValue(_id)}.Should().Be(new {statValue = (int) 15});
        }

        [Test]
        public void WhenAddMultiplierOn3AndAdditionOn2ModifiersAndValueChangeTo2_AndStartValueIs1_ThenResultShouldBe12()
        {
            Setup.Refresh();
            //Arrange
            var stats = Setup.TestModifiableStatStorage;
            stats.SetAmount(_id, 1);
            //Act
            stats.AddModifier(new MultipliengStatModifier<TestModifiableStatId>(_id, 3));
            stats.AddModifier(new AdditionStatModifier<TestModifiableStatId>(_id, 2));
            stats.SetAmount(_id, 2);
            //Assert
            new {statValue = (int) stats.GetStatValue(_id)}.Should().Be(new {statValue = (int) 12});
        }
    }
}