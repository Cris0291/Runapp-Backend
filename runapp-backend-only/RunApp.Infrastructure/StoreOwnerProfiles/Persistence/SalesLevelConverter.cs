using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RunApp.Domain.StoreOwnerProfileAggregate.ValueTypes;

namespace RunApp.Infrastructure.StoreOwnerProfiles.Persistence
{
     class SalesLevelConverter : ValueConverter<SalesLevel, string>
    {
        public SalesLevelConverter() : base
            (
            salesEnum => salesEnum.Name,
            Name => SalesLevel.FromName(Name, false) 
            ) { }
    }
}
