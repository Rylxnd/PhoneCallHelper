using System.Collections.Generic;
using GTA;
using GTA.Native;

namespace PhoneCallHelper
{
    public class PhoneCall
    {
        private PhoneContact phoneCallContact = null;
        private List<DialogueLine> dialogues = new List<DialogueLine>();

        public PhoneCall()
        {

        }

        public bool SetContact(ref PhoneContact contact)
        {
            if (phoneCallContact != null)
            {
                return false;
            }
            phoneCallContact = contact;
            return true;
        }

        public PhoneContact GetContact()
        {
            return phoneCallContact;
        }

        public void AddLine(DialogueLine line)
        {
            dialogues.Add(line);
        }

        public void AddLines(DialogueLine[] lines)
        {
            foreach (var line in lines)
            {
                AddLine(line);
            }
        }

        public bool Prepare(bool showSubtitles, bool addToBriefScreen)
        {
            if (!ConversationManager.CanStartNewPhoneCallThisFrame())
            {
                return false;
            }

            // Ensure the game isn't playing dialogue
            ConversationManager.EndCurrentConversation(false);

            Function.Call(Hash.CREATE_NEW_SCRIPTED_CONVERSATION);

            Function.Call(Hash.ADD_PED_TO_CONVERSATION, 0, 0, phoneCallContact.gameContactName);
            Function.Call(Hash.ADD_PED_TO_CONVERSATION, 1, Game.Player.Character, "");

            foreach (DialogueLine line in dialogues)
            {
                Function.Call(Hash.ADD_LINE_TO_CONVERSATION, 0, line.audioName, line.subtitle, 1, 1, line.random, false, true, true, 0, false, false, false);
            }

            Function.Call(Hash.PRELOAD_SCRIPT_PHONE_CONVERSATION, showSubtitles, addToBriefScreen);

            ConversationManager.SetCurrentCall(this);
            return true;
        }

        internal bool Play()
        {
            return ConversationManager.PlayCurrentConversation();
        }

        internal void End(bool finishCurrentLine)
        {
            Function.Call(Hash.STOP_SCRIPTED_CONVERSATION, finishCurrentLine);
        }


    }
}