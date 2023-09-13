public struct Teacher
{
    public int Id;
    public string Name;
    public Teacher(object[] vals)
    {
        Id = (int)(System.Int64)vals[0];
        Name = (string)vals[1];
    }
}