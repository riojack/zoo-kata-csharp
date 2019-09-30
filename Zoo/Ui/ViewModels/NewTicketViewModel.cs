namespace Zoo.Ui.ViewModels
{
    public class NewTicketViewModel
    {
        public string GuestName { get; set; }
        public string GuestPhone { get; set; }
        public string GuestMailingAddress { get; set; }
        public string DateAttending { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardVerificationValue { get; set; }

        protected bool Equals(NewTicketViewModel other)
        {
            return GuestName == other.GuestName
                   && GuestPhone == other.GuestPhone
                   && GuestMailingAddress == other.GuestMailingAddress
                   && DateAttending == other.DateAttending
                   && CardNumber == other.CardNumber
                   && CardExpirationDate == other.CardExpirationDate
                   && CardVerificationValue == other.CardVerificationValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NewTicketViewModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (GuestName != null ? GuestName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GuestPhone != null ? GuestPhone.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GuestMailingAddress != null ? GuestMailingAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DateAttending != null ? DateAttending.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CardNumber != null ? CardNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CardExpirationDate != null ? CardExpirationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CardVerificationValue != null ? CardVerificationValue.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}