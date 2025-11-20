namespace BankingApp.Models.DTOs.Bank
{
    public class CreateBankRequestModel
    {
        public string Name { get; set; }
        public BankBranch BankBranch { get; set; }
        public string Description { get; set; }
    }
}
