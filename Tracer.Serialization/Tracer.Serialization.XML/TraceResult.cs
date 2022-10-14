using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using Core;

namespace Serialization.XML;

    [XmlRoot(ElementName = "Root")]
    public class TraceResult
    {

        [XmlElement("Dictionary")]
        public List<KeyValuePair<int, ThreadInformation>> XMLDictionaryProxy
        {
            get { return new List<KeyValuePair<int, ThreadInformation>>(this.TraceInfo); }
            set
            {
                this.TraceInfo = new Dictionary<int, ThreadInformation>();
                foreach (var pair in value)
                    this.TraceInfo[pair.Key] = pair.Value;
            }
        }



        [XmlIgnore] public Dictionary<int, ThreadInformation> TraceInfo { get; set; } = new();




        
        public TraceResult()
        {

        }
        public TraceResult(Core.TraceResult coreTraceInfo)
        {


            Dictionary<int, ThreadInformation> traceInfo = new Dictionary<int, ThreadInformation>();

            foreach (KeyValuePair<int, Core.ThreadInformation> valuePair in coreTraceInfo.TraceInfo)
            {

                ThreadInformation methodData = new ThreadInformation(valuePair.Value);


               

                traceInfo.Add(valuePair.Key, methodData);

            }

            
            TraceInfo = traceInfo;
        }

    }
