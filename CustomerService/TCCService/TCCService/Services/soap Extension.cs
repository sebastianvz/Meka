using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services.Protocols;

namespace TCCService.Services
{
    public class SoapLoggerExtension: SoapExtension
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Stream oldStream;
        private Stream newStream;

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override object GetInitializer(Type serviceType)
        {
            return null;
        }

        public override void Initialize(object initializer)
        {

        }

        public override System.IO.Stream ChainStream(System.IO.Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        public override void ProcessMessage(SoapMessage message)
        {

            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    Log(message, "AfterSerialize");
                    CopyStream(newStream, oldStream);
                    newStream.Position = 0;
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    CopyStream(oldStream, newStream);
                    Log(message, "BeforeDeserialize");
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
            }
        }

        public void Log(SoapMessage message, string stage)
        {

            newStream.Position = 0;
            string contents = (message is SoapServerMessage) ? "SoapRequest " : "SoapResponse ";
            contents += stage + ";";

            StreamReader reader = new StreamReader(newStream);

            contents += reader.ReadToEnd();

            newStream.Position = 0;

            log.Debug(contents);
        }

        void ReturnStream()
        {
            CopyAndReverse(newStream, oldStream);
        }

        void ReceiveStream()
        {
            CopyAndReverse(newStream, oldStream);
        }

        public void ReverseIncomingStream()
        {
            ReverseStream(newStream);
        }

        public void ReverseOutgoingStream()
        {
            ReverseStream(newStream);
        }

        public void ReverseStream(Stream stream)
        {
            TextReader tr = new StreamReader(stream);
            string str = tr.ReadToEnd();
            char[] data = str.ToCharArray();
            Array.Reverse(data);
            string strReversed = new string(data);

            TextWriter tw = new StreamWriter(stream);
            stream.Position = 0;
            tw.Write(strReversed);
            tw.Flush();
        }
        void CopyAndReverse(Stream from, Stream to)
        {
            TextReader tr = new StreamReader(from);
            TextWriter tw = new StreamWriter(to);

            string str = tr.ReadToEnd();
            char[] data = str.ToCharArray();
            Array.Reverse(data);
            string strReversed = new string(data);
            tw.Write(strReversed);
            tw.Flush();
        }

        private void CopyStream(Stream fromStream, Stream toStream)
        {
            try
            {
                StreamReader sr = new StreamReader(fromStream);
                StreamWriter sw = new StreamWriter(toStream);
                sw.WriteLine(sr.ReadToEnd());
                sw.Flush();
            }
            catch (Exception ex)
            {
                string message = String.Format("CopyStream failed because: {0}", ex.Message);
                log.Error(message, ex);
            }
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class SoapLoggerExtensionAttribute : SoapExtensionAttribute
    {
        private int priority = 1;

        public override int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public override System.Type ExtensionType
        {
            get { return typeof(SoapLoggerExtension); }
        }
    }
}