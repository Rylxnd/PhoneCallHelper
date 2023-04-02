namespace PhoneCallHelper
{
    public class DialogueLine
    {
        public PhoneContact speaker;
        public string audioName;
        public string subtitle;
        public bool random;

        public DialogueLine(ref PhoneContact contact, string audioName, string subtitle, bool random = false)
        {
            speaker = contact;
            this.audioName = audioName;
            this.subtitle = subtitle;
            this.random = random;
        }
    }
}
