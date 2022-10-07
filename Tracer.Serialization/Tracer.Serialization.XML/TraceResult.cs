using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Serialization.XML;

[XmlRoot(ElementName = "Root")]
public class TraceResult
{
 
    [XmlElement("Dictionary")]
    public List<KeyValuePair<int, List<MethodData>>> XMLDictionaryProxy
    {
        get
        {
            return new List<KeyValuePair<int, List<MethodData>>>(this.TraceInfo);
        }
        set
        {
            this.TraceInfo = new Dictionary<int, List<MethodData>>();
            foreach (var pair in value)
                this.TraceInfo[pair.Key] = pair.Value;
        }
    }

    
    
    [XmlIgnore]
    public Dictionary<int, List<MethodData>> TraceInfo { get; set; }=new();
    
   
    
    
    
    
    
    
    public TraceResult()
    {
        
    }
    
   // Core.TraceResult traceResult
    public TraceResult(  Core.TraceResult coreTraceInfo )
    {
       
        
        Dictionary<int, List<MethodData>> traceInfo = new Dictionary<int, List<MethodData>>();
        
        foreach (KeyValuePair<int, List<Core.MethodData>> valuePair in coreTraceInfo.TraceInfo)
        {
            
            List<MethodData> methodsData = new List<MethodData>();
            
            foreach (var value in valuePair.Value)
            {
                MethodData methodData = new MethodData(value.MethodName,value.ClassName,value.TimeMs);
                methodsData.Add(methodData);
            }
            
            traceInfo.Add(valuePair.Key,methodsData);
            
        }
        
        
        

        TraceInfo = traceInfo;
    }
    
}