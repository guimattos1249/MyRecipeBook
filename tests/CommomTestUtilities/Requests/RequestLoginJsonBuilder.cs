using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommomTestUtilities.Requests;

public class RequestLoginJsonBuilder
{
    public static Faker<RequestLoginJson> Build()
    {
        return new Faker<RequestLoginJson>()
            .CustomInstantiator(f =>
                new RequestLoginJson(
                    f.Internet.Email(),
                    f.Internet.Password()));
    }
}
