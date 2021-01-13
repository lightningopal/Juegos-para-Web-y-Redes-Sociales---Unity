# Juegos-para-Web-y-Redes-Sociales---Unity

## **Integrantes del grupo**

| Nombres y apellidos | Correo de la universidad | Nombre de usuario GitHub |
| :---------: | :---------: | :---------: |
| Mario Belén Rivera | m.belen.2017@alumnos.urjc.es | Kratser|
| Enrique Sánchez de Francisco | e.sanchezd.2017@alumnos.urjc.es | enriuop|
| Mireya Funke Prieto | m.funke.2017@alumnos.urjc.es | mfpheras|
| Sergio Cruz Serrano | s.cruzs.2017@alumnos.urjc.es | Sergypulga|
| Samuel Ríos Carlos | s.rios.2017@alumnos.urjc.es | Thund3rDev|

* * *

# **GDD**

## **1. Introducción**

Documento de diseño del videojuego *Marshallow: Pilferage in YolkTown*. Videojuego 3D desarrollado en *Unity* para navegadores web. A continuación, explicaremos todas las características y elementos del juego. 

### **1.1. Título del juego**

*Marshallow: Pilferage in YolkTown*. Como pequeña justificación sobre el título, cabe decir que *Marhsallow* es un juego de palabras entre marshal (una figura de la ley que está por encima del sheriff) y *marshmallow* (un dulce típico de EEUU más bien conocido en nuestro país como malvavisco o nube), esto es debido a que el protagonista es un personaje con forma de nube y ejerce como marshal. Por otra parte, *pilferage* significa hurto, dado que en el juego hay un ladrón que se dedica a robar a los aldeanos del pueblo en el que se sitúa nuestro juego. Y finalmente, *YolkTown* traducido literalmente al español es Pueblo Yema, ya que los aldeanos tienen forma de ovoide. 

**Fig 1**

### **1.2. Concepto del juego**

*Marshallow: Pilferage in YolkTown* es un juego 3D para un jugador de puzles e investigación ambientado en un pueblo en fiestas. Entre todos los aldeanos de este pueblo se encuentra un ladrón, tu deber como marshal, deberá ser encontrarle y detenerle. Para ello deberás hablar con los aldeanos que han sido víctimas o testigos de un robo, ellos te darán detalles sobre la apariencia del ladrón. Pero cuidado, deberás fijarte bien en los aldeanos, porque comparten accesorios y características con el ladrón y además algunos te darán pistas dudosas.  

### **1.3. Historia introductoria**

Como todos los años, el pueblo *YolkTown* celebra sus fiestas de otoño. Durante estos días todos sus habitantes salen a la calle a disfrutar y divertirse luciendo sus originales accesorios. Desafortunadamente, alguien se está dedicando a sabotear la festividad robando a la gente. Será el deber de *Marshallow*, el agente de la ley del pueblo, encontrar al ladrón con ayuda de los aldeanos para así, garantizar la seguridad de estos. 

### **1.4. Características principales**

  * **Re-jugable:** todas las partidas son diferentes, ya que los aldeanos siempre cambian de características y sus acciones (moverse, dar información, interactuar con el ladrón) varían según avanza la partida. 
  * **Entretenido:**  el juego presenta una situación en la que tienes que recorrer un pueblo buscando características, detalles y aldeanos con los que interactuar. Esto ofrece una experiencia que pretende mantener la atención y entretener al jugador.  
  * ***Responsive:*** al ser un juego que se ejecuta en navegador, lo hemos implementado de tal forma que se adapte independientemente de la resolución del dispositivo en el que se encuentre. 
  * **Familiar o grupal:** aunque es un juego para un jugador, cuenta con la opción de resolver la partida con ayuda de otras personas desde el mismo dispositivo, es decir, varios ojos pueden ayudar a resolver el misterio de forma más sencilla. 

### **1.5. Género**

**Puzles-Investigación:**  durante una partida de *Marshallow: Pilferage in YolkTown* el jugador debe reunir las pistas proporcionadas por los aldeanos y contrastarlas con su propia información, para de esta forma resolver el puzle de descubrir la verdadera identidad del ladrón.  

### **1.6. Antecedentes**

Para el ambiente, el movimiento y el estilo general del juego hemos tomado como referentes juegos como: *Animal Crossing, Captain Toad Tressure Tracker* y *Mario 3D World. 

**Fig 2**

### **1.7. Propósito y público objetivo**

El objetivo del juego es conseguir una experiencia divertida y tranquila en la que los usuarios desconecten y se enfrenten al reto de resolver un misterio. 

Queremos que el juego sea accesible para un amplio rango de usuarios, al ser un juego que se puede jugar en el navegador desde un ordenador o un dispositivo móvil de forma gratuita, estamos dando la opción a los jugadores de poder jugar sin necesidad de tener una consola o un ordenador potente y no tener que ahorrar para comprar el juego.  

Principalmente está más enfocado a un público infantil, ya que existe la posibilidad de jugar en familia desde el mismo dispositivo para que los niños afiancen distintos términos mediante los iconos y accesorios del juego. Pero no está dedicado única y exclusivamente para niños, sino que también resultará llamativo a personas curiosas y analíticas que disfruten de los videojuegos, los rompecabezas o los juegos de mesa. En cuanto a las habilidades que debe tener el jugador de base, cabe destacar que solo será necesario el uso del ratón o el dedo (en el caso de dispositivos móviles) para jugar, por lo que no requerirá excesiva habilidad por parte del usuario para poder manejarse. 

Como ya hemos comentado anteriormente, el juego está planteado para jugar de forma individual, pero también nos gustaría acercarnos a pequeños grupos de amigos o familias, ya que existe la posibilidad de colaborar desde el mismo dispositivo para resolver el juego en conjunto. 

En cuanto a la clasificación por edades según la normativa europea, nuestro juego estaría catalogado como PEGI 3, es decir, para personas mayores de 3 años. Además de esto, habría que añadir el descriptor de contenido de *In-Game Purchases*. 

### **1.8. Estilo visual**
 
Queremos que *Marshallow: Pilferage in YolkTown* tenga un estilo visual sencillo y cartoon. En general se hará uso de formas redondeadas y suaves con colores vivos y saturados, pero mezclándolos con algunos tonos pastel para no obtener un ambiente excesivamente chillón. 

La partida se desarrolla en un pueblo en fiestas, por ello, abundan elementos como árboles, casas, farolas... propios de este tipo de lugares, pero además añadimos banderines de colores y una iluminación con tonos cálidos y luminosos para trasmitir un ambiente alegre y festivo. Y para reforzar el estilo sencillo y llamativo utilizamos texturas planas las cuales transmiten colores agradables.  

En cuanto al estilo visual de los personajes es simple y llamativo, con una anatomía estilizada que busca conseguir una apariencia adorable. La paleta de colores hace uso de tonos muy luminosos, vivos y sin mucho detalle. Además, llevarán accesorios con diseños simples pero representativos. 

### **1.9. Alcance del proyecto**

Nuestro objetivo principal es ofrecer una primera versión en la que el jugador pueda modificar los ajustes principales, navegar por las diferentes pantallas planteadas en el diagrama de flujo, acceder a un tutorial y que pueda jugar las partidas que quiera (eligiendo la dificultad), pudiendo ganar o perder y ver los resultados obtenidos. 

A partir de esta base, nos gustaría ampliar el contenido con una versión premium, que incluya un sistema de usuarios con niveles, una tienda con contenido adicional y un modo online cooperativo o competitivo con otro usuario. Al subir de nivel, los usuarios desbloquearán nuevo contenido como escenarios, aspectos para *Marshallow*, iconos de usuario y accesorios para los aldeanos, y en la tienda podrán adquirir nuevos accesorios para los aldeanos e iconos de usuario. Además, durante eventos especiales (Navidad, Verano...) se podrán adquirir unidades limitadas de accesorios especiales. Por otro lado, la versión premium también permitirá modificar ajustes sobre la partida como el número de aldeanos, o seleccionar los accesorios que el usuario quiera que aparezcan durante la partida. 

## **2. Jugabilidad y mecánicas**

En este apartado se encuentra una explicación sobre la jugabilidad, el movimiento, cómo transcurre una partida y los detalles de los personajes. 

### **2.1. Movimiento y físicas**

*Marshallow: Pilferage in YolkTown* cuenta con un sistema de controles sencillo, ya que se debe poder acceder tanto desde un ordenador como desde un dispositivo móvil. Por esto hemos optado por reducir el manejo del personaje de tal forma que el usuario pueda jugar únicamente haciendo clic en el dispositivo de escritorio, o pulsando en la pantalla en un dispositivo móvil. En función de dónde pulse el jugador, el juego reaccionará de una forma u otra, las distintas opciones que encontramos en partida son las siguientes: 

* Pulsar sobre un punto del camino para desplazarte hacia él. 
* Pulsar uno de los dos botones que se encuentran en los laterales izquierdo y derecho de la pantalla para rotar la cámara. 
* Pausar la partida con el botón de la esquina superior izquierda. 
* Seleccionar un aldeano para detenerlo. 
* Detener a un aldeano pulsando el botón que aparece sobre él al seleccionarlo.  

### **2.2. Jugabilidad**

#### **2.2.1. Modos de juego**

Contamos con único modo de juego el cual consiste en lo siguiente: 

El jugador comienza la partida en un pueblo con seis zonas principales por el que pasean y merodean PNJs de aspecto similar, pero con accesorios y cualidades características. Pasado un tiempo uno de los PNJs se dedicará a robar al resto, al ocurrir un robo, aparecerá en pantalla una señal que indica en qué dirección se ha producido el robo. El jugador deberá dirigirse hacia el PNJ que ha sido robado (el cual se encontrará parado en el sitio con una exclamación) y al acercarse a este, le indicará mediante un bocadillo qué características cree que tiene el ladrón. Además, si algún otro PNJ ha sido testigo del robo, estará recorriendo su camino (con una interrogación sobre él) y el jugador también podrá acercarse a ellos para obtener información sobre el ladrón. Una vez el jugador crea que tiene suficientes pistas y encuentre al PNJ que crea que es el ladrón, se podrá acercar a él para detenerle. Si la acusación es correcta, el jugador habrá ganado la partida, si no es correcta perderá una vida y en el caso de que pierda todas las vidas o que el ladrón haya realizado “X” robos (el número máximo depende del nivel de dificultad) perderá la partida. 

**Fig 3**

Aunque este es el único modo de juego, cuenta con los siguientes niveles de dificultad con las siguientes características principales: 

* Fácil: tres vidas, 20 aldeanos y 6 robos máximos. 
* Medio: dos vidas, 30 aldeanos y 5 robos máximos. 
* Difícil: una vida, 50 aldeanos y 4 robos máximos. 

**NB:** PNJ --> Personaje no jugable. 

#### **2.2.2. Niveles**

La partida de *Marshallow: Pilferage in YolkTown* se desarrolla dentro de un único nivel, el pueblo *YolkTown*, el cual se encuentra dividido en 6 lugares de interés principales con diferentes alturas: el ayuntamiento, el mercado, la zona residencial, el parque, la herrería/forja y la plaza principal. Los aldeanos y el personaje principal podrán moverse por cualquiera de las zonas y cualquiera de los pasillos que las unen, pero solo se producirán robos en alguna de estas 6 zonas. 

### **2.3. Flujo de juego**

En este apartado se explicará el transcurso de una partida de *Marshallow: Pilferage in YolkTown*, comentando los pasos a seguir del jugador desde el inicio del juego, hasta la pantalla de fin de partida. 

El jugador iniciará el juego en una pantalla que muestra una animación del logo de *Lightning Opal*, pasando de ahí al menú principal. Desde el menú principal se podrá acceder a los créditos, las opciones del juego, a un tutorial guiado en el que se explica todo lo necesario para poder jugar una partida y a la opción de jugar. Una vez se desee iniciar la partida, el jugador seleccionará la opción de Jugar, lo cual le dirigirá a un menú de selección de dificultad donde podrá seleccionar diferentes niveles de dificultad. 

Como se ha explicado en el apartado anterior, el objetivo del personaje principal será encontrar al ladrón del pueblo con ayuda de la información proporcionada por los aldeanos.  

La partida termina cuando el jugador encuentra al ladrón y le acusa correctamente o cuando se queda sin vidas o el ladrón ha conseguido realizar todos los robos planteados. En el primer caso el jugador habrá ganado la partida y se le indicará la victoria junto con el tiempo tardado en una pantalla de fin de nivel. Si se da alguno de los dos otros casos, en la pantalla de fin de nivel, se le indicará que ha perdido la partida. 

Desde esta pantalla de fin de nivel se podrá volver a jugar, seleccionar otra dificultad o volver al menú principal. 

### **2.4. Personajes y comportamientos**
 
#### **2.4.1. Personaje principal**

El personaje principal del juego es el *marshal* del pueblo *YolkTown* llamado *Marshallow*. Es un personaje jugable que se mueve libremente por el escenario buscando al ladrón con ayuda de la información que le dan las víctimas y los testigos. El control del personaje principal se ha simplificado lo máximo posible, pulsando en la pantalla el jugador decidirá si quiere moverse a un punto concreto, o hasta un aldeano para detenerlo. 

En caso de pulsar en un aldeano, éste queda quieto si no lo estaba ya, y aparece un botón sobre su cabeza para detenerlo. En caso de seleccionar a otro aldeano, o pulsar en un sitio para moverse, el aldeano continúa su camino y el botón desaparece. 

#### **2.4.2. Aldeano**

Son PNJs que transitan por el escenario recorriendo el mapa de un punto de interés a otro, siendo estos las diferentes partes del pueblo, como por ejemplo el parque, el mercado o el ayuntamiento. Por lo tanto, el objetivo de estos agentes será simplemente merodear el pueblo socializando, ya que son las celebraciones de las fiestas. El aldeano elegirá aleatoriamente a qué punto va a moverse una vez lleve 10 segundos en el actual, decidiendo una vez en el nuevo punto si se queda o se mueve a otro porque hay demasiada gente. Esto lo hemos planteado para que no se supere un cupo máximo de posibles aldeanos en un mismo punto, consiguiendo de esta forma un mejor reparto de las zonas. Para moverse entre zonas, el agente utilizará una Malla de Navegación. 

**CARACTERÍSTICAS/ACCESORIOS:**

Los aldeanos se diferencian entre ellos gracias a una serie de características y accesorios: 

  * **Colores:** el color principal de sus cuerpos varía entre verde, azul, morado, rosa y amarillo. 
  * **Cuernos:** podrán contar con 3 tipos de cuernos diferentes (reno, cabra o unicornio). 
  * **Ojos:** contarán con un número de ojos aleatorio entre 1 y 3. 
  * **Gorros:** podrán vestir con 4 tipos de gorros diferentes (gorra con visera, gorro de fiesta, gorro de cazador o gorro de invierno). 
  * **Accesorios de cuello:** aparte de los gorros, podrán llevar 3 tipos de accesorios diferentes en el cuello (pajarita, corbata o collar hawaiano). 
  
**ESTADOS:**

Los aldeanos cuentan con cinco tipos de estados distintos: 

  **1. Viandante:** el aldeano se mueve de un punto clave del poblado (forja, ayuntamiento...) a otro. Al establecer el nuevo punto, determina aleatoriamente si va andando o corriendo. Si la zona que ha elegido ha superado el máximo de aldeanos que puede alojar, elije otra zona y se desplaza hacia ella. No hay problema si durante el trayecto circula por una zona ocupada por el número máximo de aldeanos. 
  **2. Merodear/Ocio:** al llegar a una zona, durante 10 segundos, el aldeano deambula por ella y expresa aleatoriamente su estado de ánimo mediante emoticonos emergentes.
  **3. Víctima "!":** se produce cuando el aldeano se encuentra en una zona y es robado por el ladrón, el aldeano se queda quieto y aparece un icono sobre su cabeza que indica su estado. Cuando el jugador se acerca a la víctima (es decir, se encuentra dentro del radio de interacción del aldeano), esta le proporciona información referente al ladrón mediante un *bark* en forma de bocadillo con dos iconos (pistas) dentro. La víctima proporciona siempre **dos** datos. Llegados a este punto, el agente decide si facilitar un tipo de información u otro: 
  * Un 70%, 30% o 30% (dependiendo del nivel, fácil, medio y difícil respectivamente) de las veces el aldeano recordará sin problemas las dos características del ladrón que proporcionará al jugador, esto se traduce en que la información será fiable y por tanto las dos pistas serán 100% ciertas. 

 * El otro 30%, 70% o 70% (dependiendo del nivel, fácil, medio y difícil respectivamente) de las ocasiones, la víctima no será capaz de recordar nítidamente las características del delincuente. Esto se representa visualmente con un simple icono de interrogación encima del bocadillo informativo. En estos casos también se darán dos pistas, pero no estará asegurado el 100% de la fiabilidad en ambas. Esto quiere decir que una de las pistas será veraz y la otra dudosa, lo cual significa que existirá una probabilidad del 20%, 40% o 65% (dependiendo del nivel, fácil, medio y difícil respectivamente) de que esta última sea falsa (cabe decir que el jugador no tiene forma de saber cuál es la pista dudosa). 
 
Cuando el usuario se aleja del aldeano (sale del radio de interacción) este vuelve a su comportamiento normal de viandante. 

**Fig4**

  **4. Testigo "?":** se produce cuando el aldeano observa cómo sucede un robo (dentro de su área de visión), el aldeano seguirá deambulando por el pueblo, pero indica con un *bark* en forma de interrogación que ha presenciado el hurto. Al acercarse el jugador al testigo (es decir, se encuentra dentro del radio de interacción del aldeano), este se queda quieto y le proporciona información de la misma forma que la víctima, pero con un pequeño cambio. El testigo proporciona únicamente **un** dato y decide si:
  * Facilitar información 100% fiable (un 50%, 35% o 20% (dependiendo del nivel, fácil, medio y difícil respectivamente) de las veces). El testigo habrá visto nítidamente una característica del ladrón. 
  * Proporcionar información dudosa (el otro 50%, 65% o 80% (dependiendo del nivel, fácil, medio y difícil respectivamente) de las ocasiones). Esto se representa visualmente con un simple icono de interrogación encima del bocadillo informativo. Aunque ha presenciado el robo, el testigo no habrá podido discernir bien ninguno de los atributos del ladrón, lo cual quiere decir que la única pista será cierta sólo el 70%, 50% o 30% (dependiendo del nivel, fácil, medio y difícil respectivamente) de las veces. 

Cuando el usuario se aleja del aldeano (sale del radio de interacción), este vuelve a su comportamiento normal de viandante. En caso de que un aldeano en estado de Testigo presencie un nuevo robo, perderá su información del robo anterior. 

**Fig5**

  **5. Arresto:** cuando el jugador selecciona un aldeano cualquiera (hace clic/*tap* sobre él), el aldeano se queda quieto en el sitio y mira a *Marshallow*. Además, aparecerá un botón sobre la cabeza del aldeano que le dará la opción al jugador de arrestarle si este quisiera. Si se selecciona otro aldeano, o pulsa en un sitio para moverse, el aldeano seleccionado en primera instancia continúa su camino y el botón desaparece.
  
#### **2.4.3. Ladrón**

El ladrón es un PNJ que rondará la ciudad con un comportamiento y apariencia similar al de los aldeanos para así no llamar la atención, sin embargo, el objetivo de este agente es robar a los demás PNJs. Para ello el ladrón deberá encontrarse en una zona de interés y además deberá haber alguna víctima a la que robar dentro de su área de visión, todo esto sin dejar de lado que, si el *marshal* se acerca demasiado, el robo se cancela y disimula transitando hacia otra zona. 

**ESTADOS:**
El ladrón cuenta con cuatro tipos de estados distintos:

  **1. Viandante:** realiza el mismo comportamiento de viandante que el resto de los aldeanos. 
  **2. Robar:** si ha pasado cierto tiempo desde el robo anterior (para no llamar la atención) y llega a la zona objetivo, si el jugador no se encuentra cerca y tiene aldeanos en su radio de interacción, escoge de entre todos ellos a uno aleatorio y se dirige rápidamente a robarle. Cuando ha realizado el robo (cancela la acción si el jugador entra en el rango) volverá a al estado deambular andando rápido durante un periodo de tiempo o fingirá ser un testigo. 
  **3. Fingir ser textigo:** Una vez ha llevado a cabo un robo, decidirá si pasará a actuar como un **testigo** mientras deambula de una zona a otra. En caso de que actúe como testigo, un *bark* en forma de interrogación aparecerá sobre su cabeza dando a entender al *marshal* que ha presenciado un robo (tal y como lo haría el aldeano en estado de testigo). Si el *marshal* se acerca a él, sucederá lo mismo que sucede cuando el marshal se acerca a un aldeano testigo y este le muestra un *bark* de información dudosa (es decir, el que tiene un interrogante en el bocadillo), la diferencia es que, en el caso del ladrón, esta información tendrá un 100% de probabilidad de ser falsa. La gracia reside en que el jugador no tiene forma de saber esto y simplemente lo confundirá con un testigo normal y corriente. 
  **4. Arresto:** realiza el mismo comportamiento de arresto que el resto de los aldeanos. 

**NB:** *Bark* --> Cuando un PNJ anuncia sus intenciones/estado al jugador (ya sea mediante audio o mediante una imagen). 

## **3. Interfaz**

A continuación, mostramos las especificaciones sobre cómo se organizarán los menús y la interfaz de las diferentes pantallas de juego que conforman *Marshallow: Pilferage in Yolktown*. En cada apartado aparecerán dos versiones de la pantalla en cuestión (tanto en el caso de los *concepts* como en el de las versiones finales), siendo la segunda la versión final de la primera, pero mostrando cómo serían las interacciones y/o con especificaciones de las animaciones de la IU (esto último solo en los *concepts*). Para entender estas animaciones contamos con una leyenda (aunque se indique implícitamente en dicha leyenda, cabe decir que los elementos rojos indican movimientos de la interfaz y los azules del fondo): 

**Fig6**

Finalmente, comentar que algunas de las propuestas que aparecen en los *concepts* se acabaron descartando en la versión final como se puede comprobar. 

### **3.1. Diagrama de flujo**

**Fig7**

### **3.2. Introducción logo**

**Fig8**

### **3.3. Título**

**Fig9**
**Fig10**

### **3.4. Menú principal**

**Fig11**
**Fig12**
**Fig13**
**Fig14**

### **3.5. Opciones**

**Fig15**
**Fig16**

### **3.6. Créditos**

**Fig17**
**Fig18**

### **3.7. Elección de dificultad**

**Fig19**
**Fig20**
**Fig21**

### **3.8. Durante la partida**

**Fig22**
**Fig23**

### **3.9. Interacciones durante la partida**

**Fig24**
**Fig25**

### **3.10. Pantalla de final de partida**

**Fig26**
**Fig27**
**Fig28**
**Fig29**

### **3.11. Pausa**

**Fig30**
**Fig31**

### **3.12. Tutorial**

**Fig32**
**Fig33**

### **3.13. Contextualización**

**Fig34**
**Fig35**

### **3.14. Interacción aldeanos**

Como apartado extra de la interfaz, cabe nombrar los emoticonos que aparecen encima de los aldeanos cuando estos merodean por las zonas. Esto se añadió para darle más credibilidad a la inteligencia artificial, ya que su función es indicar el "estado de ánimo" de los aldeanos. A continuación, se muestran los iconos que aparecen: 

**Fig36**

## **4. Arte**

### **4.1. Personajes**

Nuestro juego cuenta con dos personajes distintos: *Marshallow* (el personaje principal) y los aldeanos del pueblo de *YolkTown*. Pero ambos comparten varias características en común, como los colores pastel, los ojos saltones o la apariencia adorable (extremidades cortas, sin torso...). A continuación, se mostrará todo el proceso de desarrollo de dichos personajes, desde las referencias que tomamos hasta el resultado final. 

#### **4.1.1. Referencias/inspiración**

**Fig37**
**Fig38**
**Fig39**

Como se puede observar todos los personajes que tomamos de referencia cuentan con las características ya comentadas y establecen una idea general que transmite una apariencia adorable. Esta idea es la que queríamos conseguir con nuestros propios personajes. 

#### **4.1.2. Conceptos/colores**

**ALDEANOS:**

Tomando las referencias vistas anteriormente, se ha querido crear un diseño para los aldeanos muy simplificado y con pocos detalles pero que a la vez transmitan personalidad propia, de tal forma que cuando el usuario juegue recuerde a los personajes como un aspecto importante del videojuego. 

En primer lugar, se trabajaron las formas, la complexión del cuerpo y los detalles de la piel: 

**Fig40**
**Fig41**

Tras escoger la forma de ovoide, se decidió la cara que tendrían los aldeanos:

**Fig42**

Más tarde, se sometieron a votación tres propuestas finales tanto de la cara (figura 43) como del cuerpo (figura 44) del aldeano: 

**Fig43**
**Fig44**

Finalmente, se decidieron cinco colores para los habitantes de *Yolktown*, que además de contar con una importante carga artística, también están directamente relacionados con la jugabilidad, ya que recordemos que una de las características que se tendrán en cuenta para identificar al ladrón es el color de su cuerpo: 

**Fig45**

Dos ilustraciones del resultado final del desarrollo del personaje:

**Fig46**

**PERSONAJE PRINCIPAL, MARSHALLOW:**

En este caso, tal y como su nombre indica, no existían tantas dudas a la hora de crear una forma para el cuerpo, ya que todo giraría entorno a las nubes o malvaviscos: 

**Fig47**

Seguidamente, se trabajaron las posibles caras de nuestro *marshal*: 

**Fig48**

Tras todo el proceso de elección se realizó una votación final para las formas (figura 49) y las caras (figura 50): 

**Fig49**
**Fig50**

Finalmente, tras varias pruebas entre las cuales se encuentran las de la figura 51, se decidió que el color de Marshallow sería el de la figura 52: 

**Fig51**
**Fig52**

Ilustración del resultado final del desarrollo del protagonista: 

**Fig53**

**PERSONAJE EXTRA:**

A mitad del desarrollo del juego se decidió introducir un personaje encargado de dirigir al usuario en el tutorial. Este sería el abuelo del protagonista y a su vez el antiguo *marshal*, *Marshugus*, y su papel dentro del juego consistiría en enseñar a su nieto a cómo ser un marshal. La siguiente figura muestra una ilustración de dicho personaje: 

**Fig54**

**ACCESORIOS:**

Finalmente se crearon un total de 10 accesorios para los aldeanos, sin contar el número de ojos y colores posibles. A continuación, se muestran artes conceptuales dichos accesorios y características: 

**Fig55**

Seguidamente se muestran ilustraciones de todos los accesorios y atributos, las cuales se añadieron al juego como *assets* para cuando el aldeano dé las pistas sobre el ladrón: 

**Fig56**

Estas tres últimas ilustraciones se utilizan para indicar (de izquierda a derecha) que el ladrón no porta accesorio de cuello, cuernos o accesorio de cabeza: 

**Fig57**

#### **4.1.3. Turnarounds**

En este apartado se muestran los dos *turnarounds* que se realizaron para los aldeanos y *Marshallow* para su posterior modelado: 

**Fig58**
**Fig59**

#### **4.1.4. Modelos**


**PERSONAJES:**

A continuación, se muestran los modelos usados en el juego. Entre ellos encontramos al personaje jugable, *Marshallow*, y a uno de los aldeanos. 

**Fig60**

Podemos observar también la malla usada, siendo esta *low poly* para no usar demasiada memoria ya que se trata de un juego para web y dispositivos móviles.

**Fig61**

**OBJETOS:**

**Fig62**
**Fig63**
**Fig64**

### **4.2. Escenarios**

El escenario principal en el que se desarrolla la partida consiste en un pueblo con sus respectivas casas, calles, árboles y zonas comunes como por ejemplo un parque o la plaza principal. Además, la acción coincide con la celebración de unas fiestas, por ello queremos transmitir un ambiente festivo y agradable mediante el uso de banderines de colores, texturas planas de tonos suaves pero coloridos y una iluminación con tonos cálidos y luminosos. 

Para mantener el estilo general del juego, sencillo pero llamativo, queremos que el entorno tenga un estilo de modelado *low poly* suave, es decir, haciendo uso de formas geométricas con pocos polígonos, pero redondeadas y con bordes suaves de los objetos con esquinas muy marcadas. 

#### **4.2.1. Referencias/inspiración**


**MODELADO:**

Como se ha comentado anteriormente el estilo del modelado será suave y sencillo. Nos ha sido de gran inspiración el trabajo del artista *Gustavo Henrique*:

**Fig65**

Además, comentar que también nos ha servido de inspiración para definir la estructura del pueblo de forma escalonada, destacando diferentes alturas. 

**AMBIENTACIÓN:**

Para transmitir un ambiente agradable hemos elegido los tonos cálidos y luminosos. Para obtenerlos hemos situado el pueblo sobre la estación de otoño (árboles rojos, naranjas...) y sobre la hora que el sol comienza a ponerse, proyectando largas sombras, pero una luz de ambiente anaranjada. En general abundan los tonos naranjas, rojos, rosas y verdes amarillentos. Estas son algunas de nuestras principales referencias de este entorno: 

**Fig66**
**Fig67**

#### **4.2.2. Concepts**

Primero hemos realizado un esquema general de las distintas zonas que conforman el pueblo con sus respectivas alturas. A continuación, hicimos un plano un poco más realista con respecto al tamaño y la situación de las zonas y pasillos: 

**Fig68**

Antes de pasar a realizar el modelado en detalle del escenario, hemos realizado un paso previo de *blocking* en *Unity* para tener situados los elementos principales y fijadas aproximadamente las alturas de las zonas y los edificios. A continuación, se muestran distintas vistas de este proceso: 

**Fig69**

#### **4.2.3. Modelos**

En este apartado mostramos el resultado final del escenario en *Blender*:

**Fig70**
**Fig71**
**Fig72**

**ZONAS:**

**Fig73**
**Fig74**
**Fig75**

#### **4.2.4. Iluminación**

A parte de lo comentado en el apartado de referencias sobre la iluminación de atardecer con tonos cálidos y sombras alargadas, nos gustaría destacar que para reducir el coste computacional hemos generado "mapas de luz" (*lightmap*) de tal forma que almacena los brillos y las sombras del escenario en una textura previamente calculada en vez de generar la iluminación en tiempo real durante la partida.  

### **4.3. Efectos especiales**

Para el desarrollo de los efectos especiales que aparecen durante el juego, hemos querido mantener la estética simple que tiene todo el juego. Se han realizado efectos con formas sencillas y representativas, para que el jugador entienda rápidamente la información que se quiere transmitir en cada momento. Las texturas se han realizado con un color simple, añadiéndole un sutil degradado, y un borde blanco para destacarlos del fondo. 

#### **4.3.1. Referencias/inspiración**

Nuestra principal fuente de inspiración para llevar a cabo el diseño de los efectos ha sido el juego *Animal Crossing* debido a su estilo sencillo y agradable. 

**Fig76**

#### **4.3.2. Concepts**

A continuación, se muestran los *concepts* realizados para los diferentes efectos que aparecen en el juego. 

**Fig77**
**Fig78**

#### **4.3.3. Texturas**

Para la implementación de los efectos, se ha usado una única textura, de esta forma se ahorra en el uso de materiales dentro del juego y se mejora el rendimiento ligeramente. Este sería el resultado final de la textura generada 

**Fig79**

#### **4.3.4. Resultado final**

Por último, se muestran unas imágenes con el resultado final de los efectos especiales. 

**Fig80**
**Fig81**
**Fig82**
**Fig83**

### **4.4. Interfaz**

Para llevar a cabo el diseño de la interfaz hemos querido obtener un resultado divertido con un toque infantil, acorde con el estilo del juego. Para ello hemos usado colores claros y coloridos además de formas redondeadas y con un tamaño relativamente grande para que se vean muy bien. También hemos elegido un par de fuentes (gratuitas) con grosor y bien definidas para que se lean perfectamente. Comentar que hemos intentado usar la menor cantidad de texto posible para que todo sea lo más visual posible. Por último, decir que, nuestra mayor inspiración ha sido el videojuego *Mario 3D World*: 

**Fig84**

**El resultado final de los elementos de la interfaz se encuentra recopilado en el apartado 3 de este documento.**

### **4.5. Animaciones**

Para las animaciones, se han diseñado 3 para los aldeanos y 2 para el *marshal*. Los aldeanos cuentan con una animación de *idle*, una de andar y una de correr. El marshal solo cuenta con dos de ellas, una de *idle* y otra de correr. 

### **4.6. Audio**

Se ha intentado que tanto la música como los efectos de sonido inspiraran lo mismo que el apartado visual del proyecto. Respecto a los efectos, tienen un tono muy agradable, juguetón e infantil, conceptos que casan con las ideas generales del juego. Y en cuanto al apartado musical, destacar que se ha decidido apostar por una idea que recuerda más al country (relacionado con el personaje principal), utilizando instrumentos como el banjo. Es en los créditos donde sí se nota un tono más tranquilo y agradable, separándose del estilo country del resto de la banda sonora. 

Los referentes por excelencia han sido sobre todo juegos de la compañía Nintendo, tales como Super *Mario Bros Wii* o *Animal Crossing*, pero también otros títulos como *Spyro the dragon*. 

#### **4.6.1. Música**

Se ha compuesto una BSO (Banda Sonora Original) conformada de canciones para: 

* Menús (menú del título, menú principal, menú de dificultades y menú de opciones). 
* *In game*. 
* Créditos. 
* Ganar (justo cuando aparece la pantalla de "¡Has ganado!"). 
* Perder (justo cuando aparece la pantalla de "¡Has perdido!"). 
* Haber ganado (tras sonar la pequeña melodía de "Ganar"). 
* Haber perdido (tras sonar la pequeña melodía de "Perder"). 

Toda la música es original y se ha creado mediante el software online *Bandlab*. Ciertos arreglos se han realizado utilizando *Audacity*. 

#### **4.6.1. Efectos**

Los efectos de sonido que se han implementado son los siguientes: 

* Transición circular parte 1. 
* Transición circular parte 2. 
* Dar información (Cuando aparece un bocadillo informativo). 
* Ladrón nervioso. 
* Pasar con el ratón por encima de un botón. 
* Pulsar botón. 
* Ladrón encontrado cerca. 
* Sonidos de aldeanos x5. 
* Te has equivocado, yo no soy el ladrón. 

Alguno de los sonidos ha sido descargado de la página *Freesound*, de tipo *Creative Commons 0*, y posteriormente modificado en *Audacity* para su integración en el juego. Sin embargo, los sonidos que no han sido descargados de *Freesound* han sido efectos propios y posteriormente editados también en *Audacity*. 

## **5. Monetización y modelo de negocio**

### **5.1. Modelos de negocio**

**Freemium**: Puedes acceder al juego base (un mapa con tres niveles de dificultad preestablecidos y un tutorial), pero si pagas tienes una versión premium del mismo juego con más contenido. Esta versión *premium* añade al juego base los siguientes puntos: 

* Se desbloquea un perfil de usuario con un nivel y un icono. 
* Desaparece "*free*" en el título una vez el usuario ha iniciado sesión. 
* Se añaden modificadores para las partidas (número de ladrones, número de aldeanos, filtros visuales, mapa, accesorios personalizados...). 
* Se desbloquea un sistema de recompensas por nivel donde según se vaya avanzando se van obteniendo accesorios, iconos de usuario, mapas, y aspectos para el personaje principal. Según se sube de nivel las recompensas son mejores, pero es más complicado llegar al siguiente nivel. El máximo nivel que se puede alcanzar será el nivel 90, pudiéndose ampliar en futuras actualizaciones del juego. 
* Se da acceso al modo *online* en el que puedes jugar con otro usuario visitando su pueblo, de tal forma que la partida se jugará de forma cooperativa (los dos jugadores son el *marshal* del pueblo y tienen que encontrar al ladrón o ladrones del pueblo) o competitiva (los dos jugadores son el marshal del pueblo y gana quien encuentre antes al ladrón) con los ajustes y elementos elegidos por el anfitrión.  

**Cebo y anzuelo:** tras desbloquear la versión *premium*, el usuario puede acceder a una tienda donde aparecen artículos (accesorios para los aldeanos e iconos de usuario) que se pueden comprar con dinero real directamente y que varían cada 24 horas (aunque desaparezcan, podrán volver a aparecer al cabo de un tiempo aleatorio). Estos artículos son únicos de la tienda y no se desbloquean al subir de nivel. Además, durante eventos (Navidad, Verano...) se ofrecerán artículos especiales relacionados con los eventos. Cada artículo solo se podrá comprar durante tres días, una vez acabe ese periodo de tiempo no podrá comprarse nunca más. De esta forma, se incentiva la compra de estos ya que solo algunos usuarios podrán obtener dichos artículos.  

A continuación, adjuntamos unas imágenes que **NO** se incluyen en esta versión del juego pero que se implementarán en futuras actualizaciones y que ilustran cómo se vería lo explicado anteriormente: 

Al iniciar el juego el jugador puede entrar con su cuenta premium o jugar como invitado (versión gratuita). Si elige jugar como invitado le aparecerá la imagen de la figura 86, y si no quiere obtener la versión premium simplemente podrá minimizar la imagen con el botón "*NAH... MAYBE LATER*". 

**Fig85**
**Fig86**
**Fig87**

Las siguientes capturas muestran cómo sería el juego si se tuviese la versión *premium*. Las nuevas opciones con las que contamos son: en la esquina inferior izquierda el usuario con su icono, nombre y nivel, y el modo *online*. En la esquina superior derecha el "armario" donde se puede cambiar el aspecto del personaje principal y a su derecha la tienda donde comprar los accesorios y los iconos. 

**Fig88**

Al seleccionar el nombre de usuario de la esquina inferior izquierda aparece una pantalla con el perfil del usuario, en ella se podrá cambiar el nombre, ver las estadísticas del jugador y cambiar el icono de jugador. En el caso de seleccionar la opción de cambiar icono, hay una galería con los iconos que el jugador tiene desbloqueados para que pueda cambiarlo (figura 90). 

**Fig89**
**Fig90**

Como se ha podido observar anteriormente el jugador cuenta con un nivel, este nivel aumenta según se juegan partidas, y cuando llega a "X" nivel clave se ofrece una recompensa (accesorios, iconos, aspectos y mapas). Las siguientes imágenes son las que aparecen cuando vuelves al menú principal tras haber alcanzado un nivel clave: 

**Fig91**

Los aspectos conseguidos para *Marshallow* se guardan y se pueden equipar en la pantalla del “armario” cuyo icono (una camiseta) aparecía en la esquina superior derecha de la figura 88.  

**Fig92**

Los mapas y los accesorios obtenidos se podrán usar en el modo personalizado. Antes de empezar la partida podremos elegir si jugar una partida normal (como la de la versión gratuita) o jugar una partida personalizada donde podremos modificar las opciones mostradas en la figura 94. Aparte, también se podrán elegir los accesorios que llevarán los aldeanos durante la partida (figura 95).

**Fig93**
**Fig94**
**Fig95**

En el menú principal también tenemos la opción de jugar *online* cooperativamente con otro jugador. Al pulsar el icono de la bola del mundo nos aparece este mensaje emergente que nos da la opción de "invitar a otro jugador a tú pueblo" (*host*) o de "jugar en el pueblo de otro jugador" (*guest*). 

**Fig96**
**Fig97**

Finalmente tenemos la tienda. Cómo se puede ver en la siguiente imagen, aparecen artículos cómo accesorios o iconos, los cuales varían cada 24 horas. Encima de cada artículo hemos indicado el tiempo que les queda en tienda para que sean sustituidos por otros. Además, también se puede ver un artículo especial del evento de Halloween que indica que le quedan 3 días en la tienda. Como hemos comentado anteriormente, este artículo es limitado, cuando acaben esos tres días ya no se podrá obtener.  

**Fig98**

### **5.2. Tablas de productos y precios**

| **Producto** | **Descripción** | **Precio** |
| :---------: | :---------: | :---------: |
| Pase *Premium* | Desbloquea los contenidos *premium* explicados en el apartado 5.1. | 3€ |
| Iconos de usuario | Imagen que representa al usuario en su cuenta *premium* | 2€ |
| Accesorios aldeanos | Objetos que usarán los aldeanos durante las partidas (gorros, colgantes...) | 4€ |
| Accesorios aldeanos especiales | Objetos limitados relacionados con eventos que usarán los aldeanos durante las partidas (gorros, colgantes...) | 6€ |


| **Desbloqueable (por nivel)** | **Descripción** | **Nivel (hasta nivel 90)** |
| :---------: | :---------: | :---------: |
| Mapas | Nuevos pueblos con nuevas zonas y características tematizadas | Cada 30 niveles |
| Iconos de usuario | Imagen que representa al usuario en su cuenta *premium* | Cada 5 niveles |
| Aspectos de personaje | Aspectos que cambian el diseño de personaje (color, tamaño, forma y/o accesorios) | Cada 15 niveles |
| Accesorios aldeanos | Objetos que se sarán los aldeanos durante las partidas (gorros, colgantes...) | Cada 10 niveles |

### **5.3. Modelo de lienzo o canvas**

**Fig99**

## **6. Planificación y costes**

### **6.1. El equipo humano**

El equipo de desarrollo está formado por 5 personas:    

* Programador de mecánicas y programador de IA – Samuel Ríos Carlos.  
* Programador de mecánicas y programador de IA, artista técnico y artista de VFX - Mario Belén Rivera.
* Artista 2D de personajes, artista UI/UX, diseñador de IA y compositor de banda sonora – Enrique Sánchez de Francisco.  
* Artista 3D de personajes y accesorios, animador, diseñador de IA y diseñador de SFX – Sergio Cruz Serrano.  
* Artista de entornos, diseñadora de niveles y programadora de IA – Mireya Funke Prieto.

### **6.2. Estimación temporal del desarollo**

**Fig100**

| | **Porcentaje sobre el total de tiempo desarrollado** |
| :---------: | :---------: | 
| **Diseño de juego y documento GDD** | 10%  |
| **Diseño y creación de artes** | 40% |
| **Diseño de sonido** | 10% |
| **Implementación mecánicas y jugabilidad** | 10%  |
| **Diseño e implementación IA** | 25%  |
| **Publicación y marketing**  | 5% |

### **6.3. Costes asociados**

#### **6.3.1. Material y software**

A continuación, enumeramos los distintos softwares que vamos a utilizar y si nos hace falta invertir recursos en obtenerlos: 

* Clip Studio Paint (1 licencia, propietarios)  
* BandLab (software gratuito)  
* AfterEffects (1 licencia (24’19€/mes), proporcionado por la URJC durante el primer año de desarrollo)  
* Audacity (software gratuito)  
* Photoshop (3 licencias (24’19€/mes), proporcionado por la URJC durante el primer año de desarrollo)  
* Github (licencia gratuita)  
* 3DsMax (1 licencia (2.136€/año), proporcionado por la URJC durante el primer año de desarrollo) 
* Blender (software gratuito) 
* Trello (software gratuito)  
* Teams (proporcionado por la URJC durante el primer año de desarrollo)  
* Discord (software gratuito) 
* Unity (licencia gratuita) 
* Visual Studio (licencia gratuita) 

#### **6.3.2. Financiación**

A parte de los beneficios obtenidos a través del juego, para financiar el proyecto nos gustaría mostrar nuestro juego en páginas de *'Crowdfunding'*. Además, contaremos con un apartado en nuestra página web dónde se podrán comprar artículos de *merchandising* sobre nuestros proyectos. 

**KICKSTARTER**

Para la página de *'Crowdfunding'* hemos elegido *Kickstarter*, esto se debe a la visibilidad a nivel global que tiene esta página. También la hemos elegido porque consideramos que el contenido de nuestro juego es ideal para el sistema de recompensas que ofrece la página, ya que contamos con un contenido visual atractivo del que sacar provecho mediante dichas recompensas. A pesar de que es una opción arriesgada, consideramos que nos puede aportar mucho beneficio ya que nuestro juego resulta muy llamativo. 

A continuación, explicamos las características de nuestro programa de financiación: 

Precio base: 45.000€ 

Tiempo para financiar: 30 días 

Recompensas:

* 5€ o más: nombre en los créditos y acceso a la versión premium desde el día 1. 
* 20€ o más: aspecto de *Marshallow* y accesorio para los aldeanos exclusivos (disponible únicamente en esta recompensa). 
* 50€ o más: una camiseta con un dibujo de los personajes. 
* 100€ o más: una imagen en formato digital para fondo de pantalla (con resolución 4K) y en formato póster a elegir entre tres opciones. 
* 250€ o más: dos peluches, uno de *Marshallow* y otro de un aldeano a elegir entre tres opciones, además de una figura a escala 20 cm de *Marshallow*. 
* 500€ o más: un juego de tablero inspirado en el videojuego (cuenta con cartas, figuras y mapa entre otros elementos). 
* 1.000€ o más: traje de aire completo de Marshallow y de un aldeano para hacer cosplay. 
* 3.000€ o más: acceso gratis a todos los objetos de la tienda durante un año desde la salida del videojuego. 

**Fig101**

**TIENDA DE LA PÁGINA WEB**

Estos son alguno de los productos relacionados con nuestros proyectos que estarían disponibles en un futuro en nuestra página web: 

* Tazas --> 12€. 
* Alfombrillas de ratón --> 7€. 
* Figuritas de los aldeanos con accesorios de 4 cm --> 8€. 
* Peluches (distintos a los de *Kickstarter*, así se le da exclusividad a los mecenas y además se ofrece un producto diferente para los amantes del juego y coleccionistas) --> entre 20-25€. 
* Camisetas (de nuevo distintas a las de *Kickstarter* por las mismas razones) --> 15€. 
* Sudaderas --> 30€. 
* Huevo antiestrés --> 4€. 
* Pegatinas (pack de varias) --> 2€. 
* Fundas para móvil --> 10€. 
* Bolsos de tela --> 20€. 
* Posters (de nuevo distintos a los de *Kickstarter* por las mismas razones) --> 7€. 

A continuación, nos gustaría aportar un adelanto de cómo se vería implementado esta tienda en nuestra página web: 

**Fig102**

#### **6.3.3. Fuerza de trabajo**

**Equipo de desarrollo:** 5 personas. 
**Duración:** 2 años. 

Coste bruto para la empresa por empleado al mes: 1.500€ 
Coste bruto total: 180.000€ 

**Total de sueldo:**  180.000€ + 25% de beneficio industrial 45.000€ = 225.000€ 

Si sumamos el sueldo y las licencias (225.000€ + 3.054'14€) obtenemos = 228.054'14€ de coste total del proyecto.


## **7. Contenidos pospuestos a futuras actualizaciones**

Nos gustaría comentar ciertos aspectos del juego que no hemos podido añadir para esta primera versión.

**ANIMACIONES DE LA INTERFAZ**

Nos gustaría hacer la interfaz un poco más animada, de manera que algunos elementos tengan movimiento para hacerla más dinámica y divertida. Un ejemplo de esto es en cuanto a los botones del menú principal (jugar y tutorial), que tendrían un pequeño giro bidireccional constante. 

En el apartado "3. Interfaz", se puede encontrar arte conceptual que ilustra varias animaciones planteadas para la interfaz. 

**EFECTOS VISUALES**

A futuro, nos gustaría implementar efectos visuales para terminar de pulir nuestro juego en ese aspecto. En un primer lugar, y como ejemplo de VFX, teníamos pensado introducir un efecto que se visualice cuando el jugador se desplace por el mapa, es decir, un rastro de polvo que el jugador va dejando en el camino. Otro ejemplo de efecto a añadir sería un indicador visual en pantalla que avise de que solo falta un robo para acabar la partida. 

**ANIMACIONES FACIALES**

Para mejorar visualmente nuestro juego queremos dar más personalidad a los aldeanos y se ha pensado en introducir animaciones faciales. Hay ciertos momentos en la partida en los cuales los aldeanos expresan su estado de ánimo mediante un emoticono, sin embargo, al no tener animaciones faciales actualmente, se hace raro para el usuario, que el aldeano diga que se encuentra cansado mientras tiene la expresión de felicidad en la cara. Por esto queríamos desarrollar las animaciones de alegría, tristeza, cansancio y enfado.  

**ANIMACIONES EXTRA**

En cuanto a las animaciones de nuestro juego, nos gustaría implementar animaciones adicionales tales como la animación de un aldeano sorprendido para cuando el *marshal* intenta detenerle, una animación del *marshal* apartando gente para cuando se formen aglomeraciones que corten el camino o una animación de arresto para cuando logras atrapar al ladrón. 

**MODO MULTIJUGADOR (*ONLINE*)**

Se ha planteado la posibilidad de incorporar un modo multijugador en línea, de manera que puedes jugar en las aldeas de tus amigos y que los aldeanos lleven los objetos que ellos tengan desbloqueados. 

El multijugador tendría dos modos de juego: cooperativo, donde ambos jugadores deben colaborar para encontrar al ladrón, y competitivo, donde gana el primer jugador que lo encuentre (donde pueden perder ambos jugadores si no lo encuentran o si se quedan sin intentos). 

Esto implicaría la creación de cuentas de juego, las cuales podrán subir de nivel para desbloquear nuevos objetos, aspectos y escenarios. 

En el apartado "5.1. Modelo de Negocio", se pueden encontrar imágenes que ilustran cómo funcionaría el modo *online*. 

**OBJETOS PERSONALIZADOS**

El usuario puede coleccionar objetos para los aldeanos de manera que antes de comenzar la partida el jugador puede elegir cuáles quiere que aparezcan en su partida. 

Por ejemplo: un jugador puede elegir la gorra de niño, el gorro de fiesta, el sombrero de cazador, los cuernos de alce, la pajarita, la corbata y el collar de flores. De esta manera, la partida se aleatorizaría para que solo salgan esos objetos elegidos.  

En el apartado "5.1. Modelo de Negocio", se pueden encontrar imágenes que ilustran cómo se desbloquean y almacenan estos objetos. 

**PARTIDAS PERSONALIZADAS**

Un nuevo modo de dificultad totalmente personalizado, donde los jugadores puedan modificar parámetros de la partida, como el número de aldeanos, ladrones, robos e intentos, la velocidad del personaje del jugador o las probabilidades de las pistas. 

En el apartado "5.1. Modelo de Negocio", se pueden encontrar imágenes que ilustran las pantallas de personalización de partida. 

**TIENDA**

Los jugadores podrán comprar objetos nuevos para su personaje principal y sus aldeanos. La tienda cambiará sus objetos cada cierto tiempo, de forma rotativa (es decir, un objeto puede salir un día y 3 semanas después de nuevo). 

Además, se harán objetos exclusivos que solamente puedan ser obtenidos durante un período de tiempo, como por ejemplo un sombrero de bruja para la festividad de Halloween o un gorro de Santa Claus en navidades. 

En el apartado "5.1. Modelo de Negocio", se pueden encontrar imágenes que ilustran el aspecto de la tienda. 

**ESTADÍSTICAS**

Una nueva pantalla que añade información sobre las partidas del jugador, que incluiría lo siguiente: 

* Partidas jugadas. 
* Victorias (con porcentaje respecto a las partidas jugadas). 
* Ladrones atrapados (no tiene por qué coincidir con las victorias, ya que podría haber más de un ladrón por partida). 
* Veces que se ha equivocado de aldeano. 
* Mapa favorito (el más jugado). 
* Objeto favorito (el más utilizado en sus partidas). 

## **8. Post-Mortem**
### **8.1. ¿Qué ha ido bien en el equipo?**

Esta vez hemos conseguido trabajar de una forma casi óptima haciendo uso de una mejor organización y planificación, sobre todo gracias a haber hecho un mejor uso de las herramientas de *GitHub* y *Trello*. Se nota una gran mejoría respecto al anterior proyecto sin lugar a dudas, y se ha hecho un uso bastante práctico de las reuniones semanales (más largas y utilizadas para organizar y planear) y las *Daily Standups*. Hemos hecho uso de los grupos de trabajo y no nos hemos molestado ni para hablar ni para trabajar, hemos cumplido la mayoría de los plazos propuestos al principio del proyecto o al menos hemos compensado algunas tareas retrasadas adelantando otras, esta vez hemos podido descansar más y no tener sesiones de trabajo tan largas (aunque algunos días no se ha podido evitar) y hemos rendido mucho y cumplido cada uno con su parte del trabajo de la mejor forma posible (aumentando la confianza entre los miembros del equipo respecto al trabajo realizado por cada uno). Esta vez, ha sido más importante que nunca el hecho de habernos marcado límites respecto a la magnitud del proyecto, generándonos así unos objetivos asequibles y obteniendo como resultado un muy buen trabajo reflejado en un juego bien cerrado. 

### **8.2. ¿Qué ha ido bien individualmente?**

Mario: 

* Solución de problemas en poco tiempo pudiendo así avanzar en el proyecto. 
* Rapidez implementando VFX, animaciones... 
* Trabajo óptimo, teniendo en cuenta todo lo planeado. 
* Apoyo y ayuda constante a todo el equipo en general. 

Sergio: 

* Primera vez enfrentándose a un proyecto 3D con muy buenos resultados en el modelado. 
* Abierto a propuestas y mejoras. 
* Rápido con los modelados y los SFX. 
* No hablar sobre otros temas durante el trabajo. 
* Mayor interés y participación. 

Mireya: 

* Mejor el tema del descanso. 
* Facilitar a los programadores al realizar un *blocking* del escenario. 
* Mejor comunicación. 
* Cubriendo tareas que quedaban sueltas. 
* Gran escenario en tiempo reducido. 
* Buen trabajo con la iluminación, haciendo uso de las técnicas necesarias. 

Samuel: 

* Solución de problemas en poco tiempo pudiendo así avanzar en el proyecto. 
* Ir sentando las bases de programación para que sea más fácil de implementar a futuro. 
* Buena organización. 
* Estar pendiente a la situación del equipo y organizar reuniones de planificación y organización para evitar diversas opiniones respecto al trabajo. 
* Rápida y completa implementación de la inteligencia artificial de los personajes. 

Enrique: 

* Aportación creativa en sus ámbitos. 
* Mejor comunicación. 
* Muy buen trabajo en cuanto a la banda sonora del juego. 
* Las ilustraciones del juego y las del modelo de negocio son perfectas para el proyecto. 
* Buena dinámica de trabajo cumpliendo los plazos. 
* Descubrir nuevas formas de trabajar. 

### **8.3. ¿Qué podríamos mejorar del equipo?**

El mayor problema que hemos cometido ha sido empezar el proyecto más tarde de lo debido, pero por suerte esto no ha afectado gravemente ni al desarrollo ni al resultado. Por otro lado, deberíamos cuidar tanto la constancia como la duración de las *Daily Standups*, ya que no se han realizado cotidianamente y a veces se trataban temas que alargaban estas más de lo debido. En rasgos generales se ha notado una falta de comunicación en ciertos momentos del desarrollo que, gracias a la metodología de trabajo planteada, se ha podido compensar mediante las reuniones de equipo. También hemos percibido un reparto de trabajo un tanto desigual (sobre todo hacia el final del desarrollo) que se podría haber evitado mediante una mejor planificación inicial, pero se ha sobrellevado gracias a que algunos miembros han ayudado a quien/es más lo necesitaban. Finalmente, cabe decir que la actividad en redes sociales es muy mejorable, a mitad del proyecto hemos empezado a mostrar contenido, pero deberíamos de haber empezado antes, manteniendo constancia y creando comunidad entorno al proyecto. 

### **8.4. ¿Qué podríamos mejorar a nivel individual?**

Mario:  

* Quedarse demasiado tiempo trabajando en el proyecto. 
* Desconectar más del proyecto. 

Sergio:  

* Investigación, buscar información de cara a trabajar más eficientemente y elaborar un mejor resultado, como por ejemplo en las animaciones. 
* Poca iniciativa. 
* Independencia del resto del equipo para realizar las tareas que le corresponden. 

Mireya: 

* Negatividad puntual de cara al proyecto que puede afectar a otros miembros del equipo. 
* Mejorar los descansos, estableciendo un horario más constante. 
* Pedir ayuda más a menudo para evitar quemarse con lo que hace. 

Samuel:  

* Quedarse demasiado tiempo trabajando en el proyecto. 
* No estar atento a cosas que se dicen o interrumpir sin querer por este motivo. 
* Poco participativo en cuanto a documentos. 

Enrique:  

* Evitar silenciar el micrófono sin avisar ya que causa problemas de comunicación. 
* Debería pedir ayuda para no saturarse con el trabajo. 
* Mejorar los descansos, estableciendo un horario más constante. 

### **8.5. Flujo de correcciones**

Para monitorizar mejor la duración de las *Daily Standups* en siguientes proyectos, hemos decidido usar temporizadores para así limitar el tiempo que habla cada participante y a su vez se va a establecer qué días y en qué horario se van a hacer dichas reuniones. A su vez, para mejorar la organización, se va a optimizar el uso de *Trello* (devolviendo tareas al *Sprint Backlog* si estás se ven modificadas y dejan de estar completas). Además, hemos decidido crear una lista en dicha aplicación con información y posts relevantes para el proyecto para mejorar la calidad del proyecto. 

Por otro lado, para mejorar el uso de las redes sociales, trataremos de interactuar más con la comunidad desde un primer momento, en lugar de esperar al final del proyecto. 

En cuanto a mejora a nivel personal creemos necesario comentar, al resto del equipo, los problemas personales que afecten al proyecto, manteniendo así la confianza y apoyo que hemos llevado como equipo en este proyecto. 

### **8.6. Retroalimentación**

Para obtener retroalimentación sobre el juego, hemos realizado una pequeña encuesta con 14 preguntas que engloban los principales aspectos del juego. Además, queremos también comentar algunos de los comentarios que nos han ido dejando en nuestras redes sociales. 

**RESULTADOS DE LA ENCUESTA**

Hemos contactado con 21 personas para que probasen el juego antes de su publicación, de esta forma podíamos arreglar fallos que surgiesen en el último momento y tener en cuenta sus opiniones para futuras actualizaciones. A continuación, enseñamos las medias de todas las respuestas (puntuaciones disponibles para los participantes --> de 1 a 5), los resultados en gráficos y los comentarios extra que nos hayan hecho.  

Medias: 21 respuestas 

* Diversión del usuario: 4.19 
* Ambientación y temática: 4.71 
* Claridad del juego CON tutorial: 4.32 
* Claridad del juego SIN tutorial: 4 
* Comodidad del control: 3.52 
* Estilo visual: 4.81 
* Interfaz: 4.81 
* Información en pantalla: 4.14 
* Niveles de dificultad: 3 
* Música: 4.23 

**Fig103**
**Fig104**
**Fig105**
**Fig106**
**Fig107**
**Fig108**
**Fig109**
**Fig110**
**Fig111**
**Fig112**
**Fig113**
**Fig114**
**Fig115**
**Fig116**

A continuación, adjuntamos el enlace a la encuesta que hemos realizado:
- *https://forms.gle/sFKzb9hXuZAhSA8Z7* 

**COMENTARIOS DE REDES SOCIALES:**

* "Cuteeee"
* "Love this colour palette!" 
* "That looks great!"
* "Oiii, qué cuqui!!!" 
* "Oof I love this"
* "An eggsquisitely sweet place to play!" 
* "Se ve cool" 
* "Luce bien" 
* "Que pintaza tiene vuestro juego, estaremos muy atentos para cuando lo saquéis probarlo" 
* "cómo mola la idea del juego!"

## **9. Referencias**

* Plantilla para el GDD:  
*https://eldocumentalistaudiovisual.files.wordpress.com/2015/02/gdd.pdf*


* Plantilla para el GDD:  
*https://github.com/lightningopal/Juegos-para-Web-y-Redes-Sociales---Unity*

- Enlace a nuestra página web:  
*https://lightningopal.github.io/* 


