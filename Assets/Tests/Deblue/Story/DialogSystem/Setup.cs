using System.Reflection;
using Deblue.SceneManagement;
using Deblue.Story.Characters;
using Deblue.Story.Steps;
using UnityEngine;

namespace Deblue.Story.DialogSystem.Tests
{
    public static class DialogTestConstants
    {
        public const string TestCharacter1 = "TestChar1";
        public const string TestCharacter2 = "TestChar2";
        public const string TestReplica1 = "TestReplica1";
        public const string TestReplica2 = "TestReplica2";
    }

    internal static class Setup
    {
        private static DialogSwitcher _dialogSwitcher;
        private static DialogRequester _dialogRequester;
        private static DialogSO _dialog;
        private static Dialogs _dialogs1;
        private static Dialogs _dialogs2;
        private static Character _character1;
        private static Character _character2;

        public static DialogSwitcher TestSwitcher => _dialogSwitcher ??= new DialogSwitcher();

        public static DialogRequester TestDialogRequester =>
            _dialogRequester ??= CreateDialogRequester();

        public static Dialogs Dialogs1 => _dialogs1 ??= new Dialogs();

        public static Dialogs Dialogs2 => _dialogs2 ??= new Dialogs();

        public static DialogSO TestDialogSO => _dialog ??= CreateTestDialog();

        public static Character TestCharacter1 => _character1 ??= CreateTestCharacter(DialogTestConstants.TestCharacter1);

        public static Character TestCharacter2 => _character2 ??= CreateTestCharacter(DialogTestConstants.TestCharacter2);


        public static void Refresh()
        {
            _dialogSwitcher = null;
            _dialogRequester = null;
            _dialog = null;
            _dialogs1 = null;
            _dialogs2 = null;
            _character1 = null;
            _character2 = null;
        }

        private static DialogSO CreateTestDialog()
        {
            var dialog = ScriptableObject.CreateInstance<DialogSO>();
            SetReplicasToDialog(dialog);

            return dialog;
        }

        private static void SetReplicasToDialog(DialogSO dialog)
        {
            var type = typeof(DialogSO);
            var replicasField = type.GetField("_replicas", BindingFlags.Instance | BindingFlags.NonPublic);

            var replicas = new Replica[]
            {
                new Replica(DialogTestConstants.TestCharacter1, DialogTestConstants.TestReplica1),
                new Replica(DialogTestConstants.TestCharacter1, DialogTestConstants.TestReplica2)
            };
            replicasField.SetValue(dialog, replicas);
        }

        private static Character CreateTestCharacter(string id)
        {
            var character = new GameObject(id, typeof(BoxCollider2D), typeof(Character)).GetComponent<Character>();
            SetIdToCharacter(id, character);
            return character;
        }

        private static void SetIdToCharacter(string id, Character character)
        {
            var characterType = typeof(Character);
            var idField = characterType.GetField("_id", BindingFlags.Instance | BindingFlags.NonPublic);
            idField.SetValue(character, id);
        }

        public static DialogRequester CreateDialogRequester()
        {
            return new DialogRequester(TestSwitcher, new SceneLoader(ScriptableObject.CreateInstance<StartScenesConfigSO>()));
        }
    }
}