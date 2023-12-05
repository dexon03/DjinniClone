﻿using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contacts;

public interface IVacanciesService
{
    List<VacancyGetAllDto> GetAllVacancies(CancellationToken cancellationToken = default);
    Task<Vacancy> GetVacancyById(Guid id, CancellationToken cancellationToken = default);
    Task<Vacancy> CreateVacancy(VacancyCreateDto vacancyDto, CancellationToken cancellationToken = default);
    Task<Vacancy> UpdateVacancy(VacancyUpdateDto vacancy, CancellationToken cancellationToken = default);
    Task DeleteVacancy(Guid id, CancellationToken cancellationToken = default);
    Task ActivateDeactivateVacancy(Guid id, CancellationToken cancellationToken = default);
}