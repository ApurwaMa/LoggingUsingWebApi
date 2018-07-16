using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace LoggingUsingWebApi.Controllers
{
    public class LoggingController : ApiController
    {

        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetInfoAfterLoggingToFile(string exception)
        {
            ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            await Task.Run(() => logger.Error(exception));
            return Ok("Logged Error To File Using Log4Net");
        }


        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetInfoLoggingFuntionToEventViewer(string message,string source)
        {
            EventLog m_EventLog = new EventLog("");
            m_EventLog.Source = source;
            await Task.Run(() => m_EventLog.WriteEntry("Reading text file failed " + message,
                EventLogEntryType.FailureAudit));
            return Ok("Logged Error To EventViewer");
        }


        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetInfoUsingFileStream(string strFileName, string strMessage)
        {
            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", Path.GetTempPath(), strFileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                await Task.Run(() => objFilestream.Close());
                return Ok("Logged Error Using FileStream");
            }
            catch (Exception ex)
            {
                return Ok("Exception While Logging Error Using FileStream");
            }
        }

    }
}
