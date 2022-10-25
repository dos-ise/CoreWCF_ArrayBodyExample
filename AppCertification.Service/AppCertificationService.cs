using System.ServiceProcess;
using log4net.Config;
using Test.AppCertification.Service;

namespace Test.AppCertificationService
{
  public partial class AppCertificationService : ServiceBase
  {
    public AppCertificationService()
    {
      InitializeComponent();
    }

    ServiceHostHelper _serviceHostHelper;

    protected override void OnStart(string[] args)
    {
      _serviceHostHelper = new ServiceHostHelper();
      _serviceHostHelper.Start();
      
    }

    protected override void OnStop()
    {
      _serviceHostHelper.Close();
    }
  }
}
