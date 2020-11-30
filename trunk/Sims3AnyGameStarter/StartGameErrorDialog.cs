using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jonha.TS3.AnyGameStarter
{
    public partial class StartGameErrorDialog : Form
    {
        public StartGameErrorDialog(StartGameResult startGameResult)
        {
            InitializeComponent();
            Translator.TranslateForm(this);
            switch (startGameResult)
            {
                case StartGameResult.EarlyGameExit:
                    mainInstruction.Text = Translator.GetText("EarlyGameExitMainInstruction");
                    subTitle.Text = Translator.GetText("EarlyGameExitSubtitle");
                    instruction.Text = Translator.GetText("EarlyGameExitInstruction1")
                            + "\n\n" + Translator.GetText("EarlyGameExitInstruction2")
                            + "\n\n" + Translator.GetText("EarlyGameExitInstruction3");
                    break;
                case StartGameResult.Failure:
                    mainInstruction.Text = Translator.GetText("FailureMainInstruction");
                    subTitle.Text = Translator.GetText("FailureSubtitle");
                    instruction.Text = Translator.GetText("FailureInstruction1")
                            + "\n\n" + Translator.GetText("FailureInstruction2")
                            + "\n\n" + Translator.GetText("FailureInstruction3");
                    break;
                case StartGameResult.PermissionsNeeded:
                    mainInstruction.Text = Translator.GetText("PermissionsNeededMainInstruction");
                    subTitle.Text = Translator.GetText("PermissionsNeededSubtitle");
                    instruction.Text = Translator.GetText("PermissionsNeededInstruction1")
                            + "\n\n" + Translator.GetText("PermissionsNeededInstruction2")
                            + "\n\n" + Translator.GetText("PermissionsNeededInstruction3");
                    break;
                case StartGameResult.ProfileNotFound:
                    mainInstruction.Text = Translator.GetText("ProfileMissingMainInstruction");
                    subTitle.Text = Translator.GetText("ProfileMissingSubtitle");
                    instruction.Text = Translator.GetText("ProfileMissingInstruction1");
                    break;
            }
        }
    }
}
