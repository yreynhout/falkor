using System;

namespace Falkor.Model
{
  public class TemporaryCashAccountTransferTokenMismatchException : Exception
  {
    public readonly TemporaryCashAccountId OldTemporaryCashAccountId;
    public readonly TemporaryCashAccountId NewTemporaryCashAccountId;
    public TemporaryCashAccountTransferTokenMismatchException(TemporaryCashAccountId oldTemporaryCashAccountId, TemporaryCashAccountId newTemporaryCashAccountId)
    {
      OldTemporaryCashAccountId = oldTemporaryCashAccountId;
      NewTemporaryCashAccountId = newTemporaryCashAccountId;
    }
  }
}