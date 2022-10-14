using System.Xml.Schema;
using System.Xml.Serialization;

namespace Serialization.XML;


[XmlRoot(ElementName = "Thread")]
public class ThreadInformation
{
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string Id { get; set; } = "";

    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string TimeMs { get; set; } = "";

    public List<MethodData> Methods { get; set; } = new List<MethodData>();
    
    public ThreadInformation(Core.ThreadInformation coreThreadInformation){
        
        Id = $"{coreThreadInformation.Id}";
        

        TimeMs = $"{coreThreadInformation.TimeMs}ms";

        List<MethodData> Methods = new List<MethodData>();
        
        foreach (var method in coreThreadInformation.Methods)
        {
            Methods.Add(new MethodData(method.MethodName,method.ClassName,method.TimeMs));
        }
        
    }

    public ThreadInformation()
    {
        
    }
}