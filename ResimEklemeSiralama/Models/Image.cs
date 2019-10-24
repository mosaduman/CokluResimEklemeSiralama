using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResimEklemeSiralama.Models
{
    public class Image
    {
        public int Id { get; set; }
        public Nullable<int> imgCarId { get; set; }
        public Nullable<int> imgMain { get; set; }
        public string imgAddress { get; set; }
        public Nullable<int> imgList { get; set; }
    }
}