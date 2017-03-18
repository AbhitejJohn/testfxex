
namespace TestRunExTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MSTest.TestFramework.TestRunExtensions;
    using System.Linq;

    [TestClass]
    public class FlakyTestMethodAttributeTests
    {
        private Mock<ITestMethod> mockTestMethod;
        private FlakyTestMethodAttribute flakyTestMethod;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockTestMethod = new Mock<ITestMethod>();
            this.flakyTestMethod = new FlakyTestMethodAttribute(5);
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestOnceIfItPasses()
        {
            this.mockTestMethod.Setup(tm => tm.Invoke(It.IsAny<object[]>())).Returns(new TestResult() { Outcome = UnitTestOutcome.Passed });

            this.flakyTestMethod.Execute(this.mockTestMethod.Object);

            this.mockTestMethod.Verify(tm => tm.Invoke(It.IsAny<object[]>()), Times.Once);
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestMultipleTimesTillItPasses()
        {
            int count = 0;
            this.mockTestMethod.Setup(tm => tm.Invoke(It.IsAny<object[]>()))
                .Returns(() =>
                    {
                        count++;
                        if (count < 3)
                        {
                            return new TestResult() { Outcome = UnitTestOutcome.Failed };
                        }

                        return new TestResult() { Outcome = UnitTestOutcome.Passed };
                    });

            this.flakyTestMethod.Execute(this.mockTestMethod.Object);

            this.mockTestMethod.Verify(tm => tm.Invoke(It.IsAny<object[]>()), Times.Exactly(3));
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestRetryCountNumberOfTimesAndReturnFailureIfNotPassed()
        {
            this.mockTestMethod.Setup(tm => tm.Invoke(It.IsAny<object[]>())).Returns(new TestResult() { Outcome = UnitTestOutcome.Failed });

            var tr = this.flakyTestMethod.Execute(this.mockTestMethod.Object);

            this.mockTestMethod.Verify(tm => tm.Invoke(It.IsAny<object[]>()), Times.Exactly(5));
            Assert.AreEqual(UnitTestOutcome.Failed, tr.FirstOrDefault().Outcome);
        }
    }
}
