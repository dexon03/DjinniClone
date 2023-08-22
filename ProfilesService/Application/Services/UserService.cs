using AutoMapper;
using Core.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserCreateDto> _createValidator;
    private readonly IValidator<UserUpdateDto> _updateValidator;

    public UserService(IRepository repository, IMapper mapper, IValidator<UserCreateDto> createValidator, IValidator<UserUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<User>().ToListAsync(cancellationToken);
    }

    public async Task<User> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync<User>(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task<User> CreateUser(UserCreateDto user, CancellationToken cancellationToken = default)
    {
        var validationResult = await _createValidator.ValidateAsync(user, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var userEntity = _mapper.Map<User>(user);
        var result = await _repository.CreateAsync(userEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<User> UpdateUser(UserUpdateDto user, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(user, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var userEntity = _mapper.Map<User>(user);
        var isExists = await _repository.AnyAsync<User>(x => x.Id == userEntity.Id);
        if (!isExists)
        {
            throw new Exception("User not found");
        }

        var result = _repository.Update(userEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync<User>(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        _repository.Delete(user);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteManyUsers(User[] users, CancellationToken cancellationToken = default)
    {
        var userEntities = _mapper.Map<List<User>>(users);
        _repository.DeleteRange(userEntities);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
