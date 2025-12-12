using BankingApp.Contracts.Entities;

namespace BankingApp.Models.Entities
{
    public class CardInformation : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string BankName { get; set; }
        public string CardCVV { get; set; }

        public CardInformation(Guid userId, string cardHolder, string cardNumber, string cardCVV, string expiry, string bankName)
        {
            UserId = userId;
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            CardCVV = cardCVV;
            Expiry = expiry;
            BankName = bankName;

        }

        public CardInformation(string cardHolder, string cardNumber, string cardCVV, string expiry, string bankName)
        {
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            CardCVV = cardCVV;
            Expiry = expiry;
            BankName = bankName;
            DateCreated = DateTime.UtcNow;

        }
    }
}
