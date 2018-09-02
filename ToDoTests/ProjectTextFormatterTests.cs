using NUnit.Framework;
using ToDoLib.Format;

namespace ToDoTests
{
  [TestFixture]
  public class ProjectTextFormatterTests
  {
    readonly ProjectTextFormatter formatter = new ProjectTextFormatter();

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
    public void FormatProject()
    {
      var results = formatter.Format("+project");
      Assert.AreEqual(1, results.Count);
      //Assert.AreEqual(0, results[0].StartIndex);
      //Assert.AreEqual(7, results[0].EndIndex);
      Assert.AreEqual("+project", results[0].Text);
      Assert.AreEqual(FormatType.Green, results[0].Format);
    }

    [Test]
    public void FormatStringWithProject()
    {
      var results = formatter.Format("one +project test");
      Assert.AreEqual(3, results.Count);

      //Assert.AreEqual(0, results[0].StartIndex);
      //Assert.AreEqual(2, results[0].EndIndex);
      Assert.AreEqual("one", results[0].Text);
      Assert.AreEqual(FormatType.None, results[0].Format);

      //Assert.AreEqual(3, results[1].StartIndex);
      //Assert.AreEqual(11, results[1].EndIndex);
      Assert.AreEqual(" +project", results[1].Text);
      Assert.AreEqual(FormatType.Green, results[1].Format);

      //Assert.AreEqual(12, results[2].StartIndex);
      //Assert.AreEqual(16, results[2].EndIndex);
      Assert.AreEqual(" test", results[2].Text);
      Assert.AreEqual(FormatType.None, results[2].Format);
    }

  }
}
