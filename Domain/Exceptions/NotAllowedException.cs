

namespace Domain.Exceptions
{
    public class NotAllowedException : Exception
    {
        public NotAllowedException(string mess) :
        base(mess)
        { }
        public NotAllowedException (string entityName, string messege): 
        base($"{entityName} already exist, and the Identiy Messege are {messege}")
        {}
    }
}
