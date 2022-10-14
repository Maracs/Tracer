namespace Core;

public class ThreadInformation
{
    public int Id { get; set; }

    public long TimeMs { get; set; }
        
    public List<MethodData> Methods { get; set; }
        
    
}