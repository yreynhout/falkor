using System.Collections.Generic;

namespace Falkor.EventStore
{
  public interface IStreamReader
  {
    IEnumerable<object> ReadForward(string stream);
    IEnumerable<object> ReadBackward(string stream);
  }
}