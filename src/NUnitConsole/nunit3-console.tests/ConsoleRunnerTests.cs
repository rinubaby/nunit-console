using System;
using System.IO;
using System.Xml;
using NSubstitute;
using NUnit.Common;
using NUnit.Engine;
using NUnit.Engine.Extensibility;
using NUnit.Engine.Services;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NUnit.ConsoleRunner.Tests
{
    class ConsoleRunnerTests
    {
        [Test]
        public void ThrowsNUnitEngineExceptionWhenTestResultsAreNotWriteable()
        {
            var testEngine = new TestEngine();

            testEngine.Services.Add(new FakeResultService());
            testEngine.Services.Add(new TestFilterService());
            testEngine.Services.Add(Substitute.For<IService, IExtensionService>());

            var consoleRunner = new ConsoleRunner(testEngine, new ConsoleOptions("mock-assembly.dll"), new ColorConsoleWriter());

            var ex = Assert.Throws<NUnitEngineException>(() => { consoleRunner.Execute(); });
            Assert.That(ex.Message, Is.EqualTo("The path specified in --result TestResult.xml could not be written to"));
        }

        [Test]
        public void ExistsMinus5OnAppDomainUnloadError()
        {
            int returnCode = 0;

            using (new TestExecutionContext.IsolatedContext())
            {
                var testEngine = new TestEngine();
                testEngine.Initialize();

                var fakeDomainService = Substitute.ForPartsOf<DomainManager>();
                fakeDomainService.When(x => x.Unload(Arg.Any<AppDomain>())).Do(x =>
                {
                    throw new NUnitEngineException($"Exception from {nameof(ExistsMinus5OnAppDomainUnloadError)}", new CannotUnloadAppDomainException());
                });
                testEngine.Services.ServiceManager.SubstituteService(fakeDomainService);

                //Tested inprocess as out of process Agent has it's own DomainManager which hasn't been substituted
                //TestWriter faked out to not interfere with actual test console output
                var fakeTestWriter = Substitute.For<ExtendedTextWrapper>(TextWriter.Null);
                var consoleRunner = new ConsoleRunner(testEngine, new ConsoleOptions("mock-assembly.dll", "--test=NUnit.Tests.Singletons.OneTestCase", "--inprocess"), fakeTestWriter);
                returnCode = consoleRunner.Execute();
            }
            Assert.That(returnCode, Is.EqualTo(ConsoleRunner.APPDOMAIN_UNLOAD_EXCEPTION));
        }
    }

    internal class FakeResultService : Service, IResultService
    {
        public string[] Formats
        {
            get
            {
                return new[] { "nunit3" };
            }
        }

        public IResultWriter GetResultWriter(string format, object[] args)
        {
            return new FakeResultWriter();
        }
    }

    internal class FakeResultWriter : IResultWriter
    {
        public void CheckWritability(string outputPath)
        {
            throw new UnauthorizedAccessException();
        }

        public void WriteResultFile(XmlNode resultNode, string outputPath)
        {
            throw new System.NotImplementedException();
        }

        public void WriteResultFile(XmlNode resultNode, TextWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
