// Programación del botón retroceder de un navegador web usando estructuras de datos y POO

using System; // Importa el espacio de nombres System para utilizar funcionalidades básicas como la consola
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic para utilizar colecciones genéricas como Stack

namespace NavegadorWebOOP
{    public class PaginaWeb // Clase que representa una página web
    {
        public string Url { get; private set; } // Propiedad para almacenar la URL de la página web
        public string Titulo { get; private set; } // Propiedad para almacenar el título de la página web

        public PaginaWeb(string url, string titulo) // Constructor que inicializa la URL y el título de la página web
        {
            Url = url; // Inicializa la URL de la página web
            Titulo = titulo; // Inicializa el título de la página web
        }

        public override string ToString() //Se utiliza ToString para mostrar la información de la página web de manera legible
        {
            return $"{Titulo} ({Url})"; // Imprime el título y la URL de la página web en un formato legible
        }
    }

    public class Navegador // Clase que representa un navegador web
    {
        private PaginaWeb _paginaActual; //Se establece una variable privada para almacenar la página web actual que se está visualizando
        private readonly Stack<PaginaWeb> _pilaRetroceder; //Se establece una pila para almacenar las páginas web visitadas y permitir retroceder a ellas
        private readonly Stack<PaginaWeb> _pilaAvanzar; //Se establece una pila para almacenar las páginas web visitadas y permitir avanzar a ellas

        public Navegador() // Constructor que inicializa las pilas y la página actual
        {
            _pilaRetroceder = new Stack<PaginaWeb>(); // Se inicializa la pila de retroceder
            _pilaAvanzar = new Stack<PaginaWeb>(); // Se inicializa la pila de avanzar
            _paginaActual = null; // Se inicializa la página actual como nula, ya que al inicio no hay ninguna página cargada
        }

            public void NavegarA(PaginaWeb nuevaPagina) // Método que permite navegar a una nueva página web
        {
            if (_paginaActual != null) // Condición para verificar si hay una página actual cargada
            {
                _pilaRetroceder.Push(_paginaActual); //Si se cumple la condición, se agrega la página actual a la pila de retroceder antes de navegar a la nueva página
            }

            _paginaActual = nuevaPagina; // Se actualiza la página actual con la nueva página web
 
            _pilaAvanzar.Clear(); // Se limpia la pila de avanzar, ya que al navegar a una nueva página, no se puede avanzar a páginas anteriores

            Console.WriteLine($"\n[Navegando a]: {_paginaActual}"); // Se imprime el mensaje indicando que se está navegando a la nueva página web
        }

           public void Retroceder() // Método que permite retroceder a la página web anterior
        {
            if (_pilaRetroceder.Count == 0) // Condición para verificar si hay páginas en la pila de retroceder
            {
                Console.WriteLine("\n[!] No hay páginas en el historial para retroceder."); // Si no hay páginas en la pila, se imprime un mensaje indicando que no se puede retroceder
                return;
            }

            _pilaAvanzar.Push(_paginaActual); // La página actual se agrega a la pila de avanzar antes de retroceder

            _paginaActual = _pilaRetroceder.Pop(); //Se obtiene la página anterior de la pila de retroceder y se establece como la página actual

            Console.WriteLine($"\n[Botón Atrás] -> Ahora estás en: {_paginaActual}"); // Se imprime el mensaje indicando que se ha retrocedido a la página anterior
        }
        public void Avanzar() // Método que permite avanzar a la página web siguiente
        {
            if (_pilaAvanzar.Count == 0) // Condición para verificar si hay páginas en la pila de avanzar
            {
                Console.WriteLine("\n[!] No puedes avanzar más."); // Se imprime el mensaje que no se puede avanzar
                return;
            }

            _pilaRetroceder.Push(_paginaActual); // La página actual se agrega a la pila de retroceder antes de avanzar

            _paginaActual = _pilaAvanzar.Pop(); // Se establece la página siguiente de la pila de avanzar como la página actual

            Console.WriteLine($"\n[Botón Adelante] -> Ahora estás en: {_paginaActual}"); // Se imprime el mensaje 
        }
        public void MostrarEstadoActual() // Método para mostrar el  estado del navegador
        {
            // Se imprimen los detalles del estado actual del navegador, incluyendo la página actual y el número de páginas en las pilas de retroceder y avanzar
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine($"Página Actual: {(_paginaActual != null ? _paginaActual.ToString() : "Ninguna")}");
            Console.WriteLine($"Páginas para Retroceder (En Pila): {_pilaRetroceder.Count}");
            Console.WriteLine($"Páginas para Avanzar (En Pila): {_pilaAvanzar.Count}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
    class Program // Clase para ejecutar el programa y probar la funcionalidad del navegador
    {
        static void Main(string[] args) // Método principal que se ejecuta al iniciar el programa 
        {
            Navegador miNavegador = new Navegador(); // Se crea una instancia de la clase Navegador para simular la navegación web

            //Simulación de la navegación web:
            miNavegador.NavegarA(new PaginaWeb("https://google.com", "Google"));
            miNavegador.NavegarA(new PaginaWeb("https://github.com", "GitHub"));
            miNavegador.NavegarA(new PaginaWeb("https://stackoverflow.com", "Stack Overflow"));

            miNavegador.MostrarEstadoActual(); // Muestra el estado actual del navegador después de navegar a varias páginas

            miNavegador.Retroceder(); // Se presiona el botón retroceder
            
            miNavegador.Retroceder(); // Se presiona el botón retroceder nuevamente

            miNavegador.MostrarEstadoActual(); // Indica el estado actual del navegador después de retroceder dos veces

            miNavegador.Avanzar(); // Se presiona el botón avanzar para ir a la página siguiente

            miNavegador.NavegarA(new PaginaWeb("https://youtube.com", "YouTube")); // se visita una nueva página web
            
            miNavegador.MostrarEstadoActual(); // Permite ver el estado actual del navegador
            
            miNavegador.Avanzar(); // Se presiona el botón avanzar, pero no hay páginas para avanzar
        }
    }
}