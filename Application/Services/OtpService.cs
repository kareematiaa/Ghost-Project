using Application.IService;
using Domain.Enums;
using Domain.External;
using Domain.IRepositories.IExternalRepository;

public class OtpService : IOtpService
{
    private readonly IExternalRepository _repository;
    private readonly IOtpRepository _otpRepository;

    public OtpService(IExternalRepository repository, IOtpRepository otpRepository)
    {
        _repository = repository;
        _otpRepository = otpRepository;
    }

    public async Task<string> GenerateAndSendOtp(string email, string purpose)
    {
        var otpCode = await _otpRepository.GenerateOtp(email);

        switch (purpose.ToLower())
        {
            case "confirmation":
                await _repository.MailingRepository.SendConfirmation(email, otpCode);
                break;
            case "resetpassword":
                await _repository.MailingRepository.SendResetPassword(email, otpCode);
                break;
            case "resetphone":
                await _repository.MailingRepository.SendResetPhone(email, otpCode);
                break;
            default:
                await _repository.MailingRepository.SendConfirmation(email, otpCode);
                break;
        }

        return otpCode;
    }

    public async Task<OtpValidationResult> ValidateOtp(string email, string code)
    {
        return await _otpRepository.ValidateOtp(email, code);
    }
}