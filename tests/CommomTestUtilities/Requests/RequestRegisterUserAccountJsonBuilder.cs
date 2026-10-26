using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommomTestUtilities.Requests;

public class RequestRegisterUserAccountJsonBuilder
{
    public static RequestRegisterUserAccountJson Build()
    {
        return new Faker<RequestRegisterUserAccountJson>()
            .CustomInstantiator(f =>
                new RequestRegisterUserAccountJson(
                    f.Person.FirstName,
                    f.Internet.Email(),
                    f.Internet.Password()));
    }
}
