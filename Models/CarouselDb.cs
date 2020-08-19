using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDB.Models
{
    public class CarouselDb
    {
        [Key]
        public int bannerId { get; set; }
        public byte[] Banners { get; set; }
    }
}
