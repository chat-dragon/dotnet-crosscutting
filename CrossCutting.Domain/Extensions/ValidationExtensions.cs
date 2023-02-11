using CrossCutting.Domain.SeedWork;
using CrossCutting.Domain.Support;
using CrossCutting.Domain.ValueObjects;
using FluentValidation;

namespace CrossCutting.Domain.Extensions;

public static class ValidationExtensions
{
    public static AbstractValidator<string> StringRequired(string propertyName)
    {
        var i = new InlineValidator<string>();
        i.RuleFor(x => x)
            .NotEmpty()
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<IList<T>> CollectionNotEmpty<T>(string propertyName)
    {
        var i = new InlineValidator<IList<T>>();
        i.RuleFor(x => x)
            .NotEmpty()
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<int> IntNotNegativeOrZero(string propertyName)
    {
        var i = new InlineValidator<int>();
        i.RuleFor(x => x)
            .GreaterThanOrEqualTo(0)
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<DateTime> DateTimeLessThanOrEqualToLimit(string propertyName, DateTime limit)
    {
        var i = new InlineValidator<DateTime>();
        i.RuleFor(x => x)
            .NotEmpty()
            .LessThanOrEqualTo(limit)
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<DateOnly> DateGreaterThanOrEqualToLimit(string propertyName, DateOnly limit)
    {
        var i = new InlineValidator<DateOnly>();
        i.RuleFor(x => x)
            .NotEmpty()
            .GreaterThanOrEqualTo(limit)
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<Guid> GuidNotEmpty(string propertyName)
    {
        var i = new InlineValidator<Guid>();
        i.RuleFor(x => x)
            .NotEmpty()
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<string> EmailEndereco(string propertyName)
    {
        var i = new InlineValidator<string>();
        i.RuleFor(x => x)
            .NotEmpty()
            .EmailAddress()
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<NumeroTelefone> Telefone()
    {
        var i = new InlineValidator<NumeroTelefone>();
        i.RuleFor(x => x.DDD)
            .NotEmpty()
            ;
        i.RuleFor(x => x.Numero1)
            .Length(4)
            ;
        i.RuleFor(x => x.Numero2)
            .Length(4)
            ;
        return i;
    }

    public static AbstractValidator<NumeroTelefone> Celular()
    {
        var i = new InlineValidator<NumeroTelefone>();
        i.RuleFor(x => x.DDD)
            .NotEmpty()
            ;
        i.RuleFor(x => x.Numero1)
            .NotEmpty()
            .Length(5)
            ;
        i.RuleFor(x => x.Numero2)
            .NotEmpty()
            .Length(4)
            ;
        return i;
    }

    public static AbstractValidator<string> FixLen(int len, string propertyName, string errorMessage)
    {
        var i = new InlineValidator<string>();
        i.RuleFor(x => x)
            .Length(len)
            .WithName(propertyName)
            .WithMessage(errorMessage); ;
        return i;
    }

    public static AbstractValidator<Notifiable> MustValid(string propertyName)
    {
        var i = new InlineValidator<Notifiable>();
        i.RuleFor(x => x)
            .NotNull()
            .Must(n => n.IsValid)
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<Notifiable> NotNull(string propertyName)
    {
        var i = new InlineValidator<Notifiable>();
        i.RuleFor(x => x)
            .NotNull()
            .WithName(propertyName);
        return i;
    }

    public static AbstractValidator<string> Cpf(string propertyName)
    {
        var i = new InlineValidator<string>();
        i.RuleFor(x => x)
            .NotEmpty()
            .Must(cpf => Utils.IsCpf(cpf))
            .WithName(propertyName)
            ;
        return i;
    }
}

