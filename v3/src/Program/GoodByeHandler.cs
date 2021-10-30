using Telegram.Bot.Types;

namespace Telegram.Bot.Examples.Echo
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class GoodByeHandler : Handler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public GoodByeHandler(Handler next) : base(next)
        {
            // Intencionalmente en blanco
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            if (message.Text.ToLower().Equals("chau"))
            {
                response = "¡Chau! ¡Qué andes bien!";
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}