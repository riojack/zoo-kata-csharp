using System;

namespace Zoo.Domain
{
    public class Ticket : IIdentity, ICloneable
    {
        public string Id { get; set; }
        public string AttendanceDate { get; set; }

        public object Clone()
        {
            return new Ticket
            {
                Id = Id,
                AttendanceDate = AttendanceDate
            };
        }

        protected bool Equals(Ticket other)
        {
            return Id == other.Id && AttendanceDate == other.AttendanceDate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Ticket) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^ (AttendanceDate != null ? AttendanceDate.GetHashCode() : 0);
            }
        }
    }
}