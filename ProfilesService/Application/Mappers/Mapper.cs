using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Mappers;

public static class Mapper
{
    public static void MapProfileUpdateDto<TUpdateDto, TEntity>(TUpdateDto updateDto, TEntity entity) where TUpdateDto : IProfileUpdateDto<TEntity>
    {
        foreach (var property in updateDto.GetType().GetProperties())
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(updateDto);

            var entityProperty = entity.GetType().GetProperty(propertyName);
            if (entityProperty != null)
            {
                entityProperty.SetValue(entity, propertyValue);
            }
        }
    }
}