using Caliburn.Micro;
using Dapplo.LogFacade;
using System;

namespace Dapplo.CaliburnMicro
{
	/// <summary>
	/// A logger for Caliburn
	/// </summary>
	public class CaliburnLogger : ILog
	{
		private readonly LogSource _log;

		public CaliburnLogger(Type type)
		{
			_log = new LogSource(type);
		}
		public void Error(Exception exception)
		{
			_log.Error().WriteLine(exception);
		}

		public void Info(string format, params object[] args)
		{
			_log.Info().WriteLine(format, args);
		}

		public void Warn(string format, params object[] args)
		{
			_log.Warn().WriteLine(format, args);
		}
	}
}
