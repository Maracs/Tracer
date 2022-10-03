using Core;
using Serialization.Abstraction;
using System.Text;
using YamlDotNet.Serialization;

namespace Serialization.YAML;

public class YamlSerializer:ITraceResultSerializer
{
    public void Serialize(TraceResult traceResult, Stream to)
    {
        var serializer = new SerializerBuilder().DisableAliases().Build();
        var result = serializer.Serialize(traceResult);
        to.Write(Encoding.UTF8.GetBytes(result));
    }
}