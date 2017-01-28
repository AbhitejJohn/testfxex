using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Extensions;

namespace AssertExTests
{
    [TestClass]
    public class AssertExTests
    {
        [TestMethod]
        public void ThrowsExceptionWithMessageShouldPassOnRightMessage()
        {
            var exception = AssertEx.ThrowsExceptionWithMessage<ArgumentException>(() => { throw new ArgumentException("Oh no, something bombed."); }, "something bombed");

            Assert.IsTrue(exception is ArgumentException);
            Assert.AreEqual("Oh no, something bombed.", exception.Message);
        }

        [TestMethod]
        public void ThrowsExceptionWithMessageShouldThrowOnIncorrectMessage()
        {
            Assert.ThrowsException<AssertFailedException>(() => 
                AssertEx.ThrowsExceptionWithMessage<ArgumentException>(() => { throw new ArgumentException("Oh no, something bombed."); }, "Everything is alright"));
        }

        [TestMethod]
        public void ThrowsExceptionWithMessageShouldThrowOnIncorrectException()
        {
            Assert.ThrowsException<AssertFailedException>(() => 
                AssertEx.ThrowsExceptionWithMessage<NullReferenceException>(() => { throw new ArgumentException("Oh no, something bombed."); }, "Everything is alright"));
        }

        [TestMethod]
        public void ThrowsExceptionWithMessageForActionShouldPassOnRightMessage()
        {
            Action action = () => { throw new ArgumentException("Oh no, something bombed."); };
            var exception = AssertEx.ThrowsExceptionWithMessage<ArgumentException>(action, "something bombed");

            Assert.IsTrue(exception is ArgumentException);
            Assert.AreEqual("Oh no, something bombed.", exception.Message);
        }

        [TestMethod]
        public void ThrowsExceptionWithMessageForActionShouldThrowOnIncorrectMessage()
        {
            Action action = () => { throw new ArgumentException("Oh no, something bombed."); };
            Assert.ThrowsException<AssertFailedException>(() =>
                AssertEx.ThrowsExceptionWithMessage<ArgumentException>(action, "Everything is alright"));
        }

        [TestMethod]
        public void ThrowsExceptionWithMessageForActionShouldThrowOnIncorrectException()
        {
            Action action = () => { throw new ArgumentException("Oh no, something bombed."); };
            Assert.ThrowsException<AssertFailedException>(() => 
                AssertEx.ThrowsExceptionWithMessage<NullReferenceException>(action, "Everything is alright"));
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldThrowIfRightExceptionIsNotThrown()
        {
            var exception = Assert.ThrowsException<AssertFailedException>(() =>
                AssertEx.ThrowsInnerException<NullReferenceException>(() => { throw new ArgumentException("", new FormatException()); }));

            StringAssert.Contains(exception.Message, "Could not find Exception of type NullReferenceException in exception chain System.ArgumentException, System.FormatException");
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldThrowIfNoExceptionIsThrown()
        {
            var exception = Assert.ThrowsException<AssertFailedException>(() =>
                AssertEx.ThrowsInnerException<NullReferenceException>(() => { }));

            StringAssert.Contains(exception.Message, "No exception thrown. NullReferenceException exception was expected");
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldPassIfRightExceptionIsThrown()
        {
            var exception = AssertEx.ThrowsInnerException<NullReferenceException>(() => { throw new NullReferenceException("something bombed."); });

            Assert.IsTrue(exception is NullReferenceException);
            Assert.AreEqual("something bombed.", exception.Message);
        }

        [TestMethod]
        public void ThrowsInnerExceptionShouldPassIfRightInnerExceptionIsThrown()
        {
            var exception = AssertEx.ThrowsInnerException<NullReferenceException>(() => { throw new ArgumentException("", new FormatException("",new NullReferenceException("something bombed."))); });

            Assert.IsTrue(exception is NullReferenceException);
            Assert.AreEqual("something bombed.", exception.Message);
        }
    }
}
