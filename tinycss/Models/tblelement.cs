using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCSS_Webapi.Models
{
    public class tblelement
    {
        [Key]
        public int mguid { get; set; }
        public string userid { get; set; }
        public string mtype { get; set; }
        public string mimg { get; set; }
        public string mhtml { get; set; }
        public string mcss { get; set; }
        public string mtitle { get; set; }
        public string mdesc { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string ext3 { get; set; }
    }
}
