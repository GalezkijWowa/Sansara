using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sansara.Database.Models
{
    public class Artist
    {
        [Key]
        public string ArtistId { get; set; }
        public string Name { get; set; }
    }
}
