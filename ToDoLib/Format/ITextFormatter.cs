using System.Collections.Generic;

namespace ToDoLib.Format
{
  public interface ITextFormatter
  {
    List<FormattedItem> Format(string text);
  }
}
