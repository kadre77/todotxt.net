using NUnit.Framework;
using ToDoLib.Format;

namespace ToDoTests
{
  [TestFixture]
  class UrlTextFormatterTests
  {
    UrlTextFormatter formatter = new UrlTextFormatter();

    [Test]
    public void FormatSimpleString()
    {
      var results = formatter.Format("hello");
      Assert.AreEqual(1, results.Count);
      //Assert.AreEqual(0, results[0].StartIndex);
      //Assert.AreEqual(4, results[0].EndIndex);
      Assert.AreEqual("hello", results[0].Text);
      Assert.AreEqual(FormatType.None, results[0].Format);
    }

    [Test]
    public void FormatEmptyString()
    {
      var results = formatter.Format("");
      Assert.AreEqual(0, results.Count);
    }

    [Test]
    public void FormatSimpleUrl()
    {
      var results = formatter.Format("www.google.com");
      Assert.AreEqual(1, results.Count);
      //Assert.AreEqual(0, results[0].StartIndex);
      //Assert.AreEqual(13, results[0].EndIndex);
      Assert.AreEqual("www.google.com", results[0].Text);
      Assert.AreEqual(FormatType.Url, results[0].Format);
    }

    [Test]
    public void FormatUrlAndString()
    {
      var results = formatter.Format("go to www.google.com now");
      Assert.AreEqual(3, results.Count);

      //Assert.AreEqual(0, results[0].StartIndex);
      //Assert.AreEqual(5, results[0].EndIndex);
      Assert.AreEqual("go to ", results[0].Text);
      Assert.AreEqual(FormatType.None, results[0].Format);

      //Assert.AreEqual(6, results[1].StartIndex);
      //Assert.AreEqual(19, results[1].EndIndex);
      Assert.AreEqual("www.google.com", results[1].Text);
      Assert.AreEqual(FormatType.Url, results[1].Format);

      //Assert.AreEqual(20, results[2].StartIndex);
      //Assert.AreEqual(23, results[2].EndIndex);
      Assert.AreEqual(" now", results[2].Text);
      Assert.AreEqual(FormatType.None, results[2].Format);
    }
  }
}
