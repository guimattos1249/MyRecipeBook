using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommomTestUtilities.Requests;

public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build()
    {
        return new Faker<RequestChangePasswordJson>()
            .CustomInstantiator(f =>
                new RequestChangePasswordJson(
                    f.Internet.Password(),
                    f.Internet.Password()));
    }
}
