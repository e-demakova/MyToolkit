namespace Deblue.DialogSystem
{
    public readonly struct Dialog_Choice_Maded
    {
        public readonly Choice Choice;

        public Dialog_Choice_Maded(Choice choice)
        {
            Choice = choice;
        }
    }

    public readonly struct Dialog_Give_Choice
    {
        public readonly Choice[] Choices;

        public Dialog_Give_Choice(Choice[] choices)
        {
            Choices = choices;
        }
    }

    public readonly struct Dialog_Start
    {
        public readonly DialogSO Dialog;

        public Dialog_Start(DialogSO dialog)
        {
            Dialog = dialog;
        }
    }
    
    public readonly struct Dialog_End
    {
        public readonly DialogSO Dialog;

        public Dialog_End(DialogSO dialog)
        {
            Dialog = dialog;
        }
    }
    
    public readonly struct Replica_Switch
    {
        public readonly Replica Replica;

        public Replica_Switch(Replica replica)
        {
            Replica = replica;
        }
    }
}
