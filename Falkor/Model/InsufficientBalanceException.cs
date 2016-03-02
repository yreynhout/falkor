using System;

namespace Falkor.Model
{
  public class InsufficientBalanceException : Exception
  {
    public readonly TemporaryCashAccountId TemporaryCashAccountId;
    public InsufficientBalanceException(TemporaryCashAccountId temporaryCashAccountId)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
    }
  }
}