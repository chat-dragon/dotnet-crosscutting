namespace CrossCutting.Domain.SeedWork;

public abstract class Entity : Notifiable
{
    int? _requestedHashCode;

    int _id;
    public virtual int Id
    {
        get
        {
            return _id;
        }
        protected set
        {
            _id = value;
        }
    }

    Guid _uuid;
    public virtual Guid Uuid
    {
        get
        {
            return _uuid;
        }
        protected set
        {
            _uuid = value;
        }
    }

    DateTime _createdOn;
    public virtual DateTime CreatedOn
    {
        get
        {
            return _createdOn;
        }
        protected set
        {
            _createdOn = value;
        }
    }

    DateTime? _updatedOn;
    public virtual DateTime? UpdatedOn
    {
        get
        {
            return _updatedOn;
        }
        protected set
        {
            _updatedOn = value;
        }
    }

    DateTime? _entryOn;
    public virtual DateTime? EntryOn
    {
        get
        {
            return _entryOn;
        }
        protected set
        {
            _entryOn = value;
        }
    }

    private List<IDomainEvent>? _domainEvents;

    protected Entity()
    {
        _createdOn = DateTime.MinValue;
        _entryOn = null;
    }

    protected Entity(DateTime createdOn, DateTime entryOn)
    {
        _createdOn = createdOn;
        _entryOn = entryOn;
    }

    protected Entity(Guid id, DateTime createdOn, DateTime entryOn)
    {
        _uuid = id;
        _createdOn = createdOn;
        _entryOn = entryOn;
    }

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public bool IsTransient()
    {
        return Id == default;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Entity))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return item.Id == Id;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }
    public static bool operator ==(Entity left, Entity right)
    {
        if (Equals(left, null))
            return Equals(right, null) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
