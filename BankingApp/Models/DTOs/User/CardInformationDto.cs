namespace BankingApp.Models.DTOs.User
{
    public class CardInformationDto
    {
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string BankName { get; set; }
        public string CardCVV { get; set; }
    }
}
