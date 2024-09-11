using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationProject.Models
{
    public class FilterModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? TabCat { get; set; }
        public int? ItemCat { get; set; }
        public int? Itm { get; set; }

    }
}