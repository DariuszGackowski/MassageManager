using SQLite;

namespace MassageApp.Core
{
    public class Discount
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string DiscountName { get; set; }

        public float Percentage { get; set; }
    }
}
