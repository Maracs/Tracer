using Core;

namespace Serialization.Abstraction;

public interface ITraceResultSerializer
{
    void Serialize(TraceResult traceResult, Stream to);
    
}