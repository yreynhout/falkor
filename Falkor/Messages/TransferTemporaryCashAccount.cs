using System;

namespace Falkor.Messages
{
  public class TransferTemporaryCashAccount
  {
    public readonly Guid FromTemporaryCashAccountId;
    public readonly Guid ToTemporaryCashAccountId;
    
    public TransferTemporaryCashAccount(Guid fromTemporaryCashAccountId, Guid toTemporaryCashAccountId)
    {
      FromTemporaryCashAccountId = fromTemporaryCashAccountId;
      ToTemporaryCashAccountId = toTemporaryCashAccountId;
    }
  }
}