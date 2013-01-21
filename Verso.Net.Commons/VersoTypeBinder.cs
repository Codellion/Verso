using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Octopus.Injection.Commons;

namespace Verso.Net.Commons
{
    sealed class VersoTypeBinder: SerializationBinder 
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (ConfigRepository.ServiceTypes.ContainsKey(assemblyName))
            {
                if(typeName.Contains("`"))
                {
                    //Es un tipo genérico, cogemos el tipo base y el nº de eltos gen.

                    string[] typeData = typeName.Split('`');

                    string typeBaseName = typeData[0];
                    int numGenParams = int.Parse(typeData[1].Substring(0, 1));
                    string detGenparams = typeData[1].Substring(1, typeData[1].Length-1);

                    var detGenparamsArr = detGenparams.Replace("[", string.Empty).Split(']');

                    var genTypes = new Type[numGenParams];

                    for (int i = 0; i < numGenParams; i++)
                    {
                        string detGenType = detGenparamsArr[i];
                        var detGenTypeArr = detGenType.Split(',');
                        string genAssemblyName = string.Format("{0}, {1}, {2}, {3}", 
                            detGenTypeArr[1].Trim(), detGenTypeArr[2].Trim(), detGenTypeArr[3].Trim(), detGenTypeArr[4].Trim());

                        if(ConfigRepository.ServiceTypes.ContainsKey(genAssemblyName))
                        {
                            genTypes[i] = ConfigRepository.ServiceTypes[genAssemblyName].GetType(detGenTypeArr[0]);
                        }
                        else
                        {
                            genTypes[i] = Type.GetType(detGenType);
                        }
                    }
                    
                    var typeBase = ConfigRepository.ServiceTypes[assemblyName].GetType(typeData[0] + "`" + typeData[1].Substring(0,1));
                    typeBase = typeBase.MakeGenericType(genTypes);

                    return typeBase;
                }
                else
                {
                    return ConfigRepository.ServiceTypes[assemblyName].GetType(typeName);
                }
            }
            else
            {
                return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
            }
        }
    }
}
