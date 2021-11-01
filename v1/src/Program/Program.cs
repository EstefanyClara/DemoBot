using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un programa que implementa un bot de Telegram.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// La instancia del bot.
        /// </summary>
        private static TelegramBotClient Bot;

        /// <summary>
        /// El token provisto por Telegram al crear el bot.
        /// *Importante*:
        /// Para probar este ejemplo, crea un bot nuevo y reemplaza este token por el de tu bot.
        /// </summary>
        private static string Token = "";

        /// <summary>
        /// Punto de entrada al programa.
        /// </summary>
        public static void Main()
        {
            Bot = new TelegramBotClient(Token);
            var cts = new CancellationTokenSource();

            // Comenzamos a escuchar mensajes. Esto se hace en otro hilo (en background). El primer método
            // HandleUpdateAsync es invocado por el bot cuando se recibe un mensaje. El segundo método HandleErrorAsync
            // es invocado cuando ocurre un error.
            Bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token
            );

            Console.WriteLine($"Bot is up!");

            // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
            Console.ReadLine();

            // Terminamos el bot.
            cts.Cancel();
        }

        /// <summary>
        /// Maneja las actualizaciones del bot (todo lo que llega), incluyendo mensajes, ediciones de mensajes,
        /// respuestas a botones, etc. En este ejemplo sólo manejamos mensajes de texto.
        /// </summary>
        public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Sólo respondemos a mensajes de texto
                if (update.Type == UpdateType.Message)
                {
                    await HandleMessageReceived(update.Message);
                }
            }
            catch(Exception e)
            {
                await HandleErrorAsync(e, cancellationToken);
            }
        }

        /// <summary>
        /// Maneja los mensajes que se envían al bot.
        /// Lo único que hacemos por ahora es escuchar 3 tipos de mensajes:
        /// - "hola": responde con texto
        /// - "chau": responde con texto
        /// - "foto": responde con una foto
        /// </summary>
        /// <param name="message">El mensaje recibido</param>
        /// <returns></returns>
        private static async Task HandleMessageReceived(Message message)
        {
            Console.WriteLine($"Received a message from {message.From.FirstName} saying: {message.Text}");

            string response;

            switch(message.Text.ToLower().Trim())
            {
                case "hola":
                    response = "¡Hola! ¿Cómo estás?";
                    break;

                case "chau":
                    response = "¡Chau! ¡Qué andes bien!";
                    break;

                case "foto":
                    // Si nos piden una foto, mandamos la foto en vez de responder con un mensaje de texto.
                    await SendProfileImage(message);
                    return;

                default:
                    response = "Disculpa, ¡no se qué hacer con ese mensaje!";
                    break;
            }

            // Enviamos el texto de respuesta
            await Bot.SendTextMessageAsync(message.Chat.Id, response);
        }

        /// <summary>
        /// Envía una imagen como respuesta al mensaje recibido. Como ejemplo enviamos siempre la misma foto.
        /// </summary>
        static async Task SendProfileImage(Message message)
        {
            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

            const string filePath = @"profile.jpeg";
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();
            await Bot.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new InputOnlineFile(fileStream, fileName),
                caption: "Te ves bien!"
            );
        }

        /// <summary>
        /// Manejo de excepciones. Por ahora simplemente la imprimimos en la consola.
        /// </summary>
        public static Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}