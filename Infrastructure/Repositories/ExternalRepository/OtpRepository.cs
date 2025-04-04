using Domain.Enums;
using Domain.External;
using Microsoft.Extensions.Caching.Memory;

public class OtpRepository : IOtpRepository
{
    private readonly IMemoryCache _cache;
    private const int OtpExpirationMinutes = 2;

    public OtpRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<string> GenerateOtp(string email)
    {
        // Generate random 6-digit OTP
        var random = new Random();
        var otpCode = random.Next(100000, 999999).ToString();

        // Store in cache with expiration
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(OtpExpirationMinutes));

        // Store both code and validation state
        _cache.Set(email, new OtpData
        {
            Code = otpCode,
            IsUsed = false
        }, cacheEntryOptions);

        return Task.FromResult(otpCode);
    }

    public Task<OtpValidationResult> ValidateOtp(string email, string code)
    {
        if (!_cache.TryGetValue(email, out OtpData otpData))
        {
            return Task.FromResult(OtpValidationResult.NotFound);
        }

        if (otpData.IsUsed)
        {
            return Task.FromResult(OtpValidationResult.AlreadyUsed);
        }

        if (!string.Equals(otpData.Code, code, StringComparison.Ordinal))
        {
            return Task.FromResult(OtpValidationResult.InvalidCode);
        }

        // Mark as used only on successful validation
        otpData.IsUsed = true;
        _cache.Set(email, otpData);

        return Task.FromResult(OtpValidationResult.Success);
    }

    private class OtpData
    {
        public string Code { get; set; }
        public bool IsUsed { get; set; }
    }
}

