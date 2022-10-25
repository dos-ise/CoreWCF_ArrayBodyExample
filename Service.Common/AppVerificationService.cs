using log4net;
using System.IO;
#if NET6_0
using CoreWCF;
using CoreWCF.Web;
#else
using System.ServiceModel;
using System.ServiceModel.Web;
#endif

namespace Service.Common
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AppVerificationService : IAppVerificatinonService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AppVerificationService));

        public string CheckApp(string filename, string expectedAppId, byte[] FileContainer)
        {
            //http://localhost/AppService/CheckApp/addin.app
            Log.Debug(filename);
            return filename;
        }
    }

    [ServiceContract(Name = "AppVerificationService")]
    public interface IAppVerificatinonService
    {
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CheckApp/{filename}?AppId={appid}")]
        [OperationContract]
        string CheckApp(string filename, string appid, byte[] FileContainer);
    }
}
