using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Service.Common;

namespace Test.AppCertification.Service
{
  using log4net;

  /// <summary>
  /// A helper for start the WCF service host
  /// </summary>
  internal class ServiceHostHelper : IDisposable
  {
    private readonly ILog log = LogManager.GetLogger(typeof(ServiceHostHelper));

    private readonly ServiceHost _verificationService;

    private IAppVerificatinonService _service = new AppVerificationService();

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceHostHelper"/> class.
    /// </summary>
    public ServiceHostHelper()
    {
      _verificationService = new ServiceHost(_service);
    }

    /// <summary>
    /// Starts this wcf host.
    /// </summary>
    public void Start()
    {
      _verificationService.Open();
      StringBuilder listenUris = new StringBuilder();
      foreach (ServiceEndpoint endpoint in _verificationService.Description.Endpoints)
      {
        listenUris.AppendLine(endpoint.Address.ToString());
      }
      
      log.Info(string.Format( @"{1} runs on:{2}{0}", listenUris, _verificationService.Description.Name, System.Environment.NewLine));
    }

    /// <summary>
    /// Closes this wcf host.
    /// </summary>
    public void Close()
    {
      _verificationService.Close();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (_verificationService != null && _verificationService.State != CommunicationState.Faulted)
      {
        _verificationService.Close();
      }
    }
  }
}
