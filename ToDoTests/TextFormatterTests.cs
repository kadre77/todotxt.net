using NUnit.Framework;
using System.Collections.Generic;
using ToDoLib.Format;

namespace ToDoTests
{
  [TestFixture]
  class TextFormatterTests
  {
    private readonly TextFormatter formatter = new TextFormatter(new List<ITextFormatter>
    {
      new UrlTextFormatter(),
      new ProjectTextFormatter()
    });

    [Test]
    public void FormatStringWithUrlAndProject()
    {
      var results = formatter.Format("hello +project www.yahoo.com world");
      Assert.AreEqual(5, results.Count);

      Assert.AreEqual("hello", results[0].Text);
      Assert.AreEqual(FormatType.None, results[0].Format);

      Assert.AreEqual(" +project", results[1].Text);
      Assert.AreEqual(FormatType.Green, results[1].Format);

      Assert.AreEqual(" ", results[2].Text);
      Assert.AreEqual(FormatType.None, results[2].Format);

      Assert.AreEqual("www.yahoo.com", results[3].Text);
      Assert.AreEqual(FormatType.Url, results[3].Format);

      Assert.AreEqual(" world", results[4].Text);
      Assert.AreEqual(FormatType.None, results[4].Format);
    }

  }
}
