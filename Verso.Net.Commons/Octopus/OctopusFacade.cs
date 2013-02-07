using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Verso.Net.Commons.Octopus
{
    public static class OctopusFacade
    {
        private const string DefautlOctoEndpoint = "http://localhost/ServiceOctopus";
        private static string _octoEndPoint;

        public static string OctoEndPoint
        {
            get
            {
                if(string.IsNullOrEmpty(_octoEndPoint))
                {
                    var secOcto = ConfigurationManager.GetSection("octopus") as NameValueCollection;

                    if (secOcto != null && !string.IsNullOrEmpty(secOcto["location"]))
                    {
                        _octoEndPoint = secOcto["location"];
                    }
                    else
                    {
                        _octoEndPoint = DefautlOctoEndpoint;
                    }
                }

                return _octoEndPoint;
            }
        }

        public static VersoMsg ExecuteServiceBlock(VersoMsg verso)
        {
            var res = new VersoMsg();

            var httpBind = new BasicHttpBinding();
            httpBind.ReaderQuotas.MaxArrayLength = 2147483647;
            httpBind.ReaderQuotas.MaxStringContentLength = 2147483647;

            using (var octoSvc = new OctopusSvc.ServiceBlockClient(httpBind, new EndpointAddress(new Uri(OctoEndPoint))))
            {
                var verProxy = new OctopusSvc.Verso();

                verProxy.DataVersoExtension = verso.DataVersoExtension;
                verProxy.ServiceBlock = verso.ServiceBlock;
                verProxy.TypeVerso = new OctopusSvc.ServiceTypeGeneric
                                         {
                                             AssemblyName = verso.TypeVerso.AssemblyName,
                                             ClassName = verso.TypeVerso.ClassName
                                         };
                verProxy.Verb = verso.Verb;

                var resProxy = octoSvc.ExecuteServiceBlock(verProxy);

                res.DataVersoExtension = resProxy.DataVersoExtension;
                res.ServiceBlock = resProxy.ServiceBlock;
                res.TypeVerso = new ServiceTypeGeneric
                {
                    AssemblyName = resProxy.TypeVerso.AssemblyName,
                    ClassName = resProxy.TypeVerso.ClassName
                };
                res.Verb = resProxy.Verb;
            }

            return res;
        }
    }
}
