using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
	public class Audit : IDisposable
	{

		private static EventLog customLog = null;
		const string SourceName = "Manager.Audit";
		const string LogName = "Proba";

		static Audit()
		{
			try
			{
				if (!EventLog.SourceExists(SourceName))
				{
					EventLog.CreateEventSource(SourceName, LogName);
				}
				customLog = new EventLog(LogName, Environment.MachineName, SourceName);
			}
			catch (Exception e)
			{
				customLog = null;
				Console.WriteLine("Error while trying to create log handle. Error = {0}\n", e.Message);
			}
		}

		public static void AuthenticationSuccess(string userName)
		{
			if (customLog != null)
			{
				string UserAuthenticationSuccess = AuditEvents.AuthenticationSuccess;
				string message = String.Format(UserAuthenticationSuccess, userName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.AuthenticationSuccess));
			}
		}

		public static void AuthorizationSuccess(string userName, string serviceName)
		{
			if (customLog != null)
			{
				string AuthorizationSuccess = AuditEvents.AuthorizationSuccess;
				string message = String.Format(AuthorizationSuccess, userName, serviceName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.AuthorizationSuccess));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="serviceName"> should be read from the OperationContext as follows: OperationContext.Current.IncomingMessageHeaders.Action</param>
		/// <param name="reason">permission name</param>
		public static void AuthorizationFailure(string userName, string serviceName, string reason)
		{
			if (customLog != null)
			{
				string AuthorizationFailure = AuditEvents.AuthorizationFailure;
				string message = String.Format(AuthorizationFailure, userName, serviceName, reason);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.AuthorizationFailure));
			}
		}

		public static void AddingToDatabaseSuccess(string userName, string serviceName)
		{
			if (customLog != null)
			{
				string AddingToDatabaseSuccess = AuditEvents.AddingToDatabaseSuccess;
				string message = String.Format(AddingToDatabaseSuccess, userName, serviceName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.AddingToDatabaseSuccess));
			}
		}
		public static void AddingToDatabaseFailure(string userName, string serviceName, string reason)
		{
			if (customLog != null)
			{
				string AddingToDatabaseFailure = AuditEvents.AddingToDatabaseFailure;
				string message = String.Format(AddingToDatabaseFailure, userName, serviceName, reason);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.AddingToDatabaseFailure));
			}
		}

		public void Dispose()
		{
			if (customLog != null)
			{
				customLog.Dispose();
				customLog = null;
			}
		}
	}
}
