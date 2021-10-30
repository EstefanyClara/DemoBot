using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Examples.Echo
{
    /// <summary>
    /// Clase base para implementar el patrón Chain of Responsibility. En ese patrón se pasa un mensaje a través de una
    /// cadena de "handlers" que pueden procesar o no el mensaje. Cada "handler" decide si procesa el mensaje, o si se lo
    /// pasa al siguiente. Esta clase base implmementa la responsabilidad de recibir el mensaje y pasarlo al siguiente
    /// "handler" en caso que el mensaje no sea procesado. La responsabilidad de decidir si el mensaje se procesa o no, y
    /// de procesarlo, se delega a las clases sucesoras de esta clase base.
    /// </summary>
    public abstract class Handler : IHandler
    {
        /// <summary>
        /// Obtiene el próximo "handler".
        /// </summary>
        /// <value>El "handler" que será invocado si este "handler" no procesa el mensaje.</value>
        public Handler Next { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Handler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public Handler(Handler next)
        {
            this.Next = next;
        }

        /// <summary>
        /// Este método debe ser sobrescrito por las clases sucesores. La clase sucesora procesa el mensaje y retorna
        /// true o no lo procesa y retorna false.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario</returns>
        protected virtual bool InternalHandle(Message message, out string responder)
        {
            throw new InvalidOperationException("Este método debe ser sobrescrito");
        }

        /// <summary>
        /// Procesa el mensaje o la pasa al siguiente "handler" si existe.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        public bool Handle(Message message, out string response)
        {
            bool result = this.InternalHandle(message, out response);
            if (!result && this.Next != null)
            {
                result = this.Next.Handle(message, out response);
            }

            return result;
        }
    }
}