
using System.Text;
using Core;
using Serialization.Abstraction;

namespace Tracer.Serialization;

public class JsonSerializer:ITraceResultSerializer
{
    public void Serialize(TraceResult traceResult, Stream to)
    {
        var result = System.Text.Json.JsonSerializer.Serialize(traceResult);
        
        to.Write(Encoding.Default.GetBytes(result));
    }

    
}