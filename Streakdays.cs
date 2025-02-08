using SQLite;

namespace AquaMate
{
    [Table("Streakdays")]
    public class Streakdays
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("Dates")]
        public DateTime Dates { get; set; }

        [Column("Consumption")]
        public double Consumption { get; set; }

        [Column("erreicht")]
        public bool Erreicht { get; set; }

        [Column("ChartDates")]
        public string ChartDates => Dates.ToString("dd.MM.yy");
    }
}
