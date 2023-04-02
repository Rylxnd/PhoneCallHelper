using GTA;
using GTA.Native;

namespace PhoneCallHelper
{
    internal static class CellphoneRenderer
    {
        private static int lastPlayerModel;

        private static int cellphoneScaleformInstance;
        private static string callScreenCharacterName;
        private static string callScreenCharacterPicture;
        private static string callScreenState;

        private static bool mainRender;
        private static bool hangUpButtonRender;
        private static bool pickUpButtonRender;

        public static void OpenPhone()
        {
            Globals.SetScriptCellphoneState(4);
            mainRender = true;
        }

        public static void ClosePhone() 
        {
            Globals.SetScriptCellphoneState(3);
            mainRender = false;
        }

        private static void DrawPhone()
        {
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "SET_SOFT_KEYS");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 1);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, true);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 1);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);

            if (hangUpButtonRender)
            {
                Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "SET_SOFT_KEYS");
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 3);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, true);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 6);
                Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
            }
            else
            {
                Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "SET_SOFT_KEYS");
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 3);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, true);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 1);
                Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
            }

            if (pickUpButtonRender)
            {
                Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "SET_SOFT_KEYS");
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 2);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, true);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 5);
                Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
            }
            else
            {
                Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "SET_SOFT_KEYS");
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 2);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, true);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 1);
                Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
            }

            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "SET_DATA_SLOT");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 4);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 0);
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 3);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, callScreenCharacterName);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, "CELL_2000");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, callScreenCharacterPicture);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, callScreenState);
            Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);

            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);

            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, cellphoneScaleformInstance, "DISPLAY_VIEW");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 4);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
        }

        public static void SetCallScreenCharcater(PhoneContact character)
        {
            callScreenCharacterName = character.actualContactName;
            callScreenCharacterPicture = character.pictureName;
        }

        public static void SetRenderHangUpButton(bool render)
        {
            hangUpButtonRender = render;
        }

        public static void SetRenderPickUpButton(bool render)
        {
            pickUpButtonRender = render;
        }

        public static void OnProcessTick()
        {
            if (mainRender)
            {
                if (cellphoneScaleformInstance != 0)
                {
                    DrawPhone();
                }
                else
                {
                    cellphoneScaleformInstance = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, CellphoneProperties.GetPlayerPhoneScaleformName());
                }

                if (Globals.GetScriptCellphoneState() >= 6)
                {
                    Globals.SetScriptCellphoneLocked(true);
                }
                else
                {
                    Globals.SetScriptCellphoneLocked(false);
                }
            }

            if (lastPlayerModel != Game.Player.Character.Model.Hash)
            {
                lastPlayerModel = Game.Player.Character.Model.Hash;
                cellphoneScaleformInstance = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, CellphoneProperties.GetPlayerPhoneScaleformName());
            }
        }

        public static void SetCallScreenState(eCellphoneCallState state)
        {
            callScreenState = CellphoneCallState.GetCellphoneStateString(state);
        }
    }
}
