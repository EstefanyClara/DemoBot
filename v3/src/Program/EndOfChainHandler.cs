using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Examples.Echo
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility diseñado para ser el último.
    /// </summary>
    public class EndOfChainHandler : Handler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. No puede haber un próximo "handler".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public EndOfChainHandler(Handler next) : base(next)
        {
            if (next != null)
            {
                throw new ArgumentException("Este 'handler' debe ser el último.", nameof(next));
            }
        }

        /// <summary>
        /// Procesa todos los mensajes y retorna true siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true siempre.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            response = $"No entiendo el mensaje '{message.Text}'";
            return true;
        }
    }
}