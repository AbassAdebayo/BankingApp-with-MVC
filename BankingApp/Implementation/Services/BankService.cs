using BankingApp.Interface.Repositories;
using BankingApp.Interface.Services;
using BankingApp.Models;
using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.Bank;
using BankingApp.Models.Entities;

namespace BankingApp.Implementation.Services
{
    public class BankService(IBankRepository bankRepository, ILogger<BankService> logger) : IBankService
    {
        private readonly IBankRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
        private readonly ILogger<BankService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<BaseResponse<bool>> CreateBankAsync(CreateBankRequestModel request)
        {
            var bankExists = await _bankRepository.BankExistsByBranchName(request.BankBranch);
            if (bankExists)
            {
                _logger.LogError($"Bank already exist in {request.BankBranch}");
                return new BaseResponse<bool>
                {
                    Message = $"Bank already exist in {request.BankBranch}",
                    Status = false
                };
            }



            var newBank = new Bank(request.Name, request.BankBranch, request.Description);
            var createBank = await _bankRepository.Add(newBank);

            if (createBank == null)
            {
                _logger.LogError("Bank couldn't be created");
                return new BaseResponse<bool>
                {
                    Message = "Bank couldn't be created",
                    Status = false
                };
            }
            _logger.LogInformation("Bank creation successful");
            return new BaseResponse<bool>
            {
                Message = "Bank creation successful",
                Status = true
            };
        }

        public async Task<BaseResponse<bool>> Delete(Guid id)
        {
            var bankToDelete = await _bankRepository.GetById(id);
            if (bankToDelete == null)
            {
                _logger.LogError("Bank doesn't exist");
                return new BaseResponse<bool>
                {
                    Message = "Bank doesn't exist",
                    Status = false
                };
            }
            var deleteBank = _bankRepository.Delete(bankToDelete);
            if (deleteBank == null)
            {
                _logger.LogError("Bank couldn't be deleted");
                return new BaseResponse<bool>
                {
                    Message = "Bank couldn't be deleted",
                    Status = false
                };
            }

            _logger.LogInformation("Bank deletion successful");
            return new BaseResponse<bool>
            {
                Message = "Bank deletion successful",
                Status = true
            };
        }

        public async Task<BaseResponse<List<BankDto>>> GetAllBanksAsync()
        {
            var banks = await _bankRepository.ListOfBanks();
            if (!banks.Any())
            {
                _logger.LogError("No bank found");
                return new BaseResponse<List<BankDto>>
                {
                    Message = "No bank found",
                    Status = false
                };
            }

            return new BaseResponse<List<BankDto>>
            {
                Message = "Banks fetched successfully",
                Status = true,
                Data = banks.Select(b => new BankDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    BankBranch = b.BankBranch,
                    DateCreated = b.DateCreated,
                }).ToList()
            };
        }

        public Task<BaseResponse<BankDto>> GetBankByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<BankDto>>> ListOfBanksByBranchNameAsync(BankBranch bankBranch)
        {
            throw new NotImplementedException();
        }
    }
}
