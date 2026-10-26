using MyRecipeBook.Communication.Requests;

namespace CommomTestUtilities.Requests;

public class RequestRegisterUserAccountJsonBuilder
{
    public static RequestRegisterUserAccountJson Build()
    {
        return new RequestRegisterUserAccountJson("John Doe", "johndoe@gmail.com", "123456789");
    }
}
