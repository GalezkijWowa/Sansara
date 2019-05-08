using Sansara.Database.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sansara.Database.Models
{
    public class User : BaseEntity
    {
        [Key]
        [Column("UserID")]
        public override string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
