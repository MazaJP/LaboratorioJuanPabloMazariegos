namespace Proyecto_2;

public class Tablero
{    
    //definir el tamaño del tablero
    private const int TamanoTablero = 8;
        //arreglo de dos dimensiones para definir que el tablero es 8X8
        private char[,] tablero = new char[8,8];
        private Dama damaEvaluar;
        // inicia el tablero
        public Tablero()
        {
            InicializarTablero();
        }
        //menu donde aparecen las opciones 
        public void MostrarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Agregar piezas al tablero");
                Console.WriteLine("2. Ingresar la dama a evaluar");
                Console.WriteLine("3. Mostrar movimientos válidos de la dama");
                Console.WriteLine("4. Imprimir el tablero");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarPiezas();
                        break;
                    case 2:
                        IngresarDama();
                        break;
                    case 3:
                        MostrarMovimientosDama();
                        break;
                    case 4:
                        ImprimirTablero();
                        break;
                    case 5:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }
        // funcion que crea el tablero de ajedrez vacio
        private void InicializarTablero()
        {
            for (int i = 0; i < TamanoTablero; i++)
            {
                for (int j = 0; j < TamanoTablero; j++)
                {
                    tablero[i, j] = '.';
                }
            }
        }
        //funcion que permite agregar las piezas al tablero
        private void AgregarPiezas()
        {
            Console.Write("Ingrese la cantidad de piezas a agregar: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Ingrese los datos de la pieza {i + 1}:");
                AgregarPieza();
            }
              
        }
            //funcion para poner los tipos de pieza y la posicion en el tablero
        private void AgregarPieza()
        {
            string tipo, color, posicion;
            int fila, columna;
            // ingresar datos de las piezas y si estan son validas
            while (true)
            {
                Console.Write("Tipo de pieza (alfil, peón, caballo, torre, etc.): ");
                tipo = Console.ReadLine();

                Console.Write("Color (blanco o negro): ");
                color = Console.ReadLine().ToLower();

                Console.Write("Posición (ejemplo: a1, e4, f8): ");
                posicion = Console.ReadLine().ToLower();

                (fila, columna) = ConvertirPosicion(posicion);

                if (fila < 0 || fila >= TamanoTablero || columna < 0 || columna >= TamanoTablero)
                {
                    Console.WriteLine("Posición inválida. Intente nuevamente.");
                    continue;
                }

                if (tablero[fila, columna] != '.')
                {
                    Console.WriteLine("Ya hay una pieza en esta posición. Intente nuevamente.");
                    continue;
                }

                tablero[fila, columna] = color[0] == 'b' ? char.ToUpper(tipo[0]) : tipo[0];
                break;
            }
        }
        //funcion para poner la dama en el tablero
        private void IngresarDama()
        {
            Console.WriteLine("Ingrese los datos de la dama:");
            damaEvaluar = new Dama();
            damaEvaluar.IngresarDatos();

            int fila, columna;
            (fila, columna) = ConvertirPosicion(damaEvaluar.Posicion);

            tablero[fila, columna] = damaEvaluar.Color == "blanco" ? 'Q' : 'q';
        }
        //funcion para ver la lista de movimientos de la Dama
        private void MostrarMovimientosDama()
        {
            if (damaEvaluar == null)
            {
                Console.WriteLine("Primero debe ingresar la dama a evaluar.");
                return;
            }

            int fila, columna;
            (fila, columna) = ConvertirPosicion(damaEvaluar.Posicion);

            var movimientosValidos = damaEvaluar.ObtenerMovimientosValidos(tablero, fila, columna);

            Console.WriteLine("Movimientos válidos:");
            foreach (var movimiento in movimientosValidos)
            {
                Console.WriteLine(movimiento);
            }
        }
        //funcion que imprime el tablero en la consola
        private void ImprimirTablero()
        {
            for (int i = 0; i < TamanoTablero; i++)
            {
                for (int j = 0; j < TamanoTablero; j++)
                {
                    Console.Write(tablero[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

            //convierte la notacion de tablero en cordenadas para el tablero
        private (int, int) ConvertirPosicion(string pos)
        {
            int columna = pos[0] - 'a';
            int fila = TamanoTablero - (pos[1] - '0');
            return (fila, columna);
        }
}
