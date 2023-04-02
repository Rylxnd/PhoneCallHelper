using GTA;
using GTA.Native;
using System;

namespace PhoneCallHelper
{
    internal enum eCallManagerState : int
    { 
        InActive,
        Calling,
        Connected
    }

    public enum eCallType : int
    { 
        Outgoing,
        Incoming,
    }

    public static class CallManager
    {
        private static int lastTickTime;
        private static bool canHangUpCall;
        private static eCallType phoneCallType;
        private static eCallManagerState currentState;
        private static PhoneCall phoneCallInstance;

        /// <summary>
        /// When the phone call is finished, and the phone is hung up
        /// </summary>
        public static event EventHandler OnCallCompleted;
        /// <summary>
        /// When the phone call dialogue is finished playing
        /// </summary>
        public static event EventHandler OnDialogueFinished;

        public static bool Call(PhoneCall callInstance, eCallType callType, bool canHangUp)
        {
            if (ConversationManager.IsSetCallReadyToPlay())
            {
                return false;
            }

            CellphoneRenderer.SetCallScreenCharcater(callInstance.GetContact());
            CellphoneRenderer.SetRenderHangUpButton(canHangUp);
            CellphoneRenderer.OpenPhone();

            switch (callType)
            {
                case eCallType.Outgoing:
                    CellphoneRenderer.SetCallScreenState(eCellphoneCallState.Dialing);
                    Function.Call(Hash.PLAY_PED_RINGTONE, "Remote_Ring", Game.Player.Character, 1);
                    break;
                case eCallType.Incoming:
                    CellphoneRenderer.SetCallScreenState(eCellphoneCallState.Incoming);
                    CellphoneRenderer.SetRenderPickUpButton(true);
                    Function.Call(Hash.PLAY_PED_RINGTONE, Globals.GetScriptCellphoneRingtoneName(), Game.Player.Character, 1);
                    break;
            }

            phoneCallInstance = callInstance;
            canHangUpCall = canHangUp;
            phoneCallType = callType;

            currentState = eCallManagerState.Calling;
            lastTickTime = Game.GameTime;

            return true;
        }

        public static bool CanStartNewPhoneCall()
        {
            return ConversationManager.CanStartNewPhoneCallThisFrame();
        }

        internal static void Initialize()
        {
            ConversationManager.OnConversationFinishedBeforeCleanup += OnConversationFinishedBeforeCleanup;
        }

        private static void OnConversationFinishedBeforeCleanup(object sender, EventArgs e)
        {
            OnDialogueFinished?.Invoke(null, EventArgs.Empty);
        }

        internal static void OnProcessTick()
        {
            int currentTickTime = Game.GameTime;
            if (currentState == eCallManagerState.Calling)
            {
                if (phoneCallType == eCallType.Outgoing)
                {
                    if (currentTickTime - lastTickTime > phoneCallInstance.GetContact().pickupTime)
                    {
                        CellphoneRenderer.SetCallScreenState(eCellphoneCallState.Connected);
                        ConversationManager.PlayCurrentConversation();
                        currentState = eCallManagerState.Connected;
                    }
                }
                else if (phoneCallType == eCallType.Incoming)
                {
                    if (currentTickTime - lastTickTime > phoneCallInstance.GetContact().pickupTime || (Game.IsControlJustPressed(Control.PhoneSelect) || Game.IsEnabledControlJustPressed(Control.PhoneSelect)))
                    {
                        Audio.PlaySoundFrontend("Menu_Accept", CellphoneProperties.GetPlayerPhoneSoundSetName());
                        CellphoneRenderer.SetRenderPickUpButton(false);
                        CellphoneRenderer.SetCallScreenState(eCellphoneCallState.Connected);
                        ConversationManager.PlayCurrentConversation();
                        currentState = eCallManagerState.Connected;
                    }
                }
            }
            else if (currentState == eCallManagerState.Connected)
            {
                if (Function.Call<bool>(Hash.IS_PED_RINGTONE_PLAYING, Game.Player.Character))
                {
                    Function.Call(Hash.STOP_PED_RINGTONE, Game.Player.Character);
                }
                if (!ConversationManager.IsPlayingConversation())
                {
                    if (!Game.Player.Character.IsDead)
                    {
                        Audio.PlaySoundFrontend("Hang_Up", CellphoneProperties.GetPlayerPhoneSoundSetName());
                    }
                    currentState = eCallManagerState.InActive;
                    OnCallCompleted?.Invoke(null, EventArgs.Empty);
                }
            }

            if (currentState != eCallManagerState.InActive)
            {
                if (canHangUpCall && (Game.IsControlJustPressed(Control.PhoneCancel) || Game.IsEnabledControlJustPressed(Control.PhoneCancel)))
                {
                    if (!Game.Player.Character.IsDead)
                    {
                        Audio.PlaySoundFrontend("Hang_Up", CellphoneProperties.GetPlayerPhoneSoundSetName());
                    }
                    currentState = eCallManagerState.InActive;
                    ConversationManager.EndCurrentConversation(false);
                    CellphoneRenderer.ClosePhone();
                }
            }
        }
    }
}
