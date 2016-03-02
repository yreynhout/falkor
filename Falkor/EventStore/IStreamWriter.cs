using System.Collections.Generic;

namespace Falkor.EventStore
{
  public interface IStreamWriter
  {
    void Append(string stream, IEnumerable<object> messages);
  }
}