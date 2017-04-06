
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                st.WithMessage(null),
                "The exceptions message has a non null value.");
        }

        [TestMethod]
        public void WithMessageShouldThrowIfMessageIsNotPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                st.WithMessage("Everything is bright."),
                "The exceptions message \"oh no, something bombed!!\" does not contain \"Everything is bright.\"");
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() => 
                st.WithExactMessage("something bombed!"),
                "The exception message \"oh no, something bombed!!\" is not equivalent to \"something bombed!\"");
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                st.WithStackTrace(null),
                "The exceptions stack trace has a non null value.");
        }

        [TestMethod]
        public void WithStackTraceShouldThrowIfStackTraceIsNotPartOfTheExceptionStackTrace()
        {
            var st = new ShouldThrow(new TestableException("oh no, something bombed!!"));
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() => 
                st.WithStackTrace("Everything is bright."),
                "The exceptions stack trace \"oh no, something bombed!!\" does not contain \"Everything is bright.\"");
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
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                st.WithInnerException<NullReferenceException>(),
                "The inner exception is null.");
        }

        [TestMethod]
        public void WithInnerExceptionShouldThrowIfInnerExceptionDoesNotMatch()
        {
            var st = new ShouldThrow(new ArgumentException("", new OutOfMemoryException()));
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                st.WithInnerException<NullReferenceException>(),
                "The inner exception \"System.OutOfMemoryException\" is not of type \"System.NullReferenceException\"");
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
