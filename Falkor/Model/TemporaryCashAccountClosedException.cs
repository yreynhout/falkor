using System;

namespace Falkor.Model
{
  public class TemporaryCashAccountClosedException : Exception
  {
    public readonly TemporaryCashAccountId OldTemporaryCashAccountId;
    public readonly TemporaryCashAccountId NewTemporaryCashAccountId;
    public TemporaryCashAccountClosedException(TemporaryCashAccountId oldTemporaryCashAccountId, TemporaryCashAccountId newTemporaryCashAccountId)
    {
      OldTemporaryCashAccountId = oldTemporaryCashAccountId;
      NewTemporaryCashAccountId = newTemporaryCashAccountId;
    }
  }
}