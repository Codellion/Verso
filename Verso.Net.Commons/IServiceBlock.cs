using System.ServiceModel;

namespace Verso.Net.Commons
{
    [ServiceContract]
    public interface IServiceBlock
    {
        [OperationContract]
        VersoMsg ExecuteServiceBlock(VersoMsg verso);
    }
}
