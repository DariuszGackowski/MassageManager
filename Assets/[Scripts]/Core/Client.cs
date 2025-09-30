using SQLite;

namespace MassageApp.Core
{
    public enum GenderType
    {
        None,
        Male,
        Female,
        Other
    }
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        public int VisitCount { get; set; } = 0;
        public string DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string DetailedNotes { get; set; }
    }
}