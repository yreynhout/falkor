using System;

namespace Falkor.Messages
{
  public class TemporaryCashAccountOpened
  {
    public readonly Guid TemporaryCashAccountId;
    public readonly Guid OwnerId;

    public TemporaryCashAccountOpened(Guid temporaryCashAccountId, Guid ownerId)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
      OwnerId = ownerId;
    }
  }
}