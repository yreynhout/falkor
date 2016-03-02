using System;

namespace Falkor.Messages
{
  public class TemporaryCashAccountCredited
  {
    public readonly Guid TemporaryCashAccountId;
    public readonly double Amount;

    public TemporaryCashAccountCredited(Guid temporaryCashAccountId, double amount)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
      Amount = amount;
    }
  }
}