using System;

namespace Falkor.Messages
{
  public class TemporaryCashAccountClosed
  {
    public readonly Guid OldTemporaryCashAccountId;
    public readonly Guid NewTemporaryCashAccountId;
    public TemporaryCashAccountClosed(Guid oldTemporaryCashAccountId, Guid newTemporaryCashAccountId)
    {
      OldTemporaryCashAccountId = oldTemporaryCashAccountId;
      NewTemporaryCashAccountId = newTemporaryCashAccountId;
    }
  }
}