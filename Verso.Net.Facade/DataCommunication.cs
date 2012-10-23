using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;
using Verso.Net.Commons;

namespace Verso.Net.Facade
{
    public class DataCommunication: IDataCommunication
    {
        public OperationResult<Message> InvokeMethod(Phrase phrase)
        {
            var res = new OperationResult<Message>();
            var msg = new Message();

            try
            {
                object mres = phrase.Method.Invoke("dummy", phrase.Params != null? phrase.Params.ToArray():null);

                var mStream = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(mStream, mres);
                mStream.Close();
                
                //var marshall = new XmlSerializer(mres.GetType());
                //var mStream = new MemoryStream();

                //marshall.Serialize(mStream, mres);

                using (var reader = new StreamReader(mStream))
                {
                    msg.Value = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                res.HasError = true;
                res.Message = ex.Message;
            }

            res.Result = msg;

            return res;
        }
    }
}
