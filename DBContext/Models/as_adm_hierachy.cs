using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace backendTask.DBContext.Models
{
    public class as_adm_hierachy
    {
        [Key]
        public BigInteger id { get; set; }
        public BigInteger objectid { get; set; }
        public BigInteger parentobjid { get; set; }
        public BigInteger changeid { get; set; }
        public string regioncode { get; set; }
        public string areacode { get; set; }
        public string citycode { get; set; }
        public int placecode { get; set; }
        public BigInteger plancode { get; set; }
        public BigInteger streetcode { get; set; }
        public DateOnly previd { get; set; }
        public DateOnly updatedate { get; set; }
        public DateOnly startdate { get; set; }
        public DateOnly enddate { get; set; }
        public int isactive { get; set; }
        public string path { get; set; }
    }
}
