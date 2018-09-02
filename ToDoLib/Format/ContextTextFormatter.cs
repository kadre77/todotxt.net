﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ToDoLib.Format
{
  public class ContextTextFormatter : ITextFormatter
  {
    
    // contexts start with space character and @ sign
    private static readonly Regex regex = new Regex(@"((^|\s)@[^\s]*)");

    private readonly FormatType format = FormatType.Blue;


    public List<FormattedItem> Format(string text)
    {
      var result = new List<FormattedItem>();

      // Find all URLs using a regular expression
      int lastPos = 0;
      foreach (Match match in regex.Matches(text))
      {
        // Copy raw string from the last position up to the match
        if (match.Index != lastPos)
        {
          result.Add(new FormattedItem
          {
            Text = text.Substring(lastPos, match.Index - lastPos),
            Format = FormatType.None,
          });
        }

        result.Add(new FormattedItem
        {
          Text = text.Substring(match.Index, match.Length),
          Format = format,
        });

        // Update the last matched position
        lastPos = match.Index + match.Length;
      }

      // Finally, copy the remainder of the string
      if (lastPos < text.Length)
      {
        result.Add(new FormattedItem()
        {
          Text = text.Substring(lastPos, text.Length - lastPos),
          Format = FormatType.None,
        });
      }

      return result;

    }

  }
}
