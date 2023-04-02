using GTA;

namespace PhoneCallHelper
{
    internal static class CellphoneProperties
    {
        public static string GetPlayerPhoneScaleformName()
        {
            string phoneScaleformName;

            int playerPedModel = Game.Player.Character.Model.Hash;

            if (playerPedModel == Game.GenerateHash("player_one"))
            {
                phoneScaleformName = "cellphone_badger";
            }
            else if (playerPedModel == Game.GenerateHash("player_two"))
            {
                phoneScaleformName = "cellphone_facade";
            }
            else
            {
                phoneScaleformName = "cellphone_ifruit";
            }

            return phoneScaleformName;
        }

        public static string GetPlayerPhoneSoundSetName()
        {
            string phoneSoundSetName;

            int playerPedModel = Game.Player.Character.Model.Hash;

            if (playerPedModel == Game.GenerateHash("player_zero"))
            {
                phoneSoundSetName = "Phone_SoundSet_Michael";
            }
            else if (playerPedModel == Game.GenerateHash("player_one"))
            {
                phoneSoundSetName = "Phone_SoundSet_Franklin";
            }
            else if (playerPedModel == Game.GenerateHash("player_two"))
            {
                phoneSoundSetName = "Phone_SoundSet_Trevor";
            }
            else
            {
                phoneSoundSetName = "Phone_SoundSet_Default";
            }

            return phoneSoundSetName;
        }
    }
}
