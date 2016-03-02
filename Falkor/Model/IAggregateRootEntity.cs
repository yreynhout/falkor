namespace Falkor.Model
{
  public interface IAggregateRootEntity
  {
    Router Router { get; }
    Recorder Recorder { get; }
  }
}