using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserAccountUseCase useCase,
        [FromBody] RequestRegisterUserAccountJson request)
    {
        await useCase.Execute(request);

        return Created();
    }
}
