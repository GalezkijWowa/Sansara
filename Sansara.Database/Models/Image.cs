using Sansara.Database.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sansara.Database.Models
{
    public class Image : BaseEntity
    {
        public Image()
        {

        }
        public Image(Uri source)
        {
            if (source != null)
            {
                Source = "https://" + source.DnsSafeHost + source.AbsolutePath;
            }
            else
            {
                Source = "https://blog.sqlauthority.com/i/a/errorstop.png";
            }
        }

        [Key]
        [Column("ImageID")]
        public override string Id { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return Source;
        }
    }
}
