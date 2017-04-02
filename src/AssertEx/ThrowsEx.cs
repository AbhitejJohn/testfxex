
namespace MSTest.TestFramework.AssertExtensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Globalization;
    using System.Text;
    
    /// <summary>
    /// Extensions for the MSTest Assertions.
    /// </summary>
    public static class ThrowsEx
    {
        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exception of type 'T'
        /// </summary>
        /// <param name="assert">
        /// The assert class.
        /// </param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception expected to be thrown.
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="action"/> does not throws exception of type <typeparamref name="T"/>.
        /// </exception>
        /// <returns>
        /// The context assertion for further actions.
        /// </returns>
        public static ShouldThrow Throws<T>(this Assert assert, Action action) where T : Exception
        {
            var exception = Assert.ThrowsException<T>(action);

            return new ShouldThrow(exception);
        }

        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exception of type 'T'
        /// </summary>
        /// <param name="assert">
        /// The assert class.
        /// </param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception expected to be thrown.
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="action"/> does not throws exception of type <typeparamref name="T"/>.
        /// </exception>
        /// <returns>
        /// The context assertion for further actions.
        /// </returns>
        public static ShouldThrow Throws<T>(this Assert assert, Func<object> action) where T : Exception
        {
            return Throws<T>(assert, () => { action(); });
        }

        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exact 
        /// given exception/has inner exception of type <typeparamref name="T"/> (and not of derived type).
        /// </summary>
        /// <param name="assert">
        /// The assert class.
        /// </param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception expected to be thrown.
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="action"/> does not throws exception of type <typeparamref name="T"/>.
        /// </exception>
        /// <returns>
        /// The type of exception expected to be thrown.
        /// </returns>
        public static ShouldThrow ThrowsInnerException<T>(this Assert assert, Action action) where T : Exception
        {
            string finalMessage;

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Exception innerException = ex;
                StringBuilder exTypeBuilder = new StringBuilder();

                do
                {
                    var exType = innerException.GetType();
                    if (typeof(T).Equals(exType))
                    {
                        return new ShouldThrow((T)innerException);
                    }
                    if(exTypeBuilder.Length != 0)
                    {
                        exTypeBuilder.Append(", ");
                    }

                    exTypeBuilder.Append(exType);
                    innerException = innerException.InnerException;
                } while (innerException != null);

                finalMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "Could not find Exception of type {0} in exception chain {1}. \r\n Exception Message {2} \r\n Stack Trace {3}",
                        typeof(T).Name,
                    exTypeBuilder.ToString(),
                    ex.Message,
                    ex.StackTrace);

                Assert.Fail("Assert.That.ThrowsInnerException {0}", finalMessage);
            }

            finalMessage = string.Format(
                CultureInfo.CurrentCulture,
                "No exception thrown. {0} exception was expected",
                typeof(T).Name);

            Assert.Fail("Assert.ThrowsException {0}", finalMessage);

            // This will not hit, but need it for compiler.
            return null;
        }

        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exact
        /// exception/has inner exception of type <typeparamref name="T"/> (and not of derived type).
        /// </summary>
        /// <param name="assert">
        /// The assert class.
        /// </param>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception expected to be thrown.
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="action"/> does not throws exception/contain inner-exception of type <typeparamref name="T"/>.
        /// </exception>
        /// <returns>
        /// The type of exception expected to be thrown.
        /// </returns>
        public static ShouldThrow ThrowsInnerException<T>(this Assert assert, Func<object> action) where T : Exception
        {
            return ThrowsInnerException<T>(assert, () => { action(); });
        }

        /// <summary>
        /// Asserts that the delegate does not throw an exception.
        /// </summary>
        /// <param name="assert">The assert class.</param>
        /// <param name="action">Delegate to code to be tested and which is expected to throw exception.</param>
        public static void DoesNotThrow(this Assert assert, Action action)
        {
            try
            {
                action.Invoke();
            }
            catch(Exception exception)
            {
                throw new AssertFailedException("An unexpected exception was thrown ", exception);
            }
        }

        /// <summary>
        /// Asserts that the delegate does not throw an exception.
        /// </summary>
        /// <param name="assert">The assert class.</param>
        /// <param name="action">Delegate to code to be tested and which is expected to throw exception.</param>
        public static void DoesNotThrow(this Assert assert, Func<object> action)
        {
            DoesNotThrow(assert, () => { action(); });
        }
    }
}
