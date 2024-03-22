using FileCopyApps.Entities;
using FileCopyApps.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.Services
{
    public class BaseService
    {
        protected static readonly RnDContext db = new();
        //protected static readonly ConverterContext db = new();
        //protected static readonly GoAmlContext db = new();
        public static FileUtils fileUtils = new();
    }
}
