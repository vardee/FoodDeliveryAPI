using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace backendTask.DBContext.Models
{
    public class as_houses
    {
        [Key]
        public BigInteger id { get; set; }
        public BigInteger objectid { get; set; }
        public Guid objectguid { get; set; }
        public BigInteger changeid { get; set; }
        public string housenum { get; set; }
        public string addnum1 { get; set; }
        public string addnum2 { get; set; }
        public string housetype { get; set; }
        public string addtype1 { get; set; }
        public string addtype2 { get; set; }
        public int opertypeid { get; set; }
        public BigInteger previd { get; set; }
        public BigInteger nextid { get; set; }
        public DateOnly updatedate { get; set; }
        public DateOnly startdate { get; set; }
        public DateOnly enddate { get; set; }
        public int isactual { get; set; }
        public int isactive { get; set; }
    }
}
