using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncZeus.Api.Entities
{
    public class File
    {
        public string lastModified { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public string type { get; set; }
 
    }
}
