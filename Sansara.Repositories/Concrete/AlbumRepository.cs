using Sansara.Database;
using Sansara.Database.Models;
using Sansara.Repositories.Base;
using Sansara.RepositoriesFacade.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sansara.Repositories.Concrete
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(SansaraContext context) : base(context)
        {
            
        }
    }
}
