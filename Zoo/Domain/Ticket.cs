namespace Zoo.Domain
{
    public class Ticket : IIdentity
    {
        public string Id { get; set; }
        public string AttendanceDate { get; set; }
    }
}