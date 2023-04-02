using GTA;
using GTA.Native;
using PhoneCallHelper;
using System;
using System.Windows.Forms;

namespace ExampleScript
{
    public class ExampleScript : Script
    {
        PhoneCall phoneCall;
        PhoneContact phoneContact;

        public ExampleScript() 
        {
            // Load the subtitles GXT file
            Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 14, false);
            Function.Call(Hash.REQUEST_ADDITIONAL_TEXT_FOR_DLC, "CAGTAU", 14);

            while (!Function.Call<bool>(Hash.HAS_ADDITIONAL_TEXT_LOADED, 14))
            {
            }

            KeyDown += OnKeyDown;
            CallManager.OnDialogueFinished += OnDialogueFinished;

            phoneCall = new PhoneCall();

            phoneContact = new PhoneContact("CAS_AGATHA", "Ms. Baker", "CHAR_CASINO_MANAGER", 4000);
            phoneCall.SetContact(ref phoneContact);

            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAA", "CAGT_M1_UC1_1", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAB", "CAGT_M1_UC1_3", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAC", "CAGT_M1_UC1_5", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAD", "CAGT_M1_UC1_7", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAE", "CAGT_M1_UC1_9", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAF", "CAGT_M1_UC1_10", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAG", "CAGT_M1_UC1_11", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAH", "CAGT_M1_UC1_12", false));
            phoneCall.AddLine(new DialogueLine(ref phoneContact, "CAGT_HAAI", "CAGT_M1_UC1_14", false));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T && CallManager.CanStartNewPhoneCall())
            {
                phoneCall.Prepare(true, true);
                CallManager.Call(phoneCall, eCallType.Incoming, true);
            }
        }

        private void OnDialogueFinished(object sender, EventArgs e)
        {
            // You can display a menu or something else here. Once the function returns the phone will hang up.
        }

        private void OnCallCompleted(object sender, EventArgs e)
        {
            // Do something after the phone hangs up.
        }
    }
}
