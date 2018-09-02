using System.Collections.Generic;

namespace ToDoLib.Format
{
  public class TextFormatter : ITextFormatter
  {
    private List<ITextFormatter> _formatters;

    public TextFormatter(List<ITextFormatter> formatters)
    {
      _formatters = formatters;
    }

    public List<FormattedItem> Format(string text)
    {
      return FormatWith(text, 0);
    }

    // recursive function to run all formatters
    private List<FormattedItem> FormatWith(string text, int formatterIndex)
    {
      var results = new List<FormattedItem>();

      // if no formatter exists at index just return the text as result
      if (formatterIndex >= _formatters.Count)
      {
        results.Add(new FormattedItem
        {
          Text = text,
          Format = FormatType.None
        });
        return results;
      }

      var items = _formatters[formatterIndex].Format(text);
      foreach (var item in items)
      {
        if (item.Format == FormatType.None)
        {
          results.AddRange(FormatWith(item.Text, formatterIndex + 1));
        }
        else
        {
          results.Add(item);
        }
      }
      return results;
    }
  }
}
