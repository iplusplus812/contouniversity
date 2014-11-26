using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Logging
{
    public interface ILogger
    {
        void Info(string msg);
        void Info(string fmt, params object[] vars);
        void Info(Exception ex, string fmt, params object[] vars);


        void Warning(string msg);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception ex, string fmt, params object[] vars);


        void Error(string msg);
        void Error(string fmt, params object[] vars);
        void Error(Exception ex, string fmt, params object[] vars);

        void TraceApi(string componentName, string method, TimeSpan timespan);
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);

    }
}
