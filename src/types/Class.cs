public struct Class
{
    public int Id;
    public string Name;
    public Class(object[] vals)
    {
        Id = (int)(System.Int64)vals[0];
        Name = (string)vals[1];
    }
}