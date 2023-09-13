public struct Grade
{
    public int Id;
    public string Schulnote;
    public string NotenTyp;
    public int SchuelerId;
    public int KursId;
    public string Datum;


    public Grade(object[] vals)
    {
        Id = (int)(System.Int64)vals[0];
        Schulnote = (string)vals[1];
        NotenTyp = (string)vals[2];
        SchuelerId = (int)(System.Int64)vals[3];
        KursId = (int)(System.Int64)vals[4];
        Datum = (string)vals[5];
    }
}