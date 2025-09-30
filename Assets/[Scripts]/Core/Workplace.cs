using SQLite;

namespace MassageApp.Core
{
    public class Workplace
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}