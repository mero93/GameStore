using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities;

namespace API.Data
{
    public static class EntitiesHelper
    {
        public static void CopyProperties(this Game g, Game copy)
        {
            g.Name = copy.Name;
            g.Description = copy.Description;
            g.ReleaseDate = copy.ReleaseDate;
            g.PublisherId = copy.PublisherId;
        }
    }
}