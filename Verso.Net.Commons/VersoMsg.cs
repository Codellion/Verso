using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Verso.Net.Commons
{
    [DataContract(Name = "Verso")]
    public class VersoMsg
    {
        [DataMember]
        public string ServiceBlock { get; set; }

        [DataMember]
        public string Verb { get; set; }

        [DataMember]
        public ServiceTypeGeneric TypeVerso { get; set; }

        [DataMember]
        public byte[] DataVersoExtension { get; set; }

        public object DataVerso
        {
            get { return GetData(null); }
            set { SetData(value); }
        }

        private object _data;

        public T GetData<T>()
        {
            if (typeof (T).FullName == TypeVerso.ClassName && _data == null)
            {
                using (var stream = new MemoryStream(DataVersoExtension))
                {
                    var bformatter = new BinaryFormatter();
                    bformatter.Binder = new VersoTypeBinder();

                    _data = bformatter.Deserialize(stream);
                    stream.Close();
                }

                return (T) _data;
            }

            throw new FormatException("El tipo almacenado no se corresponde con el especificado");
        }

        private object GetData(Type type)
        {
            if (_data == null)
            {
                using (var stream = new MemoryStream(DataVersoExtension))
                {
                    var bformatter = new BinaryFormatter();
                    bformatter.Binder = new VersoTypeBinder();

                    _data = bformatter.Deserialize(stream);
                    stream.Close();
                }
            }

            return _data;
        }

        public void SetData(object data)
        {
            TypeVerso = new ServiceTypeGeneric();

            var tdata = data.GetType();
                
            TypeVerso.ClassName = tdata.FullName;
            TypeVerso.AssemblyName = tdata.Assembly.FullName;

            using (var stream = new MemoryStream())
            {
                var bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, data);
                stream.Close();

                DataVersoExtension = stream.ToArray();
            }
        }

        public void SetData<T>(object data)
        {
            TypeVerso = new ServiceTypeGeneric();

            var tdata = typeof(T);

            TypeVerso.ClassName = tdata.FullName;
            TypeVerso.AssemblyName = tdata.Assembly.FullName;

            using (var stream = new MemoryStream())
            {
                var bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, data);
                stream.Close();

                DataVersoExtension = stream.ToArray();
            }
        }

        public object Execute(object inteface, Type type)
        {
            object res = null;

            try
            {
                res = inteface.GetType().GetMethod(Verb, new Type[] { type }).Invoke(inteface, new[] { GetData(type) });
            }
            catch(Exception ex)
            {
                throw ex.InnerException;
            }

            return res;
        }

        public T ToServiceDto<T>() where T : new()
        {
            var res = new T();

            res.GetType().GetProperty("DataVersoExtension").SetValue(res, DataVersoExtension, null);
            res.GetType().GetProperty("TypeVerso").SetValue(res, TypeVerso, null);
            res.GetType().GetProperty("Verb").SetValue(res, Verb, null);

            return res;
        }
    }
}
