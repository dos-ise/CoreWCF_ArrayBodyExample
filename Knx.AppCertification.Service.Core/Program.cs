using System;
using System.Threading.Tasks;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Services.Description;
using System.Xml;
using Service.Common;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
  options.AllowSynchronousIO = true;
  options.Limits.MaxRequestBodySize = null;
  options.Limits.MaxRequestBufferSize = null;
});

// Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
builder.Services.AddServiceModelWebServices();

var app = builder.Build();
app.UseServiceModel(serviceBuilder =>
{
  ////Increase Message Size otherwise Apps greater then 510kb can not be uploaded
  var binding = new WebHttpBinding
  {
    MaxReceivedMessageSize = Int32.MaxValue,
    MaxBufferSize = Int32.MaxValue
  };
  serviceBuilder.AddService<System.Web.Services.Description.Service>();
  serviceBuilder.AddService<AppVerificationService>();
  serviceBuilder.AddServiceWebEndpoint<AppVerificationService, IAppVerificatinonService>(binding, "AppService");
});
var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();
