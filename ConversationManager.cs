using System;
using GTA.Native;

namespace PhoneCallHelper
{
    internal static class ConversationManager
    {
        private static bool isPLaying = false;
        private static PhoneCall activeCall = null;
        private static bool isReadyForNewCall = true;

        public static event EventHandler OnConversationFinishedBeforeCleanup;

        public static void SetCurrentCall(PhoneCall phoneCall)
        {
            if (activeCall == null)
            {
                activeCall = phoneCall;
                isReadyForNewCall = false;
                isPLaying = false;
            }
        }

        public static bool PlayCurrentConversation()
        {
            if (activeCall == null || !Function.Call<bool>(Hash.GET_IS_PRELOADED_CONVERSATION_READY))
            {
                //return false;
            }

            Function.Call(Hash.START_PRELOADED_CONVERSATION);
            isPLaying = true;
            return true;
        }

        public static void EndCurrentConversation(bool finishCurrentLine)
        {
            if (isPLaying && activeCall != null)
            {
                activeCall.End(finishCurrentLine);
            }
            else
            {
                if (Function.Call<bool>(Hash.IS_SCRIPTED_CONVERSATION_ONGOING) || Function.Call<bool>(Hash.IS_MOBILE_PHONE_CALL_ONGOING))
                {
                    Function.Call(Hash.STOP_SCRIPTED_CONVERSATION, false);
                }
            }

            isPLaying = false;
            activeCall = null;
            isReadyForNewCall = true;
        }

        public static bool IsSetCallReadyToPlay()
        {
            return (isPLaying == false) && activeCall != null && Function.Call<bool>(Hash.GET_IS_PRELOADED_CONVERSATION_READY);
        }

        public static bool CanStartNewPhoneCallThisFrame()
        {
            return (isPLaying == false) && isReadyForNewCall && activeCall == null && CellphoneRenderer.IsPhoneClosed();
        }

        public static bool IsPlayingConversation()
        {
            return (isPLaying) && activeCall != null && (Function.Call<bool>(Hash.IS_SCRIPTED_CONVERSATION_ONGOING) || Function.Call<bool>(Hash.IS_MOBILE_PHONE_CALL_ONGOING));
        }

        public static void OnProcessTick()
        {
            if (isPLaying == true && activeCall != null)
            {
                if (!Function.Call<bool>(Hash.IS_MOBILE_PHONE_CALL_ONGOING))
                {
                    OnConversationFinishedBeforeCleanup?.Invoke(null, EventArgs.Empty);
                    EndCurrentConversation(false);
                    CellphoneRenderer.ClosePhone();

                    isPLaying = false;
                    activeCall = null;
                    isReadyForNewCall = true;
                }
            }
        }
    }
}
