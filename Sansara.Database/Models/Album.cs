using Sansara.Database.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sansara.Database.Models
{
    public class Album : BaseEntity
    {
        [Key]
        [Column("AlbumID")]
        public override string Id { get; set; }
        public string Name { get; set; }
        public string ArtistId { get; set; }
        public Image Image { get; set; }
        public string ImageId { get; set; }
    }
}
