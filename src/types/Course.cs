public struct Course
{
    public int Id;
    public string Name;
    public int KlasseId;
    public int LehrerId;
    public string Schuljahr;


    public Course(object[] vals)
    {
        Id = (int)(System.Int64)vals[0];
        Name = (string)vals[1];
        KlasseId = (int)(System.Int64)vals[2];
        LehrerId = (int)(System.Int64)vals[3];
        Schuljahr = (string)vals[4];
    }
}