using Sansara.Database.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sansara.Database.Models
{
    public class Artist : BaseEntity
    {
        [Key]
        [Column("ArtistID")]
        public override string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Photo { get; set; }
        public string ImageId { get; set; }
    }
}
