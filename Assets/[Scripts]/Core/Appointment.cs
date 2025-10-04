using SQLite;

namespace MassageApp.Core
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int MassageId { get; set; }
        public int WorkplaceId { get; set; }
        public string DateTime { get; set; }
        public int? DiscountId { get; set; }
        public float FinalPrice { get; set; }
    }
}