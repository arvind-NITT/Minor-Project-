namespace MatrimonialApp.Models
{
    public class Subscription
    {
        public int SubscriptionID { get; set; }
        public int UserID { get; set; }
        public string SubscriptionType { get; set; } // Consider using an enum
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public virtual User User { get; set; }
    }

}
