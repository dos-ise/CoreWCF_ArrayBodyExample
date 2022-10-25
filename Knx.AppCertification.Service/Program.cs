using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Test.AppCertification.Service;

namespace Test.AppCertificationService
{
  using log4net.Config;

  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      XmlConfigurator.Configure(); // configure log4net

      if (args.Length == 1 && args[0] == "/console")
      {
        ServiceHostHelper helper = new ServiceHostHelper();
        helper.Start();
        Console.WriteLine("running servicehost... press enter to exit host ");
        Console.ReadLine();
        helper.Close();
      }
      else
      {
        ServiceBase[] ServicesToRun;
        ServicesToRun = new ServiceBase[] { new AppCertificationService() };
        ServiceBase.Run(ServicesToRun);
      }
    }
  }
}
