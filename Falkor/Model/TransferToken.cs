using System;
using System.Linq;

namespace Falkor.Model
{
  public struct TransferToken : IEquatable<TransferToken>
  {
    private readonly byte[] _value;

    public TransferToken(byte[] value)
    {
      if(value == null)
        throw new ArgumentNullException(nameof(value));
      _value = value;
    }

    public bool Equals(TransferToken other)
    {
      var value = other._value;
      if (value == null && _value == null) return true;
      if (value == null || _value == null) return false;
      return value.SequenceEqual(_value);
    }

    public override bool Equals (object other)
    {
      if (other == null || GetType() != other.GetType())
      {
        return false;
      }

      return Equals((TransferToken)other);
    }

    public override int GetHashCode()
    {
      return _value?.Aggregate(0, (current, next) => current ^ next.GetHashCode()) ?? 0;
    }

    public override string ToString()
    {
      return _value == null ?
        "" :
        string.Concat(_value.Select(value => value.ToString("X2")));
    }

    public byte[] ToByteArray()
    {
      return _value;
    }

    public static bool operator ==(TransferToken instance1, TransferToken instance2)
    {
      return instance1.Equals(instance2);
    }

    public static bool operator !=(TransferToken instance1, TransferToken instance2)
    {
      return !instance1.Equals(instance2);
    }

    public static implicit operator byte[](TransferToken instance)
    {
      return instance._value;
    }
  }
}