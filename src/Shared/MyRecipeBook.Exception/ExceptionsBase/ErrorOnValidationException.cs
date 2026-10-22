namespace MyRecipeBook.Exception.ExceptionsBase;

public class ErrorOnValidationException : MyRecipeBookException
{
    private readonly List<string> _errors;
    public ErrorOnValidationException(List<string> messages)
    {
        _errors = messages;
    }

    public List<string> GetErrorMessages() => _errors;
}
