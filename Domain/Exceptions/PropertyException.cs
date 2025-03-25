namespace Domain.Exceptions;

public class PropertyException : Exception
{
    public PropertyException(string propertyName) :
        base($"{propertyName} is not a property")
    { }
    public PropertyException(string propertyName, string type) :
    base($"{propertyName} is not valid {type}")
    { }
    public override string Message => "Property is invalid";
}
