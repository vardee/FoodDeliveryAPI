using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace backendTask.DBContext.Models
{
    public class AsAddrObj
    {
        [Key]
        public long id { get; set; }

        public long objectid { get; set; }

        public Guid objectguid { get; set; }

        public long? changeid { get; set; }

        public string name { get; set; } = null!;

        public string? typename { get; set; }

        public string level { get; set; } = null!;

        public int? opertypeid { get; set; }

        public long? previd { get; set; }

        public long? nextid { get; set; }

        public DateOnly? updatedate { get; set; }

        public DateOnly? startdate { get; set; }
        public DateOnly? enddate { get; set; }

        public int? isactual { get; set; }

        public int? isactive { get; set; }

    }
}
