using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDB.Models
{
    public class MoviesDB
    {
        [Key]
        public int Id { get; set; }
      
        public byte[] Banner { get; set; }
        [Required]
        public string MovieLink { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string About { get; set; }

        public string Category { get; set; }
        public enum ECategory { Action = 0, Adventure = 1, Horror = 2, Drama = 3, Romance = 4, Comedy = 5}
        [Required]
        public string Director { get; set; }
        [Required]
        public string Producer { get; set; }
        [Required]
        public string Actors { get; set; }
        [Required]
        public string Watchtime { get; set; }
        [Required]
        public string Release { get; set; }

        public string Ratings { get; set; }
        public enum ERatings { Zero = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5}

    }
}