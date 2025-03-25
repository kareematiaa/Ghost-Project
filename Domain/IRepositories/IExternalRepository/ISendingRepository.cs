namespace Domain.External
{
    /// <summary>
    /// Define what will be sent to users regardless of the method 
    /// by which it is sent (mail, phone, etc.)
    /// <code>user</code> represent Email or Phone of the User
    /// <para>It is not required to implement all methods in all classes that implement this 
    /// <seealso cref="ISendingRepository"/> </para>
    /// </summary>
    public interface ISendingRepository
    {
        Task SendConfirmation(string user, string otp);
        Task SendResetPassword(string user, string otp);
        Task SendResetPhone(string user, string otp);
        Task SendResetEmail(string user, string otp);
    }
}
