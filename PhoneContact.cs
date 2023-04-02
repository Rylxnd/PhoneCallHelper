namespace PhoneCallHelper
{
    public class PhoneContact
    {
        public string gameContactName;
        public string actualContactName;
        public string pictureName;
        public int pickupTime;

		public PhoneContact(string gameContactName, string actualContactName, string pictureName, int pickupTime)
        {
            this.gameContactName = gameContactName;
            this.actualContactName = actualContactName;
            this.pictureName= pictureName;
            this.pickupTime = pickupTime;
        }
    }
}
