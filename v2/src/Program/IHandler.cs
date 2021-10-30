using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Examples.Echo
{
    /// <summary>
    /// Interfaz para implementar el patrón Chain of Responsibility. En ese patrón se pasa un mensaje a través de una
    /// cadena de "handlers" que pueden procesar o no el mensaje. Cada "handler" decide si procesa el mensaje, o si se lo
    /// pasa al siguiente. Esta interfaz define un atributo para definir el próximo "handler" y una una operación para
    /// recibir el mensaje y pasarlo al siguiente "handler" en caso que el mensaje no sea procesado. La responsabilidad de
    /// decidir si el mensaje se procesa o no, y de procesarlo, se realiza en las clases que implementan esta interfaz.
    /// <remarks>
    /// La interfaz se crea en función del principio de inversión de dependencias, para que los clientes de la cadena de
    /// responsabilidad, que pueden ser concretos, no dependan de una clase "handler" que potencialmente es abstracta.
    /// <remarks/>
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Obtiene el próximo "handler".
        /// </summary>
        /// <value>El "handler" que será invocado si este "handler" no procesa el mensaje.</value>
        Handler Next { get; set; }

        /// <summary>
        /// Procesa el mensaje o la pasa al siguiente "handler" si existe.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        bool Handle(Message message, out string response);
    }
}