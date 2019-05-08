using System;
using System.Collections.Generic;
using System.Text;

namespace Sansara.Database.Base
{
    public abstract class BaseEntity<TKey> where TKey : struct
    {
        public abstract TKey Id { get; set; }
    }
}
