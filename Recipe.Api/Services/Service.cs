using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;


namespace Recipe.API.Services;

public class Service<T> : IService<T> where T : BaseEntity
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<UserApp> _userManager;

    public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<T> CheckUserAccess(int id, string userId)
    {
        if (!typeof(IUserOwnedEntity).IsAssignableFrom(typeof(T)))
            throw new InvalidOperationException("This entity does not support user ownership.");

        var entity = await Where(t => t.Id == id)
            .FirstOrDefaultAsync(e => ((IUserOwnedEntity)e).UserId == userId);

        if (entity == null)
        {
            throw new NotFoundException();
        }
        return entity;
    }

    public async Task<bool> EnsureExistsOrThrow(Expression<Func<T, bool>> expression)
    {
        var isExist = await _repository.AnyAsync(expression);

        if (isExist != true)
        {
            throw new NotFoundException();
        }

        return isExist;
    }

    public async Task<bool> EnsureNotExistsOrThrow(Expression<Func<T, bool>> expression)
    {
        var isExist = await _repository.AnyAsync(expression);

        if (isExist)
        {
            throw new AlreadyDefinedException();
        }

        return isExist;
    }


    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new NotFoundException($"{typeof(T).Name} not found");
        }

        return entity;
    }

    public IQueryable<T> GetAll()
    {
        return _repository.GetAll();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _repository.Where(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await _repository.AnyAsync(expression);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        return entities;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        return entity;

    }

    public async Task RemoveAsync(T entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
    }
}