using System;
using System.Collections.Generic;
using System.Text;

namespace MugginsDominoes.Models
{
    public class Result
    {
        public int TargetEnd { get; set; }
        public int Match { get; set; }
        public int Sum { get; set; }
        public bool IsPotentialEnd { get; set; }
    }
}
