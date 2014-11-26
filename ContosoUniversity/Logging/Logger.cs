using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace ContosoUniversity.Logging
{
    public class Logger : ILogger 
    {
        public void Info(string msg)
        {
            Trace.TraceInformation(msg);
        }

        public void Info(string fmt, params object[] vars)
        {
            Trace.TraceInformation(fmt, vars);
        }

        public void Info(Exception ex, string fmt, params object[] vars)
        {
            Trace.TraceInformation(FormatExceptionMessage(ex, fmt, vars));
        }

        public void Warning(string msg)
        {
            Trace.TraceWarning(msg);
        }

        public void Warning(string fmt, params object[] vars)
        {
            Trace.TraceWarning(fmt, vars);
        }

        public void Warning(Exception ex, string fmt, params object[] vars)
        {
            Trace.TraceWarning(FormatExceptionMessage(ex, fmt, vars));
        }

        public void Error(string msg)
        {
            Trace.TraceError(msg);
        }

        public void Error(string fmt, params object[] vars)
        {
            Trace.TraceError(fmt, vars);
        }

        public void Error(Exception ex, string fmt, params object[] vars)
        {
            Trace.TraceError(FormatExceptionMessage(ex, fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string msg = string.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            Trace.TraceInformation(msg);
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }

        private static string FormatExceptionMessage(Exception ex, string fmt, object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(ex.ToString());
            return sb.ToString();
        }
    }
}