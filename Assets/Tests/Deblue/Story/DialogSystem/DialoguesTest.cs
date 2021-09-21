using FluentAssertions;
using NUnit.Framework;

namespace Deblue.Story.DialogSystem.Tests
{
    public class DialoguesTest
    {
        [Test]
        public void WhenDialogRequested_ShowingDialogIsEqualPushedDialog()
        {
            Setup.Refresh();
            //Arrange
            var dialogSwitcher = Setup.TestSwitcher;
            var dialogSO = Setup.TestDialogSO;
            dialogSwitcher.Events.DialogStarted.Subscribe(AssertDialogs);
            //Act 
            dialogSwitcher.StartDialog(dialogSO, DialogTestConstants.TestCharacter1);
            //Assert
            void AssertDialogs(DialogStarted context)
            {
                dialogSwitcher.Events.DialogStarted.Unsubscribe(AssertDialogs);
                new {dialog = context.Dialog}.Should().BeEquivalentTo(new {dialog = dialogSO});
            }
        }

        [Test]
        public void WhenDialogPushedToCharacter_AndCharacterPushedToDialogRequester_ThenDialogInCharacterIsEqualPushedDialog()
        {
            Setup.Refresh();
            //Arrange
            var character = Setup.TestCharacter1;
            var dialogSO = Setup.TestDialogSO;
            var requester = Setup.TestDialogRequester;
            var dialogs = Setup.Dialogs1;
            dialogs.CharacterId = character.Id;
            dialogs.DialogsSO = new[] {dialogSO};
            //Act
            requester.SetNewCharactersInScene(new[] {character});
            requester.UpdateDialogues(new[] {dialogs});
            //Assert
            new {dialog = character.GetDialog()}.Should().BeEquivalentTo(new {dialog = dialogSO});
        }
    }
}