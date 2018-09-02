using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ToDoLib.Format;

namespace Client
{
	/// <summary>
	/// An attached property for TextBlocks to allow URLs to be clickable.
	/// </summary>
	/// <remarks>From http://stackoverflow.com/questions/861409/wpf-making-hyperlinks-clickable </remarks>
	public static class TextFormatService
	{
		// Copied from http://flanders.co.nz/2009/11/08/a-good-url-regular-expression-repost/
		private static readonly Regex ReUrl = new Regex(@"(?:(())(www\.([^/?#\s]*))|((http(s)?|ftp):)(//([^/?#\s]*)))([^?#\s]*)(\?([^#\s]*))?(#([^\s]*))?");

		public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
			"Text",
			typeof(string),
			typeof(TextFormatService),
			new PropertyMetadata(null, OnTextChanged)
		);

		public static string GetText(DependencyObject d)
		{ return d.GetValue(TextProperty) as string; }

		public static void SetText(DependencyObject d, string value)
		{ d.SetValue(TextProperty, value); }

		private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var textBlock = d as TextBlock;
			if (textBlock == null)
				return;

			textBlock.Inlines.Clear();

			var newText = (string)e.NewValue;
			if (string.IsNullOrEmpty(newText))
				return;

      TextFormatter textFormatter = new TextFormatter(new List<ITextFormatter>() {
        new UrlTextFormatter(),
        new ProjectTextFormatter(),
        new ContextTextFormatter(),
        new PriorityTextFormatter()
      });

      List<FormattedItem> items = textFormatter.Format(newText);

      foreach (var item in items)
      {
        switch (item.Format) {
          case FormatType.None:
            AddRawTextInline(textBlock, item.Text);
            break;
          case FormatType.Url:
            AddUrlInline(textBlock, item.Text);
            break;
          case FormatType.Green:
            AddGreenTextInline(textBlock, item.Text);
            break;
          case FormatType.Blue:
            AddBlueTextInline(textBlock, item.Text);
            break;
          case FormatType.Bold:
            AddBoldTextInline(textBlock, item.Text);
            break;
          default:
            throw new NotImplementedException($"{item.Format} not implemented");
        }
      }

		}

    private static void AddRawTextInline(TextBlock textBlock, string text)
    {
      if (string.IsNullOrEmpty(text))
        return;

      textBlock.Inlines.Add(new Run(text));
    }

    private static void AddUrlInline(TextBlock textBlock, string text)
    {
      if (string.IsNullOrEmpty(text)) return;
      
      // Create a hyperlink for the match
      var uri = text.StartsWith("www.") ? string.Format("http://{0}", text) : text;

      // in case the url is not correct
      if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
      {
        AddRawTextInline(textBlock, text);
        return;
      }

      var link = new Hyperlink(new Run(text))
      {
        // If it starts with "www." add "http://" to make it a valid Uri
        NavigateUri = new Uri(uri),
        ToolTip = uri
      };

      link.Click += OnUrlClick;

      textBlock.Inlines.Add(link);

    }

    private static void AddGreenTextInline(TextBlock textBlock, string text)
    {
      if (string.IsNullOrEmpty(text))
        return;

      textBlock.Inlines.Add(new Run(text)
      {
        Foreground = Brushes.Green
      });
    }

    private static void AddBlueTextInline(TextBlock textBlock, string text)
    {
      if (string.IsNullOrEmpty(text))
        return;

      textBlock.Inlines.Add(new Run(text)
      {
        Foreground = Brushes.Blue
      });
    }

    private static void AddBoldTextInline(TextBlock textBlock, string text)
    {
      if (string.IsNullOrEmpty(text)) return;

      textBlock.Inlines.Add(new Run(text)
      {
        FontWeight = FontWeights.Bold
      });
    }

		private static void OnUrlClick(object sender, RoutedEventArgs e)
		{
			var link = (Hyperlink)sender;
			// Do something with link.NavigateUri like:
			Process.Start(link.NavigateUri.ToString());
		}
	}
}