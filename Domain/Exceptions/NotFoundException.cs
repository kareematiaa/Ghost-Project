namespace Domain.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string entityName) :
        base($"{entityName} not found")
    { }
    public NotFoundException(string baseEntity, string childEntity) :
    base($"{baseEntity} doesn't have any {childEntity}")
    { }
    public NotFoundException(string entityName, string messege, bool nothing = false) :
        base($"{entityName}  not found, and the Identiy Messege are ")
    { }
}
