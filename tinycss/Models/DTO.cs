using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCSS_Webapi.Models
{
    public class ElementDTO
    {
        public int mguid { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public string img { get; set; }
        public string html { get; set; }
        public string css { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string ext3 { get; set; }
    }
}
