using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace backendTask.DBContext.Models
{
    public class as_houses
    {
        [Key]
        public long id { get; set; }

        public long objectid { get; set; }

        public Guid objectguid { get; set; }

        public long? changeid { get; set; }

        public string? housenum { get; set; }
        public string? addnum1 { get; set; }

        public string? addnum2 { get; set; }

        public int? housetype { get; set; }

        public int? addtype1 { get; set; }

        public int? addtype2 { get; set; }

        public int? opertypeid { get; set; }

        public long? orevid { get; set; }

        public long? nextid { get; set; }

        public DateOnly? updatedate { get; set; }

        public DateOnly? startdate { get; set; }

        public DateOnly? enddate { get; set; }

        public int? isactual { get; set; }
        public int? isactive { get; set; }
    }
}
