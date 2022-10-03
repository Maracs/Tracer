using System.Xml.Schema;
using System.Xml.Serialization;

namespace Serialization.XML;


[XmlRoot(ElementName = "Method")]
public class MethodData
{
    string _methodName;
    string _className;
    long _timeMs;

    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public long TimeMs 
    { 
        get { return _timeMs; } 
        set { _timeMs = value; }
    }
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string MethodName
    {
        get { return _methodName; }			
    }
    [XmlAttribute(Form = XmlSchemaForm.Unqualified)]
    public string ClassName
    {
        get { return _className; }	
    }

    public MethodData(string methodName, string className,long timeMs ){
        _methodName = methodName;

        _className = className;

        _timeMs = timeMs;

    }

    public override bool Equals( object? md )
    {
        if ( md == null ) return false;
        var md1 = (MethodData)md;
        return _methodName == md1._methodName &&
               _className == md1._className;				   
    }

		

}