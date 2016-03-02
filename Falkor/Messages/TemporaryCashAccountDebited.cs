using System;

namespace Falkor.Messages
{
  public class TemporaryCashAccountDebited
  {
    public readonly Guid TemporaryCashAccountId;
    public readonly double Amount;

    public TemporaryCashAccountDebited(Guid temporaryCashAccountId, double amount)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
      Amount = amount;
    }
  }

  public class DebitTemporaryCashAccount
  {
    public readonly Guid TemporaryCashAccountId;
    public readonly double Amount;

    public DebitTemporaryCashAccount(Guid temporaryCashAccountId, double amount)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
      Amount = amount;
    }
  }
}