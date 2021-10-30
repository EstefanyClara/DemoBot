using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Examples.Echo
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
    /// </summary>
    public class HelloHandler : Handler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HelloHandler"/>. Esta clase procesa el mensaje "hola".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public HelloHandler(Handler next) : base(next)
        {
            // Intencionalmente en blanco
        }

        /// <summary>
        /// Procesa el mensaje "hola" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            if (message.Text.ToLower().Equals("hola"))
            {
                response = "¡Hola! ¿Cómo estás?";
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}