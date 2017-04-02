
namespace MSTest.TestFramework.AssertExtensions
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class holds additional functionality that can be validated on an exception.
    /// </summary>
    public class ShouldThrow
    {
        private Exception exception;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="exception">The exception context.</param>
        public ShouldThrow(Exception exception)
        {
            this.exception = exception;
        }

        /// <summary>
        /// The exception Thrown.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return this.exception;
            }
        }

        /// <summary>
        /// Ensures that the exception contains the specified message.
        /// </summary>
        /// <param name="message">The expected exception message.</param>
        /// <exception cref="AssertFailedException">
        /// When the message is not part of the exception message.
        /// </exception>
        public ShouldThrow WithMessage(string message)
        {
            StringAssert.Contains(this.exception.Message, message);

            return this;
        }

        /// <summary>
        /// Ensures that the exception has exactly the specified message.
        /// </summary>
        /// <param name="message">The expected exception message.</param>
        /// <exception cref="AssertFailedException">
        /// When the message is not exactly similar to the exception message.
        /// </exception>
        public ShouldThrow WithExactMessage(string message)
        {
            Assert.AreEqual(this.exception.Message, message);
            return this;
        }

        /// <summary>
        /// Ensures that the exception contains the specified stack trace.
        /// </summary>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns></returns>
        public ShouldThrow WithStackTrace(string stackTrace)
        {
            if(this.exception.StackTrace == null && stackTrace == null)
            {
                return this;
            }
            else if(this.exception.StackTrace == null)
            {
                throw new AssertFailedException("The exception does not have a stack trace.");
            }
            else if(stackTrace == null)
            {
                throw new AssertFailedException("stackTrace is not null.");
            }

            StringAssert.Contains(this.exception.StackTrace, stackTrace);

            return this;
        }

        /// <summary>
        /// Ensures that the exception contains an inner exception of the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type of the inner exception.</typeparam>
        /// <returns></returns>
        public ShouldThrow WithInnerException<T>() where T:Exception
        {
            Assert.IsNotNull(this.exception.InnerException, "Inner Exception is null.");
            Assert.AreEqual(typeof(T), this.exception.InnerException.GetType());

            return this;
        }
    }
}
