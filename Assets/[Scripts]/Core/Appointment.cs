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
        public float BasePrice { get; set; }
        public int? AppliedDiscountId { get; set; }
        public float AgeDiscountAmount { get; set; } = 0f;
        public float GenderDiscountAmount { get; set; } = 0f;
        public float FinalPrice { get; set; }
    }
}