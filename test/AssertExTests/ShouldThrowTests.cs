
namespace AssertExTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MSTest.TestFramework.AssertExtensions;

    [TestClass]
    public class ShouldThrowTests
    {
        [TestMethod]
        public void WithMessageShouldThrowOnNullExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("Something bad."));
            Assert.ThrowsException<AssertFailedException>(() =>
                st.WithMessage(null));
        }

        [TestMethod]
        public void WithMessageShouldThrowIfMessageIsNotPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            Assert.ThrowsException<AssertFailedException>(() => st.WithMessage("Everything is bright."));
        }

        [TestMethod]
        public void WithMessageShouldNotThrowIfMessageIsPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            st.WithMessage("something bombed!");
        }

        [TestMethod]
        public void WithExactMessageShouldThrowIfMessageIsNotEqualToTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            Assert.ThrowsException<AssertFailedException>(() => st.WithExactMessage("something bombed!"));
        }

        [TestMethod]
        public void WithExactMessageShouldNotThrowIfMessageIsPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            st.WithExactMessage("oh no, something bombed!!");
        }
        
        [TestMethod]
        public void WithStackTraceShouldCompareNullExceptionStackTrace()
        {
            var st = new ShouldThrow(new ArgumentException("bombed."));
            st.WithStackTrace(null);
        }

        [TestMethod]
        public void WithStackTraceShouldThrowOnNullExceptionStackTrace()
        {
            var st = new ShouldThrow(new TestableException("Random Location."));
            Assert.ThrowsException<AssertFailedException>(() =>
                st.WithStackTrace(null));
        }

        [TestMethod]
        public void WithStackTraceShouldThrowIfStackTraceIsNotPartOfTheExceptionStackTrace()
        {
            var st = new ShouldThrow(new TestableException("oh no, something bombed!!"));
            Assert.ThrowsException<AssertFailedException>(() => st.WithStackTrace("Everything is bright."));
        }

        [TestMethod]
        public void WithStackTraceShouldNotThrowIfStackTraceIsPartOfTheExceptionStackTrace()
        {
            var st = new ShouldThrow(new TestableException("oh no, something bombed!!"));
            st.WithStackTrace("something bombed!");
        }

        [TestMethod]
        public void WithInnerExceptionShouldThrowIfInnerExceptionIsNull()
        {
            var st = new ShouldThrow(new ArgumentException());
            Assert.ThrowsException<AssertFailedException>(() =>
                st.WithInnerException<NullReferenceException>());
        }

        [TestMethod]
        public void WithInnerExceptionShouldThrowIfInnerExceptionDoesNotMatch()
        {
            var st = new ShouldThrow(new ArgumentException("", new OutOfMemoryException()));
            Assert.ThrowsException<AssertFailedException>(() =>
                st.WithInnerException<NullReferenceException>());
        }

        [TestMethod]
        public void WithInnerExceptionShouldNotThrowIfInnerExceptionMatches()
        {
            var st = new ShouldThrow(new ArgumentException("", new NullReferenceException()));
            st.WithInnerException<NullReferenceException>();
        }
    }

    public class TestableException : Exception
    {
        private string stackTrace;

        public TestableException(string stackTrace)
        {
            this.stackTrace = stackTrace;
        }

        public override string StackTrace
        {
            get
            {
                return this.stackTrace;
            }
        }
    }
}
