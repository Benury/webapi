﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCSS_Webapi.Models
{
    public class tblfav
    {
        [Key]
        public int fguid { get; set; }
        public string userid { get; set; }
        public string mtype { get; set; }
        public int mguid { get; set; }
    }
}
