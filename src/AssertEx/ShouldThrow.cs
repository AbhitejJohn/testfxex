
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
        /// <returns>The current instance of the ShouldThrow class so this can be chained with other asserts.</returns>
        public ShouldThrow WithMessage(string message)
        {
            this.VerifyContains("message", this.exception.Message, message);

            return this;
        }

        /// <summary>
        /// Ensures that the exception has exactly the specified message.
        /// </summary>
        /// <param name="message">The expected exception message.</param>
        /// <exception cref="AssertFailedException">
        /// When the message is not exactly similar to the exception message.
        /// </exception>
        /// <returns>The current instance of the ShouldThrow class so this can be chained with other asserts.</returns>
        public ShouldThrow WithExactMessage(string message)
        {
            if(string.Compare(this.exception.Message, message) != 0)
            {
                AssertionFailure.Handle(
                    string.Format(
                        "The exception message \"{0}\" is not equivalent to \"{1}\"",
                        this.exception.Message,
                        message));
            }

            return this;
        }

        /// <summary>
        /// Ensures that the exception contains the specified stack trace.
        /// </summary>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns></returns>
        /// <returns>The current instance of the ShouldThrow class so this can be chained with other asserts.</returns>
        public ShouldThrow WithStackTrace(string stackTrace)
        {
            this.VerifyContains("stack trace", this.exception.StackTrace, stackTrace);

            return this;
        }

        /// <summary>
        /// Ensures that the exception contains an inner exception of the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type of the inner exception.</typeparam>
        /// <returns>The current instance of the ShouldThrow class so this can be chained with other asserts.</returns>
        public ShouldThrow WithInnerException<T>() where T:Exception
        {
            if(this.exception.InnerException == null)
            {
                AssertionFailure.Handle("The inner exception is null.");
            }
            if(!typeof(T).Equals(this.exception.InnerException.GetType()))
            {
                AssertionFailure.Handle(string.Format("The inner exception \"{0}\" is not of type \"{1}\"", this.exception.InnerException.GetType(), typeof(T)));
            }

            return this;
        }

        /// <summary>
        /// Verify if a string contains another string. Throws exception if not.
        /// </summary>
        /// <param name="propertyName">The name of the string/property being verified.</param>
        /// <param name="propertyValue">Its value.</param>
        /// <param name="substring">The substring to verify for.</param>
        private void VerifyContains(string propertyName, string propertyValue, string substring)
        {
            if (propertyValue == null && substring == null)
            {
                return;
            }
            else if (propertyValue == null)
            {
                AssertionFailure.Handle(string.Format("The exception has no {0}", propertyName));
            }
            else if (substring == null)
            {
                AssertionFailure.Handle(string.Format("The exceptions {0} has a non null value.", propertyName));
            }

            if(!propertyValue.Contains(substring))
            {
                AssertionFailure.Handle(string.Format("The exceptions {0} \"{1}\" does not contain \"{2}\"", propertyName, propertyValue, substring));
            }
        }
    }
}
