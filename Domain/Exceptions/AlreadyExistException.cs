namespace Domain.Exceptions;

public sealed class AlreadyExistException : Exception
{
    public AlreadyExistException(string entityName) :
    base($"{entityName} already exist")
    { }

    public AlreadyExistException(string entityName, string messege): 
        base($"{entityName} already exist, and the Identiy Messege are {messege}")
    {

    }

}
