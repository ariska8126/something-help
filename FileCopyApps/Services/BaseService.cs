using FileCopyApps.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.Services
{
    internal class BaseService
    {
        protected static readonly ConverterContext db = new();
    }
}
