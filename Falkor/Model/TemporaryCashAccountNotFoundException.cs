using System;

namespace Falkor.Model
{
  public class TemporaryCashAccountNotFoundException : Exception
  {
    public readonly TemporaryCashAccountId TemporaryCashAccountId;
    public TemporaryCashAccountNotFoundException(TemporaryCashAccountId temporaryCashAccountId)
    {
      TemporaryCashAccountId = temporaryCashAccountId;
    }
  }
}