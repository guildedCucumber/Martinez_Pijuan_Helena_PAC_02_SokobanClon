PAC 2 - CLON DE SOKOBAN Y CREACIÓN DE NIVELES


Mi idea para este proyecto era hacer un código simple; al fin y al cabo, el juego original tiene unas mecánicas muy sencillas. Para evitar crear demasiados scripts y dependencias entre clases decidí implementar la herramienta de los niveles a través de ficheros .json, y así contener la información de todos los niveles en una misma escena y no depender de instanciar scriptable objects tdoo el rato. Huelga decir que he seguido las instrucciones de la PEC fielmente, en que no he perdido el tiempo en la estética o “eye-candy” del prototipo :_).


GRID MANAGER

Para inicializar el grid del juego he seguido las indicaciones de este tutorial muy simple y muy útil: https://www.youtube.com/watch?v=kkAjpQAM-jE. A partir de aquí, he expandido en la instanciación de GameObjects y la creación de listas para almacenar las posiciones de los muros, las cajas, y los puntos de fin de partida.


LEVEL DATA & PARSE LEVEL (JSON PARSING)

La plantilla inicial del fichero Json contiene un int para el nivel y un struct para cada celda, que contendrá 4 valores: su posición x/y, si es un muro o no, y su contenido (i.e. jugador, caja, final o null). 25 copia y pegas hacen un nivel de 5x5, multiplicado por los 10 niveles hacen 250 objectos que contiene el fichero json, contando con 4 valores por objeto son unas 1000 líneas de código (sin contar las líneas de paréntesis o claudator). Apenas tardé nada en crear la plantilla de cada nivel, pero estoy segura de que hay una manera más eficiente para parsear los niveles a partir de texto. En otra asignatura ya estamos trabajando con Scriptable Objects y quería explorar los distintos recursos que ofrece Unity para almacenar este tipo de información. La estructura de cada celda en el fichero se representa así:
{
    "level": x,
    "tiles": [
        {
            "x": "0",
            "y": "0",
            "wall": "true",
            "content": ""
        },
        {
            "x": "0",
            "y": "1",
            "wall": "true",
            "content": ""
        },
        {
            "x": "0",
            "y": "2",
            "wall": "true",
            "content": ""
        },
	...
    ]
}


PLAYER CONTROLLER

El script que controla el movimiento del jugador (PlayerController.cs) es muy rudimentario, pero funcional. Cuando tengo que jugar con las posiciones, como es el caso de este script que siempre debe controlar qué hay en la siguiente casilla, me gusta más separarlo todo para evitar errores, ya que se empieza a complicar a la que estás calculando dos posiciones más allá.


LEVEL MANAGER

El LevelManager.cs es una clase estática que no hereda de MonoBehaviour. En un principio, esto era porque iba a cargar la escena de nuevo y así resetear todos los scripts, cambiando solamente el nivel actual, y por lo tanto, el nivel parseado. Esto resultó bastante complicado, como se detalla en la sección de ERRORES, así que decidí destruir todos los objetos en la escena y volver a cargarlos, sin necesidad de usar el SceneManagement de Unity.

Esto me llevó un tiempo en implementar, hasta que descubrí que la clave de todo estaba en cambiar todos los métodos Start() por OnEnable(). Tales tonterías como desactivar el GridManager.cs en la línea de código 189 y el ParseLevel en la línea 190 generaban una serie de errores que eran muy difíciles de rastrear incluso debuggeando. El peor de todos fue que la posición del jugador se mantenía al cargar el siguiente nivel.

En cuanto al jugador, FINALMENTE averigué que mi error venía dado porque el script PlayerController.cs estaba activando el final del juego un paso antes de que el jugador se moviera. Este era el código original:

  // Check if the next position contains a box
  if (grid.CheckNextTile(nextPos) == "box")
  {
      //Debug.Log("Box found at " + nextPos);

      // Assign temporary box-adjacent position
      Vector3 boxPosAdjacent = nextPos + Vector3.up;

      // Check that the box is not next to a wall or another box
      if (grid.CheckNextTile(boxPosAdjacent) != "wall" && grid.CheckNextTile(boxPosAdjacent) != "box")
      {
          // Check if the next tile has a finish point
          if (grid.CheckNextTile(boxPosAdjacent) == "finish")
          {
              // Check if level is finished
              grid.FinishBox();
              ResetPlayer();
          }

          grid.MoveBox(nextPos, Vector3.up);
      }
  }

  grid.MovePlayer(nextPos);
  Debug.Log("Player moves to " + nextPos);

Por lo tanto, si el nivel acababa en el método ResetPlayer() en el GridManager.cs, que comprueba que todas las cajas estén colocadas en la posición final, se reseteaba todo el nivel y después se movía el jugador.


ERRORES

Cuando hice la primera prueba con el ejecutable me dio error. No se mostraba el grid por pantalla, y al volver a unity y darle al play me pasaba lo mismo. Me daba error de:

	NullReferenceException: Object reference not set to an instance of an object

en la línea del GridManager:

	isWall = wallPositions.Contains(currentPos);

la cual no había modificado ni sus referencias en otros scripts. Comprobé que no se me hubiera duplicado algún script y que todas las referencias estubieran asignadas en la escena, pero el error persistía, cuando antes de hacer el Build and Run sí que funcionaba el juego. La única manera con la que conseguí que volviera a funcionar fue quitando los scripts de la escena y volver a ponerlos, asignando de nuevo las referencias.

Este error se extendió cuando empecé a implementar el cambio de nivel. Tenía un juego operativo y con varios ficheros json de niveles cargados en la escena. El primer nivel funcionaba correctamente, pero al pasar al segundo nivel me daba el mismo error, a pesar de que el Debug me confirmaba que el fichero se había parseado correctamente. A día de hoy sigo sin entender de dónde venía el error, pero lo "corregí" cambiando el método para pasar al siguiente nivel [sección LEVEL MANAGER (SceneManager -> Destroy()].


PS. He añadido a la entrega un fichero día que explica la estructura de los scripts.
