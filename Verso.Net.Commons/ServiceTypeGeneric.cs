using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Verso.Net.Commons
{
    [DataContract] 
    public class ServiceTypeGeneric
    {
        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string AssemblyName { get; set; }
    }
}
