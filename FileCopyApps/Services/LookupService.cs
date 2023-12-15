using FileCopyApps.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.Services
{
    internal class LookupService : BaseService
    {
        public Lookup? findByType(string type) { 
            return db.Lookups.FirstOrDefault(x => x.Type == type);
        }
    }
}
