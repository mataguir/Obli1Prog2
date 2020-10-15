using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Consola
{
    class Program
    {
        public static Agencia agencia = new Agencia();
        static void Main(string[] args)
        {
            //Precarga de datos
            agencia.PrecargarDatos();
            //Menu
            int opcion = -1; //Inicializo para que corra el while
            Console.WriteLine("Bienvenido a la Agencia");

            while (opcion != 0)
            {
                Console.WriteLine("\n"+"1.Alta Destino");
                Console.WriteLine("2.Ver destinos disponibles");
                Console.WriteLine("3.Cambiar valor del Dólar");
                Console.WriteLine("4.Precargar Excursiones");
                Console.WriteLine("5.Ver las excursiones disponibles");
                Console.WriteLine("6.Ver excursiones por fecha y lugar" + "\n");
                Console.WriteLine("Ingrese una opción o digite 0 para salir:");
                
               
                if (int.TryParse(Console.ReadLine(), out opcion)) // Valida que lo ingresado por el usuario sea un numero
                {
                    switch (opcion)
                    {
                        case 0:
                            break;
                        case 1:
                            if (!AltaDestino())
                            {
                                Console.WriteLine("El destino no fue creado.");
                            }
                            break;
                        case 2:
                            ListaDestinos();
                            break;
                        case 3:
                            if (!CambiarDolar())
                            {
                                Console.WriteLine("Dólar no fue actualizado.");
                            }
                            break;
                        case 4:
                            PrecargarExcursiones();
                            break;
                        case 5:
                            ListaExcursiones();
                            break;
                        case 6:
                            ExcursionEntreFechas();
                            break;
                        default: //En caso que no sea ninguno de los numeros de las opciones
                            Console.WriteLine("La opción ingresada no existe.");
                            break;
                    }
                }
                else
                {
                    opcion = -1;
                    Console.WriteLine("Debe ingresar un numero.");
                }
            }
            //Console.ReadKey();
        }

        public static bool AltaDestino() //Crea un destino y lo agrega a la lista
        {
            double error;
            Console.WriteLine("Ingrese una ciudad:");
            string ciudad = Console.ReadLine();
            if (ciudad.Length < 3)
            {
                Console.WriteLine("La ciudad debe tener al menos 3 letras");
                return false;
            }
            else if (double.TryParse(ciudad, out error))
            {
                Console.WriteLine("La ciudad no puede ser un número");
                return false;
            }
            Console.WriteLine("Ingrese un país:");
            string pais = Console.ReadLine();
            if (pais.Length < 3)
            {
                Console.WriteLine("El pais debe tener al menos 3 letras");
                return false;
            }
            else if (double.TryParse(ciudad, out error))
            {
                Console.WriteLine("El país no puede ser un número");
                return false;
            }
            int dias = 0;
            Console.WriteLine("Ingrese los días de estadía:");
            int.TryParse(Console.ReadLine(), out dias);
            if (dias <= 0)
            {
                Console.WriteLine("Los días de estadía deben ser positivos");
                return false;
            }
            double costoDiario = 0;
            Console.WriteLine("Ingrese el costo diario:");
            double.TryParse(Console.ReadLine(), out costoDiario);
            if (costoDiario <= 0)
            {
                Console.WriteLine("El costo diario debe ser positivo");
                return false;
            }
            bool altaCorrecta = agencia.AgregarDestino(ciudad, pais, dias, costoDiario);
            if (altaCorrecta)
            {
                Console.WriteLine("Alta correcta");
                return true;
            }
            else
            {
                Console.WriteLine("El destino ya existe");
                return false;
            }
        }

        public static void ListaDestinos() //Llama al método de listar destinos disponibles
        {
            agencia.ListarDestinos();
        }

        public static bool CambiarDolar() //Cambiar el valor del dolar
        {
            Console.WriteLine("La cotización actual del dolar es: $" + agencia.Dolar + "");
            double dolActual;
            Console.WriteLine("Ingrese el valor actual del Dólar:");
            if (double.TryParse(Console.ReadLine(), out dolActual)) //Valida que sea un número
            {
                if (dolActual > 0) //Valida que sea positivo
                {
                    if (Agencia.ActualizarDolar(dolActual)) //Chequea la doble validación
                    {
                        Console.WriteLine("Dólar actualizado exitosamente.");
                        return true;
                    }
                    return false;
                }
                else
                {
                    Console.WriteLine("El valor debe ser positivo.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("El valor debe ser un número.");
                return false;
            }
        }

        public static void PrecargarExcursiones() //Llama al metodo que precarga excursiones
        {
            agencia.PrecargarDatos();
        }

        public static void ListaExcursiones() //Llama al método de listar excursiones
        {
            agencia.ListarExcursiones();
        }

        public static bool ExcursionEntreFechas() //Listar excursiones que vayan a un destino entre dos fechas.
        {
            double error;
            Console.WriteLine("Ingrese una ciudad:");
            string ciudad = Console.ReadLine();
            if (ciudad.Length < 3)
            {
                Console.WriteLine("La ciudad debe tener al menos 3 letras");
                return false;
            }
            else if (double.TryParse(ciudad, out error))
            {
                Console.WriteLine("La ciudad no puede ser un número");
                return false;
            }
            Console.WriteLine("Ingrese un país:");
            string pais = Console.ReadLine();
            if (pais.Length < 3)
            {
                Console.WriteLine("El pais debe tener al menos 3 letras");
                return false;
            }
            else if (double.TryParse(ciudad, out error))
            {
                Console.WriteLine("El país no puede ser un número");
                return false;
            }
            Destino dest = new Destino(ciudad, pais, 1, 1); //Como para validar que un destino exista sólo considero la ciudad y el pais, agrego datos random ya que no van a ser usados
            agencia.ExisteDestino(dest);
            if (agencia.ExisteDestino(dest)) //Valida que exista el destino
            {
                Console.WriteLine("\n" + "Ingrese la fecha de inicio(dd/mm/aaaa):");
                string fecha1 = Console.ReadLine();
                Console.WriteLine("\n" + "Ingrese la fecha de fin(dd/mm/aaaa):");
                string fecha2 = Console.ReadLine();
                DateTime fchInicial = new DateTime();
                DateTime fchFin = new DateTime();
                //Valido el ingreso de fechas
                if (DateTime.TryParseExact(fecha1, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fchInicial) && DateTime.TryParseExact(fecha2, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fchFin))
                {
                    agencia.MostrarExEntreFechas(dest, fchInicial, fchFin);
                    return true;
                }
                else
                {
                    Console.WriteLine("Las fechas ingresadas no son correctas");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("No hay excursiones que vayan a ese destino.");
                return false;
            }
        }
    }
}
