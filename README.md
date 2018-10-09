# Videojuegos
Para el curso de Videojuegos, programado en C# con Unity
Teclas:
'x': Aceptar, poner bombas.
'c': Volver atrás.
'p': Pausar.
Flechas: Mover entre las opciones o a Bomberman.

Se ha elegido el juego: "Super Bomberman 2" para la Super Nintendo Entertainment System (SNES).
Menús:
- Menú principal (nuevo juego, batalla y password) junto con la bomba que sirve como cursor.
- Password: el usuario puede ingresar una contraseña (cuatro números del 0 al 9) y si es la correcta, se le manda al primer nivel.
- Batalla: si es tag battle o no (no está implementado los menús del tag battle), elegir si los jugadores serán HUMAN, COM o OFF,
  elegir las propiedades de la batalla (tiempo de juego, golden bomber, número de batallas, nivel de dificultad), elegir el mapa (solo dos
  mapas).
- Nuevo juego: Inicia el primer nivel.


Primer nivel:
- Un tipo de enemigo que cambia de dirección al detectar colisión, mata a Bomberman si lo colisiona, muere si colisiona con la
  explosión.
- Mapa con agua animada, paredes sólidas, escaleras, imanes que atraen bombas y giran 90° a la derecha cuando detectan colisión
  con la explosión de una bomba, un switch que debe ser activado, cofres que se pueden destruir y la puerta (hace aparecer enemigos restantes cuando detecta colisión
  con la explosión de una bomba y se abre cuando no hay enemigos y el switch ha sido activado).
- Pantalla de puntajes (Scoreboard) donde se muestra el puntaje del jugador, tiempo restante (va disminuyendo), número de vidas, número de bombas y de fuego que posee.
- Items (bomba, velocidad, fuego) que tienen una probabilidad de aparecer al destruir cada cofre.
- Bomberman que es el jugador, puede poner bombas con la 'x' y cada siempre siempre está en el centro de una cuadrícula, puede tomar ítems para aumentar
  cantidad de bombas que puede poner a la vez, el fuego (poder) de sus bombas (bombas más largas) y su velocidad. Puede morir con las explosiones
  de sus bombas, con la colisión de los enemigos, colisiona con sus bombas, las paredes, no puede colocar bombas en las escaleras ni en el
  switch, puede pausar el nivel con la tecla 'p'. El juego acaba cuando llega a la puerta abierta (música y animación de victoria)
- Cuando el jugador gasta todas sus vidas (incluyendo la vida del 0), se le manda a la pantalla de game over.

Game over:
- Pantalla donde espera 10 segundos para ver si el jugador continúa o se rinde.
- Si el jugador continúa, reinicia el nivel 1 con 3 vidas.
- Si se le pasa el tiempo y decide no continuar, se le devuelve al menú principal.
- Se muestra una constraseña en esta pantalla.

Cada entidad importante (Bomberman, enemigo, bomba, scoreboard, etc.) tiene un script asociado, así como objetos Master que controlan
el flujo del juego (decidir cuándo abrir la puerta, manejar las distintas pantallas en los menú, guardar opciones del jugador, etc.).
Asimismo, estas entidades tienen componentes de sonido, rigidbody, box colliders, entre otras. Se ha trabajado con prefabs en la mayoría
de los casos para facilitar los cambios (cambiar un enemigo cambia a todos)
