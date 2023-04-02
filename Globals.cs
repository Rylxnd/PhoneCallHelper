using System;
using GTA.Native;

namespace PhoneCallHelper
{
    internal static class Globals
    {
        private static GlobalVariable scriptGlobalCellphoneState;
        private static GlobalVariable scriptGlobalCellphoneLocked;
        private static GlobalVariable scriptGlobalCellphoneRingtone;

        public static void Initialize()
        {
            // as of b2845
            scriptGlobalCellphoneLocked = GlobalVariable.Get(20361);
            scriptGlobalCellphoneState = GlobalVariable.Get(20384); // Global_20383.f_1
            scriptGlobalCellphoneRingtone = GlobalVariable.Get(127701).GetArrayItem(1,1).GetStructField(10); // Global_113648.f_14053[1].f_11
        }

        public static int GetScriptCellphoneState()
        {
            return scriptGlobalCellphoneState.Read<int>();
        }

        public static void SetScriptCellphoneState(int state)
        {
            scriptGlobalCellphoneState.Write<int>(state);
        }

        public static bool GetScriptCellphoneLocked()
        {
            return Convert.ToBoolean(scriptGlobalCellphoneLocked.Read<int>());
        }

        public static void SetScriptCellphoneLocked(bool locked)
        {
            scriptGlobalCellphoneLocked.Write(Convert.ToInt32(locked));
        }

        public static string GetScriptCellphoneRingtoneName()
        {
            return scriptGlobalCellphoneRingtone.Read<string>();
        }
    }
}
