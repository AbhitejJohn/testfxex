
namespace AssertExTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MSTest.TestFramework.AssertExtensions;

    [TestClass]
    public class ThrowsExTests
    {
        [TestMethod]
        public void ThrowsShouldNotThrowIfExpectedExceptionIsThrown()
        {
            Assert.That.Throws<ArgumentException>(() => { throw new ArgumentException(); });
        }

        [TestMethod]
        public void ThrowsShouldThrowIfUnexpectedExceptionIsThrown()
        {
            Assert.ThrowsException<AssertFailedException>(() =>
                Assert.That.Throws<ArgumentException>(() => 
                    { throw new NullReferenceException(); }));
        }

        [TestMethod]
        public void ThrowsForActionShouldNotThrowIfExpectedExceptionIsThrown()
        {
            Action a = () => { throw new ArgumentException(); };
            Assert.That.Throws<ArgumentException>(a);
        }

        [TestMethod]
        public void ThrowsForActionShouldThrowIfUnexpectedExceptionIsThrown()
        {
            Action a = () => { throw new NullReferenceException(); };
            Assert.ThrowsException<AssertFailedException>(() => 
                Assert.That.Throws<ArgumentException>(a));
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldThrowIfExpectedExceptionIsNotThrown()
        {
            var exception = Assert.ThrowsException<AssertFailedException>(() =>
                Assert.That.ThrowsInnerException<NullReferenceException>(() => 
                    {
                        throw new ArgumentException("", new FormatException());
                    }));

            StringAssert.Contains(exception.Message, "Could not find Exception of type NullReferenceException in exception chain System.ArgumentException, System.FormatException");
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldThrowIfNoExceptionIsThrown()
        {
            var exception = Assert.ThrowsException<AssertFailedException>(() =>
                Assert.That.ThrowsInnerException<NullReferenceException>(() => { }));

            StringAssert.Contains(exception.Message, "No exception thrown. NullReferenceException exception was expected");
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldPassIfRightExceptionIsThrown()
        {
            Assert.That.ThrowsInnerException<NullReferenceException>(() => { throw new NullReferenceException("something bombed."); });
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldPassIfRightInnerExceptionIsThrown()
        {
            Assert.That.ThrowsInnerException<NullReferenceException>(() => { throw new ArgumentException("", new FormatException("",new NullReferenceException("something bombed."))); });
        }

        [TestMethod]
        public void DoesNotThrowShouldNotThrowIfNoExcpetionIsThrown()
        {
            Assert.That.DoesNotThrow(() => { });
        }

        [TestMethod]
        public void DoesNotThrowShouldThrowIfAnExcpetionIsThrown()
        {
            Assert.ThrowsException<AssertFailedException>(() => 
                Assert.That.DoesNotThrow(() => 
                    {
                        throw new NullReferenceException();
                    }));
        }

        [TestMethod]
        public void DoesNotThrowWithActionShouldNotThrowIfNoExcpetionIsThrown()
        {
            Action action = () => { };
            Assert.That.DoesNotThrow(action);
        }

        [TestMethod]
        public void DoesNotThrowWithActionShouldThrowIfAnExcpetionIsThrown()
        {
            Action action = () => throw new NullReferenceException();
            Assert.ThrowsException<AssertFailedException>(() =>
                Assert.That.DoesNotThrow(action));
        }
    }
}
