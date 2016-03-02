using System;

namespace Falkor.Messages
{
  public class OpenTemporaryCashAccount
  {
    public readonly Guid TemporaryCashAccountId;
    public readonly Guid OwnerId;

    public OpenTemporaryCashAccount(Guid temporaryCashAccountId, Guid ownerId)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
      OwnerId = ownerId;
    }
  }
}