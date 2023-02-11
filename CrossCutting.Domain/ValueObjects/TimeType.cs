using CrossCutting.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Domain.ValueObjects;

public class TimeType : Enumeration
{
    public static TimeType Equipe = new TimeType("EQU", nameof(Equipe));
    public static TimeType Grupo = new TimeType("GRO", nameof(Grupo));
    public static TimeType Associacao = new TimeType("ASS", "Associação");
    public TimeType(string id, string nome) : base(id, nome)
    {
    }
    protected TimeType()
    {
    }
    public override string ToString()
    {
        return Id;
    }
    public static IEnumerable<TimeType> List() => new[] { Equipe, Grupo, Associacao };

    public static TimeType From(string id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new ArgumentException($"Possible values for TimeType: {String.Join(",", List().Select(s => $"{s.Id} - {s.Name}"))}");
        }

        return state;
    }

    public static implicit operator TimeType(string id)
    {
        return From(id);
    }
}
