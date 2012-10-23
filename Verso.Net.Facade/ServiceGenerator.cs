using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Verso.Net.Commons;

namespace Verso.Net.Facade
{
    public class ServiceGenerator
    {
        public void Null()
        {
            Type t = typeof (Message);

            foreach (object cAttribute in t.GetProperty("").GetCustomAttributes(false))
            {
                
                if(cAttribute is DataMemberAttribute)
                {
                    var a = cAttribute as DataMemberAttribute;

                }
            }
        }
    }
}
