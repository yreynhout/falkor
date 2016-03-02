using System;

namespace Falkor.Messages
{
  public class TemporaryCashAccountTransfered
  {
    public readonly Guid NewTemporaryCashAccountId;
    public readonly Guid OldTemporaryCashAccountId;
    public readonly Guid OwnerId;
    public readonly double Balance;
    public readonly byte[] TransferToken;

    public TemporaryCashAccountTransfered(Guid newTemporaryCashAccountId, Guid oldTemporaryCashAccountId, Guid ownerId, double amount, byte[] transferToken)
    {
      NewTemporaryCashAccountId = newTemporaryCashAccountId;
      OldTemporaryCashAccountId = oldTemporaryCashAccountId;
      OwnerId = ownerId;
      Balance = amount;
      TransferToken = transferToken;
    }
  }
}