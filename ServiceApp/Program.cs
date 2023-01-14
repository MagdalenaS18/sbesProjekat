using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Contracts;
using System.IO;
using static System.Net.WebRequestMethods;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;
using Manager;
using System.Threading;

namespace ServiceApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			//string fileName = @"C:\Users\EC\Downloads\rjesenjeSaCert\rjesenje\Vezba_03_resenje\baza.txt";
			string fileName = @"C:\Users\EC\Downloads\provjeraLog\provjeraLog\rjesenjeSaCert\rjesenje\Vezba_03_resenje\baza.txt";

			string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			//string srvCertCN = Formatter.ParseName(Thread.CurrentPrincipal.Identity.Name);

			NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
			string address = "net.tcp://localhost:9999/WCFService";

			ServiceHost host = new ServiceHost(typeof(WCFService));
			host.AddServiceEndpoint(typeof(IWCFService), binding, address);


			// NetTcpBinding binding = new NetTcpBinding();
			//string address = "net.tcp://localhost:9999/WCFService";

			//ServiceHost host = new ServiceHost(typeof(WCFService));
			//host.AddServiceEndpoint(typeof(IWCFService), binding, address);

			/// Custom validation mode enables creation of a custom validator -CustomCertificateValidator

			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
			host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
			host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
			host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

			// ???
			//host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
			//host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
			//// ??? ovo je za checkAccess
			////host.Authorization.ServiceAuthorizationManager = new ServiceAuthorizationManager();
			//// ovo sig treba
			//host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
			//List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
			//policies.Add(new CustomAuthorizationPolicy());
			//host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

			// Audit
			//ServiceSecurityAuditBehavior newAuditBehavior = new ServiceSecurityAuditBehavior();
			//newAuditBehavior.AuditLogLocation = AuditLogLocation.Application;
			//newAuditBehavior.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;
			//host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
			//host.Description.Behaviors.Add(newAuditBehavior);


			// ovo provjeriti da li nam treba
			//host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
			//host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
			//host.Authorization.ServiceAuthorizationManager = new ServiceAuthorizationManager();
			//host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
			//List<IAuthorizationPolicy> authorizationPolicies = new List<IAuthorizationPolicy>();
			//authorizationPolicies.Add(new AuthorizationPolicy());
			//host.Authorization.ExternalAuthorizationPolicies = authorizationPolicies.AsReadOnly();

			try
			{
				host.Open();
				Console.WriteLine("WCFService is opened.");
				Database db = new Database();

				FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);
				Console.WriteLine("File opened");
				StreamWriter streamWriter = null;

				try
				{
					using (streamWriter = new StreamWriter(file))
					{
						streamWriter.WriteLine("Spisak koncerata:");
						foreach (Koncert k in Database.koncerti.Values)
						{
							k.ToString();
							streamWriter.WriteLine("\n\t" + k);
						}
						streamWriter.WriteLine("Spisak rezervacija:");
						foreach (Rezervacija r in Database.rezervacije.Values)
						{
							r.ToString();
							streamWriter.WriteLine("\n\t" + r);

						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}

				Console.ReadLine();
			}
			catch (Exception e)
			{
				Console.WriteLine("[ERROR] {0}", e.Message);
				Console.WriteLine("[StackTrace] {0}", e.StackTrace);
			}
			finally
			{
				Console.ReadLine();
				host.Close();
			}
		}
	}
}
