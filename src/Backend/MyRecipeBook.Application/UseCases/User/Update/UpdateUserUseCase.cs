using FluentValidation.Results;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Identity;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Update;

public class UpdateUserUseCase(
    ILoggedUser loggedUser, 
    IUserReadOnlyRepository userReadOnlyRepository, 
    IUserUpdateOnlyRepository userUpdateOnlyRepository, 
    IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();

        await ValidateAsync(request, loggedUser);

        loggedUser.Name = request.Name;
        loggedUser.Email = request.Email;

        _userUpdateOnlyRepository.UpdateProfile(loggedUser);

        await _unitOfWork.Commit();
    }

    public async Task ValidateAsync(RequestUpdateUserJson request, Domain.Entities.User loggedUser)
    {
        var result = new UpdateUserValidator().Validate(request);

        if (loggedUser.Email.Equals(request.Email) == false)
        {
            var existsUserWithEmail = await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);
            if (existsUserWithEmail)
                result.Errors.Add(new ValidationFailure("email", ResourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid == false)
            throw new ErrorOnValidationException([.. result.Errors.Select(e => e.ErrorMessage)]);
    }
}
