namespace Proyecto_2;

    public class Dama
    {     
        //ingresar color de la dama 
        public string Color { get; private set; }
        //ingresar la posicion
        public string Posicion { get; private set; }
        //funcion que permite ingresar los datos de la reina y agregarla al tablero
        public void IngresarDatos()
        {
            while (true)
            {
                //ingresar color y posicion de la Dama
                Console.Write("Color (blanco o negro): ");
                Color = Console.ReadLine();

                Console.Write("Posición en notacion de ajedrez (letra luego numero): ");
                Posicion = Console.ReadLine();

                //permitir solo el color negro y blanco
                if (Color != "blanco" && Color != "negro")
                {
                    Console.WriteLine("Color inválido. Intente nuevamente.");
                    continue;
                }

                //limite del tablero 
                int fila, columna;
                (fila, columna) = ConvertirPosicion(Posicion);

                if (fila < 0 || fila >= 8 || columna < 0 || columna >= 8)
                {
                    Console.WriteLine("Posición inválida. Intente nuevamente.");
                    continue;
                }

                break;
            }
        }
    //lista de los movimientos que puede hacer la dama en la posicion colocada
        public List<string> ObtenerMovimientosValidos(char[,] tablero, int fila, int columna)
        {
            var movimientos = new List<string>();

            // Direcciones: vertical, horizontal, diagonales
            int[][] direcciones = new int[][]
            {
                //movi izquierda       mov derecha      mov abajo             mov derecha
                new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 },
                // diagonal izq abajo  diagonal abajo      arriba diagonal der abajo  diagonal der arriba
                new int[] { -1, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 }, new int[] { 1, 1 }
            };

            //
            foreach (var dir in direcciones)
            {
                int f = fila + dir[0];
                int c = columna + dir[1];

                while (f >= 0 && f < 8 && c >= 0 && c < 8)
                {
                    if (tablero[f, c] == '.')
                    {
                        movimientos.Add($"{(char)('a' + c)}{8 - f}: Vacío");
                    }
                    else if (char.IsUpper(tablero[f, c]) != char.IsUpper(tablero[fila, columna]))
                    {
                        movimientos.Add($"{(char)('a' + c)}{8 - f}: {tablero[f, c]}");
                        break;
                    }
                    else
                    {
                        break;
                    }

                    f += dir[0];
                    c += dir[1];
                }
            }

            return movimientos;
        }
        //convierte la notacion de tablero en cordenadas para el tablero
        private (int, int) ConvertirPosicion(string pos)
        {
            int columna = pos[0] - 'a';
            int fila = 8 - (pos[1] - '0');
            return (fila, columna);
        }
    }

