using System;

namespace Falkor.Messages
{
  public class CloseTemporaryCashAccount
  {
    public readonly Guid CloseAccountId;
    public readonly Guid TransferAccountId;
    public CloseTemporaryCashAccount(Guid closeAccountId, Guid transferAccountId)
    {
      CloseAccountId = closeAccountId;
      TransferAccountId = transferAccountId;
    }
  }
}