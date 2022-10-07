using System.Xml.Schema;
using System.Xml.Serialization;

namespace Serialization.XML;


[XmlRoot(ElementName = "Method")]
public class MethodData
{


    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string TimeMs { get; set; } = "";

    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string MethodName { get; set; } = "";

    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string ClassName { get; set; } = "";

    public MethodData(string methodName, string className,long timeMs ){
        MethodName = methodName;

        ClassName = className;

        TimeMs = $"{timeMs}ms";

    }

    public MethodData()
    {
        
    }

		

}