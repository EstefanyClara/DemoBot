![UCU](https://github.com/ucudal/PII_Conceptos_De_POO/raw/master/Assets/logo-ucu.png)

### FIT - Universidad Católica del Uruguay

<br>

# Demo de bots de Telegram

### v1

Pequeña demo de un bot de telegram en C#. Este bot responde a los siguientes mensajes de texto:

- "Hola"
- "Chau"
- "Foto"

Para probarlo, clona este repo y crea un nuevo bot en Telegram. Luego, modifica el token en [Program.cs](https://github.com/ucudal/PII_TelegramBot_Demo/blob/master/src/Program/Program.cs#L28) para utilizar el token que te indica el [@BotFather](https://telegram.me/BotFather).

### v2

Ídem v1 pero usando el patrón [Chain of Responsibility](https://refactoring.guru/design-patterns/chain-of-responsibility) para implementar los tres comandos del bot.

### v3

Ídem v2 pero sin usar un bot, es decir, prueba los comandos mediante casos de prueba.