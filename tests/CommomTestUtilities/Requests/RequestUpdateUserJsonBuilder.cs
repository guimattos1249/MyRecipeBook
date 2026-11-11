using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommomTestUtilities.Requests;

public class RequestUpdateUserJsonBuilder
{
    public static RequestUpdateUserJson Build()
    {
        return new Faker<RequestUpdateUserJson>()
            .CustomInstantiator(f =>
                new RequestUpdateUserJson(
                    f.Person.FullName,
                    f.Internet.Email()));
    }
}
