using System;

namespace Falkor.Model
{
  public struct OwnerId : IEquatable<OwnerId>
  {
    private readonly Guid _value;

    public OwnerId(Guid value)
    {
      if(value == Guid.Empty)
        throw new ArgumentException("The value of an owner id can not be empty.");
      _value = value;
    }

    public bool Equals(OwnerId other)
    {
      return _value == other._value;
    }

    public override bool Equals (object other)
    {
      if (other == null || GetType() != other.GetType())
      {
        return false;
      }
      return Equals((OwnerId)other);
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

    public static bool operator ==(OwnerId instance1, OwnerId instance2)
    {
      return instance1.Equals(instance2);
    }

    public static bool operator !=(OwnerId instance1, OwnerId instance2)
    {
      return !instance1.Equals(instance2);
    }

    public static implicit operator Guid(OwnerId instance)
    {
      return instance._value;
    }
  }
}