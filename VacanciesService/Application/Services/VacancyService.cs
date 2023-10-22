using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace VacanciesService.Application.Services;

public class VacancyService : IVacanciesService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<VacancyCreateDto> _createValidator;
    private readonly IValidator<VacancyUpdateDto> _updateValidator;

    public VacancyService(IRepository repository, 
        IMapper mapper, 
        IValidator<VacancyCreateDto> createValidator, 
        IValidator<VacancyUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public List<VacancyGetAllDto> GetAllVacancies(CancellationToken cancellationToken = default)
    {
        var vacancies = 
                (from vacancy in _repository.GetAll<Vacancy>().Where(v => v.IsActive) 
                join locationVacancy in _repository.GetAll<LocationVacancy>() 
                    on vacancy.Id equals locationVacancy.VacancyId
                join location in _repository.GetAll<Location>()
                    on locationVacancy.LocationId equals location.Id
                join company in _repository.GetAll<Company>()
                    on vacancy.CompanyId equals company.Id
                select new
                {
                    vacancy.Id,
                    vacancy.Title,
                    vacancy.PositionTitle,
                    vacancy.Salary,
                    vacancy.CreatedAt,
                    vacancy.Experience,
                    vacancy.AttendanceMode,
                    vacancy.Description,
                    company.Name,
                    location.City,
                    location.Country
                })
                .ToList()
                .GroupBy(v => new
                {
                    v.Id,
                    v.Title,
                    v.PositionTitle,
                    v.Salary,
                    v.Experience,
                    v.CreatedAt,
                    v.AttendanceMode,
                    v.Description,
                    v.Name,
                }).Select(g => new VacancyGetAllDto
                {
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    PositionTitle = g.Key.PositionTitle,
                    Salary = g.Key.Salary,
                    Experience = g.Key.Experience,
                    CreatedAt = g.Key.CreatedAt,
                    AttendanceMode = g.Key.AttendanceMode,
                    Description = g.Key.Description,
                    CompanyName = g.Key.Name,
                    Locations = g.Select(l => new LocationGetDto
                    {
                        City = l.City,
                        Country = l.Country
                    })
                });
        return vacancies.ToList();
    }

    public async Task<Vacancy> GetVacancyById(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }

        return vacancy;
    }

    public async Task<Vacancy> CreateVacancy(VacancyCreateDto vacancy, CancellationToken cancellationToken = default)
    {
        var validationResult =  await _createValidator.ValidateAsync(vacancy,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var vacancyEntity = _mapper.Map<Vacancy>(vacancy);
        vacancyEntity.CreatedAt = DateTime.Now;
        var result = await _repository.CreateAsync(vacancyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;

    }

    public async Task<Vacancy> UpdateVacancy(VacancyUpdateDto vacancy, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(vacancy,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var vacancyEntity = _mapper.Map<Vacancy>(vacancy);
        var isExists = await _repository.AnyAsync<Vacancy>(x => x.Id == vacancyEntity.Id);
        if (!isExists)
        {
            throw new ExceptionWithStatusCode("Vacancy that you trying to update, not exist", HttpStatusCode.BadRequest);
        }
        vacancyEntity.UpdatedAt = DateTime.Now;
        var result = _repository.Update(vacancyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteVacancy(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        _repository.Delete(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task ActivateDeactivateVacancy(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        vacancy.IsActive = !vacancy.IsActive;
        vacancy.UpdatedAt = DateTime.Now;
        _repository.Update(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}