using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace backendTask.DBContext.Models
{
    public class as_addr_obj
    {
        [Key]
        public BigInteger id { get; set; }
        public BigInteger objectid { get; set; }
        public Guid objectguid { get; set; }
        public BigInteger changeid { get; set; }
        public string name { get; set; }
        public string typename { get; set; }
        public string level { get; set; }
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
