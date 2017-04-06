
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                Assert.That.Throws<ArgumentException>(() => 
                    { throw new NullReferenceException(); }),
                    "Assert.ThrowsException failed. Threw exception NullReferenceException, but exception ArgumentException was expected. \r\nException Message: Object reference not set to an instance of an object.");
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() => 
                Assert.That.Throws<ArgumentException>(a),
                "Assert.ThrowsException failed. Threw exception NullReferenceException, but exception ArgumentException was expected. \r\nException Message: Object reference not set to an instance of an object.");
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldThrowIfExpectedExceptionIsNotThrown()
        {
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                Assert.That.ThrowsInnerException<NullReferenceException>(() => 
                    {
                        throw new ArgumentException("", new FormatException());
                    }),
                    "Could not find Exception of type NullReferenceException in exception chain System.ArgumentException, System.FormatException");
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldThrowIfNoExceptionIsThrown()
        {
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                Assert.That.ThrowsInnerException<NullReferenceException>(() => { }),
                "No exception thrown. NullReferenceException exception was expected");
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() => 
                Assert.That.DoesNotThrow(() => 
                    {
                        throw new NullReferenceException();
                    }),
                    "An unexpected exception \"System.NullReferenceException\" was thrown: Object reference not set to an instance of an object.");
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                Assert.That.DoesNotThrow(action),
                "An unexpected exception \"System.NullReferenceException\" was thrown: Object reference not set to an instance of an object.");
        }
    }
}
