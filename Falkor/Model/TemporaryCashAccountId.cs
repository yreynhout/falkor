using System;

namespace Falkor.Model
{
  public struct TemporaryCashAccountId : IEquatable<TemporaryCashAccountId>
  {
    private readonly Guid _value;

    public TemporaryCashAccountId(Guid value)
    {
      if(value == Guid.Empty)
        throw new ArgumentException("The value of a temporary account id can not be empty.");
      _value = value;
    }

    public bool Equals(TemporaryCashAccountId other)
    {
      return _value == other._value;
    }

    public override bool Equals (object other)
    {
      if (other == null || GetType() != other.GetType())
      {
        return false;
      }
      return Equals((TemporaryCashAccountId)other);
    }

    public override int GetHashCode()
    {
      return _value.GetHashCode();
    }

    public override string ToString()
    {
      return _value.ToString("N");
    }

    public Guid ToGuid()
    {
      return _value;
    }

    public static bool operator ==(TemporaryCashAccountId instance1, TemporaryCashAccountId instance2)
    {
      return instance1.Equals(instance2);
    }

    public static bool operator !=(TemporaryCashAccountId instance1, TemporaryCashAccountId instance2)
    {
      return !instance1.Equals(instance2);
    }

    public static implicit operator Guid(TemporaryCashAccountId instance)
    {
      return instance._value;
    }
  }
}