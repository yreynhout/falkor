using System;

namespace Falkor.Messages
{
  public class CreditTemporaryCashAccount
  {
    public readonly Guid TemporaryCashAccountId;
    public readonly double Amount;

    public CreditTemporaryCashAccount(Guid temporaryCashAccountId, double amount)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
      Amount = amount;
    }
  }
}