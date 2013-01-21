using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Verso.Net.Commons;

namespace Verso.Net.Facade
{
    [ServiceContract]
    public interface IDataCommunication
    {
        [OperationContract]
        OperationResult<Message> InvokeMethod(Phrase phrase);
    }
}
