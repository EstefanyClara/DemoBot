using NUnit.Framework;
using Telegram.Bot.Examples.Echo;
using Telegram.Bot.Types;

namespace ProgramTests
{
    public class HelloHandlerTests
    {
        IHandler handler;
        Message message;

        [SetUp]
        public void Setup()
        {
            handler = new HelloHandler(null);
            message = new Message();
        }

        [Test]
        public void TestHandle()
        {
            message.Text = "hola";
            string response;

            handler.Handle(message, out response);

            Assert.That(response, Is.EqualTo("¡Hola! ¿Cómo estás?"));
        }

        [Test]
        public void TestDoesNotHandle()
        {
            message.Text = "adios";
            string response;

            bool result = handler.Handle(message, out response);

            Assert.That(result, Is.False);
            Assert.That(response, Is.Empty);
        }
    }
}