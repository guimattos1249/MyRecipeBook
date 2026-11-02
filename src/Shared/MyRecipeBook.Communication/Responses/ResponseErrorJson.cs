namespace MyRecipeBook.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> Errors { get; private set; }
    public bool AccessTokenExpired { get; private set; }

    public ResponseErrorJson(List<string> errorMessages) => Errors = errorMessages;

    public ResponseErrorJson(string message) => Errors = [message];

    public ResponseErrorJson(string message, bool accessTokenExpired)
    {
        Errors = [message];
        AccessTokenExpired = accessTokenExpired;
    }
}
