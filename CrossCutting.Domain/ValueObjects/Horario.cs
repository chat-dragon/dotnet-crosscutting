using CrossCutting.Domain.SeedWork;
using CrossCutting.Domain.Support;

namespace CrossCutting.Domain.ValueObjects;

public class Horario : ValueObject
{
    private readonly TimeOnly _horainicio;
    private readonly TimeOnly _horafim;
    private readonly DayOfWeek? _diainicio;
    private readonly DayOfWeek? _diafim;

    public Horario(DayOfWeek diasemana, TimeOnly horainicio, TimeOnly horafim)
    {
        _horainicio = horainicio;
        _horafim = horafim;

        _diainicio = diasemana;
        _diafim = diasemana;

        if (horafim < horainicio)
        {
            _diafim = Utils.CircularIncrement<DayOfWeek>(0, DayOfWeek.Saturday, diasemana, 1);
        }
    }

    public Horario(TimeOnly horainicio, TimeOnly horafim)
    {
        _horainicio = horainicio;
        _horafim = horafim;
    }

    public TimeOnly HoraInicio => _horainicio;

    public TimeOnly HoraFim => _horafim;

    public DayOfWeek? DiaInicio => _diainicio;

    public DayOfWeek? DiaFim => _diafim;

    public static bool HasConflicted(Horario a, Horario b)
    {
        if (a.DiaInicio == b.DiaInicio)
        {
            return b.HoraInicio.IsBetween(a.HoraInicio, a.HoraFim);
        }
        else if (a.HoraInicio > a.HoraFim)
        {
            return a.HoraFim.IsBetween(b.HoraInicio, b.HoraInicio <= b.HoraFim ? b.HoraFim : TimeOnly.Parse("23:59"));
        }
        return false;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"Inicio hora {_horainicio} dia {_diainicio} - Fim hora {_horafim} dia {_diafim}";
    }
}
