using System;
using GTA;

namespace PhoneCallHelper
{
    class PhoneCallHelper : Script
    {
        public PhoneCallHelper() 
        {
            Init();
            Tick += OnTick;
        }

        private void Init()
        {
            Globals.Initialize();
            CallManager.Initialize();
        }

        private void OnTick(object sender, EventArgs e)
        {
            ConversationManager.OnProcessTick();
            CellphoneRenderer.OnProcessTick();
            CallManager.OnProcessTick();
        }
    }
}
