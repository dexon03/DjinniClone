﻿using System.Linq.Expressions;

namespace Core.Database;

public interface IBaseRepository
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IQueryable<T> GetAll<T>() where T : class;
    int Count<T>(Expression<Func<T, bool>>? predicate = null) where T : class;
    bool Any<T>(Expression<Func<T, bool>>? predicate = null) where T : class;
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>>? predicate = null) where T : class;
    bool All<T>(Expression<Func<T, bool>> predicate) where T : class;
    Task<bool> AllAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
    T Get<T>(Expression<Func<T,bool>> predicate) where T : class;
    Task<T> GetAsync<T>(Expression<Func<T,bool>> predicate) where T : class;
    T? GetById<T>(Guid id) where T : class;
    ValueTask<T?> GetByIdAsync<T>(Guid id) where T : class;
    IQueryable<T> GetByIds<T>(params object[] ids) where T : class;
    T Create<T>(T entity) where T : class;
    Task<T> CreateAsync<T>(T entity) where T : class;
    void CreateRange<T>(params T[] entities) where T : class;
    T Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task DeleteRange<T>(Expression<Func<T, bool>> condition) where T : class;
    void DeleteRange<T>(IEnumerable<T> entities) where T : class;
    Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>>? predicate = null) where T : class;
}