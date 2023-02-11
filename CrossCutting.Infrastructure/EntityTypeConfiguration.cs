using CrossCutting.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace CrossCutting.Infrastructure;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Entity, IAggregateRoot
{
    private readonly string _name;
    private readonly string? _schema;
    private readonly string _keyColumnName;
    private readonly ILogger _logger;
    private readonly bool _mapCreatedOn;
    private readonly bool _mapUpdateOn;

    public EntityTypeConfiguration(string name, string? schema, ILogger logger, bool mapCreatedOn = true, bool mapUpdateOn = true, string keyColumnName = "Id")
    {
        this._name = name;
        this._schema = schema;
        this._keyColumnName = keyColumnName;
        this._logger = logger;
        this._mapCreatedOn = mapCreatedOn;
        this._mapUpdateOn = mapUpdateOn;
    }

    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(_name, _schema);

        string keyPropertyName = typeof(T).IsAssignableTo(typeof(IAggregateIntRoot)) ? nameof(Entity.Id) : nameof(Entity.Uuid);
        string ignorePropertyName = typeof(T).IsAssignableTo(typeof(IAggregateIntRoot)) ? nameof(Entity.Uuid) : nameof(Entity.Id);

        _logger.LogInformation("EntityTypeConfiguration iniciou {0} {1} {2} {3}", _name, _schema, keyPropertyName, ignorePropertyName);

        builder.HasKey(keyPropertyName);
        builder
            .Property(keyPropertyName)
            .HasColumnName(_keyColumnName)
            .ValueGeneratedOnAdd();
        builder.Ignore(ignorePropertyName);

        if (_mapCreatedOn)
            builder
                .Property(b => b.CreatedOn)
                //.HasColumnType("datetime")
                .HasColumnName("CreatedOn")
                //.ValueGeneratedOnAdd()
                .IsRequired(true);
        else builder.Ignore(b => b.CreatedOn);

        if (_mapUpdateOn)
            builder
                .Property(b => b.UpdatedOn)
                //.HasColumnType("datetime")
                .HasColumnName("UpdatedOn")
                .ValueGeneratedOnUpdate()
                .IsRequired(false);
        else builder.Ignore(b => b.UpdatedOn);

        builder.Ignore(b => b.IsValid);
        builder.Ignore(b => b.DomainEvents);
        builder.Ignore(b => b.Notifications);

        CustomConfigure(builder);

        _logger.LogInformation("EntityTypeConfiguration terminou {0} {1}", _name, _schema);
    }

    protected virtual void CustomConfigure(EntityTypeBuilder<T> entity)
    {
    }
}

