using SQLite;

namespace MassageApp.Core
{
    public class Massage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string MassageName { get; set; }

        public float Price { get; set; }

        public int DurationMinutes { get; set; }
    }
}