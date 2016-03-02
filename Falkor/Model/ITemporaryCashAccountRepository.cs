namespace Falkor.Model
{
  public interface ITemporaryCashAccountRepository
  {
    TemporaryCashAccount GetMostRecent(TemporaryCashAccountId id);
    TemporaryCashAccount Get(TemporaryCashAccountId id);
    void Add(TemporaryCashAccount instance);
  }
}