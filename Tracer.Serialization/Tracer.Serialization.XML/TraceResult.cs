using System.Text;
using System.Xml.Serialization;

namespace Serialization.XML;

[XmlRoot(ElementName = "Root")]
public class TraceResult
{
 
    IReadOnlyDictionary<int, List<MethodData>> _traceInfo;
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
        
        
        

        _traceInfo = traceInfo;
    }
    
    
    
    
    

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach(int key in _traceInfo.Keys){
            sb.AppendLine( key.ToString() );
            foreach ( MethodData value in _traceInfo[ key ] )
            {
                sb.Append( value.ClassName + " " );
                sb.Append( value.MethodName + " " );
                sb.Append( value.TimeMs + " " );
            }

        }
        sb.AppendLine( "\n" );
        return sb.ToString();
    }
}