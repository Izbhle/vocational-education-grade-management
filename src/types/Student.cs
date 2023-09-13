public struct Student
{
    public int Id;
    public string Name;
    public Student(object[] vals)
    {
        Id = (int)(System.Int64)vals[0];
        Name = (string)vals[1];
    }
}