using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Agencia
    {
        #region Singleton
        private static Agencia instancia = null;

        public static Agencia Instancia
        {
            set
            {
                if (instancia == null)
                {
                    instancia = new Agencia();
                }
            }
            get { return instancia; }
        }
        #endregion

        #region Atributos
        private List<Excursion> excursiones;
        private List<Destino> destinos;
        private List<Aerolinea> aerolineas;
        private static double dolar = 43;
        #endregion

        #region Propiedades
        public List<Excursion> Excursiones
        {
            get { return excursiones; }
        }
        public List<Destino> Destinos
        {
            get { return destinos; }
        }
        public List<Aerolinea> Aerolineas
        {
            get { return aerolineas; }
        }
        public double Dolar
        {
            get { return dolar; }
            set { value = dolar; }
        }
        #endregion

        #region Metodos

        #region Métodos tradicionales
        public Agencia() //Constructor de Agencia
        {
            excursiones = new List<Excursion>();
            destinos = new List<Destino>();
            aerolineas = new List<Aerolinea>();
        }

        public bool ValidarStrings(string palabra)//Chequea que los strings tengan al menos 3 caracteres
        {
            if (palabra.Length > 2)
            {
                return true;
            }

            return false;
        }

        public bool ValidarIntPositivo(int nro) //Chequea que un int sea positivo (para cantidad de días)
        {
            if (nro > 0)
            {
                return true;
            }
            return false;
        }

        public bool ValidarDoublePositivo(double nro) //Chequea que un double sea positivo (para costo diario)
        {
            if (nro > 0)
            {
                return true;
            }
            return false;
        }
        public string ListToString(List<string> lista) // Convierte una lista de strings en un string
        {
            string frase = "";
            foreach (string s in lista)
            {
                if (frase == "")
                {
                    frase = s;
                }
                else
                {
                    frase = frase + ", " + s;
                }
            }
            return frase;
        }

        public static bool ActualizarDolar(double valor) //Actualiza el valor del dolar
        {
            if (valor > 0) //Valida que sea postivio
            {
                dolar = valor;
                return true;
            }
            return false;
        }
        #endregion

        #region Métodos-de-Destinos
        public void ListarDestinos() //Imprime la lista de destinos y sus datos
        {
            if (this.Destinos != null && this.Destinos.Count != 0) //Valida que exista y no esté vacía
            {
                int num = 0;
                foreach (Destino d in this.Destinos)
                {
                    num++;
                    Console.WriteLine(num + " - Ciudad: " + d.Ciudad + ". País: " + d.Pais + ". Días: " + d.Dias + ". Costo diario: " + d.CostoDiario + ". Estadía por persona(US$): " + d.CostoDiario * d.Dias + ". Estadía por persona ($): " + d.CostoDiario * d.Dias * Dolar);
                }
            }
            else
            {
                Console.WriteLine("Aún no hay destinos disponibles. \n");
            }
        }

        public bool AgregarDestino(string ciudad, string pais, int dias, double costoDiario)//Funcion para agregar destino
        {
            Destino destino = new Destino(ciudad, pais, dias, costoDiario);
            if (!ExisteDestino(destino) && ValidarStrings(destino.Ciudad) && ValidarStrings(destino.Pais) && ValidarIntPositivo(destino.Dias) && ValidarDoublePositivo(destino.CostoDiario))// Chequea que no exista el destino y que cumple las validaciones
            {
                this.Destinos.Add(destino);
                return true;
            }
            return false;
        }

        public bool DestinoRepetido(List<Destino> desList) //Devuelve true si una lista tiene algun destino repetido
        {
            List<string> ciudades = new List<string>();
            List<string> paises = new List<string>();
            foreach (Destino d in desList)
            {
                ciudades.Add(d.Ciudad);
                paises.Add(d.Pais);
            }
            if (ciudades.Distinct().Count() == desList.Count() || paises.Distinct().Count() == desList.Count()) //Valida que la combinacion Ciudad/Pais no exista ya
            {
                return false;
            }
            return true;
        }

        public bool ExisteDestino(Destino destino) //Chequea si ya existe el destino
        {
            foreach (Destino d in this.Destinos)
            {
                if (d.Ciudad.ToLower() == destino.Ciudad.ToLower() && d.Pais.ToLower() == destino.Pais.ToLower()) //Chequea que no exista ya
                {
                    return true;
                }
            }
            return false;
        }

        //Listar excursiones que vayan a un destino dado entre dos fechas.

        public string MostrarListaDestinos(Excursion excursion) //Dada una excursion, devuelve un string con la lista de destinos de la misma
        {
            if (excursion != null) //Valida que no sea null
            {
                string destinos = "";
                foreach (Destino d in excursion.Destinos)
                {
                    if (destinos == "")//Si está vacía agrego sólo la ciudad
                    {
                        destinos = d.Ciudad;
                    }
                    else //Si ya tiene alguna ciudad, se agrega luego
                    {
                        destinos = destinos + ", " + d.Ciudad;
                    }
                }
                return destinos;
            }
            return "";
        }
        #endregion

        #region Métodos-de-Excursiones
        public void ListarExcursiones() //Imprime la lista de excursiones y sus datos
        {
            if (this.Excursiones != null && this.Excursiones.Count != 0) //Valida que exista y no esté vacía
            {
                foreach (Excursion e in this.Excursiones)
                {
                    if (e is EInternacional) //Chequea que sea una excursion internacional
                    {
                        ImprimirExcursionInternacional((EInternacional)e);
                    }
                    else if (e is ENacional) //Sino, es una excursion nacional
                    {
                        ImprimirExcursionNacional((ENacional)e);
                    }
                }
            }
            else
            {
                Console.WriteLine("Aún no hay excursiones disponibles. \n");
            }
        }

        public void ImprimirExcursionInternacional(EInternacional e)
        {
            double desSum = 0;
            List<string> des = new List<string>();
            foreach (Destino d in e.Destinos)
            {
                desSum = d.CostoDiario + desSum;
                des.Add(d.Ciudad);
            }
            Console.WriteLine("\t" + "Código: " + e.Codigo + ". Descripcion: " + e.Descripcion + ". Comienzo: " + e.Comienzo.ToShortDateString() + ". Destinos: No hay destinos ingresados" + ". Aerolínea: " + e.Aerolinea.Codigo + ". Stock: " + e.Stock + ". Costo estimado(US$): No hay destinos ingresados" + ". Costo estimado($): No hay destinos ingresados");
        }

        public void ImprimirExcursionNacional(ENacional e)
        {
            double desSum = 0;
            List<string> des = new List<string>();
            foreach (Destino d in e.Destinos)
            {
                desSum = d.CostoDiario + desSum;
                des.Add(d.Ciudad);
            }
            Console.WriteLine("\t" + "Código: " + e.Codigo + ". Descripcion: " + e.Descripcion + ". Comienzo: " + e.Comienzo.ToShortDateString() + ". Destinos: " + ListToString(des) + ". Interés nacional: " + InteresNacionalAString(e.InteresNacional) + ". Stock: " + e.Stock + ". Costo estimado(US$): " + desSum + ". Costo estimado($): " + desSum * Dolar);
        }

        public string InteresNacionalAString(bool iN)
        {
            if (iN == true)
            {
                return "Si";
            }
            return "No";
        }

        public void AgregarENacional(bool iNacional, string descripcion, DateTime comienzo, List<Destino> destinos, int stock)//Funcion para agregar excursion nacional
        {
            ENacional excursion = new ENacional(iNacional, descripcion, comienzo, destinos, stock);
            if (SonDestinosNacionales(destinos)) //Valida que todos los destinos sean nacionales
            {
                if (destinos.Count >= 2 && !DestinoRepetido(destinos))//Valida que haya al menos 2 destinos y que no haya destinos repetidos en los destinos de la excursion
                {
                    Excursiones.Add(excursion);
                }
            }
        }

        public void AgregarEInternacional(Aerolinea aerolinea, string descripcion, DateTime comienzo, List<Destino> destinos, int stock)//Funcion para agregar excursion internacional
        {
            EInternacional excursion = new EInternacional(aerolinea, descripcion, comienzo, destinos, stock);
            if (SonDestinosInternacionales(destinos))//Valida que todos los destinos sean internacionales
            {
                if (destinos.Count >= 2 && !DestinoRepetido(destinos)) //Valida que haya al menos 2 destinos y que no haya destinos repetidos en los destinos de la excursion
                {
                    Excursiones.Add(excursion);
                }
            }
        }

        public bool SonDestinosNacionales(List<Destino> des) //Valida que todos los destinos sean nacionales
        {
            foreach (Destino d in des)
            {
                if (d.Pais.ToLower() != "uruguay")
                {
                    return false;
                }
            }
            return true;
        }

        public bool SonDestinosInternacionales(List<Destino> des) //Valida que todos los destinos sean internacionales
        {
            foreach (Destino d in des)
            {
                if (d.Pais.ToLower() == "uruguay")
                {
                    return false;
                }
            }
            return true;
        }

        public void MostrarExEntreFechas(Destino unDestino, DateTime fecha1, DateTime fecha2)
        {
            bool hayDestEntreFch = false;//Variable auxiliar para chequear que hay destinos entre las fechas indicadas
            List<Excursion> exAux = new List<Excursion>();
            if (Excursiones != null && Excursiones.Count > 0) //Valido que exista y tenga elementos
            {
                foreach (Excursion e in Excursiones)
                {
                    foreach (Destino d in e.Destinos)
                    {
                        if (unDestino.Ciudad.ToLower() == d.Ciudad.ToLower() && unDestino.Pais.ToLower() == d.Pais.ToLower()) //Valido que el destino sea el buscado
                        {
                            exAux.Add(e); //Genero una lista auxiliar de destinos para imprimir luego
                        }
                    }
                }
                if (exAux.Count > 0) //Valido que no esté vacía
                {
                    foreach (Excursion ex in exAux)
                    {
                        if (ex.Comienzo >= fecha1 && ex.Comienzo <= fecha2) //Valido que la fecha esté entre las fechas dadas
                        {
                            if (ex is EInternacional)//Chequea que sea una excursion internacional
                            {
                                ImprimirExcursionInternacional((EInternacional)ex);
                            }
                            else if (ex is ENacional)//Chequea que sea una excursion nacional
                            {
                                ImprimirExcursionNacional((ENacional)ex);
                            }
                            hayDestEntreFch = true;
                        }
                    }
                    if (!hayDestEntreFch) //Si no hay ningún destino entre las fechas indicadas
                    {
                        Console.WriteLine("No hay excursiones para el destino entre las fechas comprendidas.");
                    }
                }
                else
                {
                    Console.WriteLine("No hay excursiones para ese destino.");
                }
            }
            else
            {
                Console.WriteLine("No hay excursiones precargadas aún.");
            }
        }

        public double CalcularCostoDiarioEx(List<Excursion> excursiones)//Funcion para calcular el costo total de los destinos
        {
            double SumaDestinos = 0;
            foreach (Excursion ex in excursiones) //Acá calculo la suma de los costos diarios de los destinos
            {
                foreach (Destino des in ex.Destinos)
                {
                    SumaDestinos = des.CostoDiario + SumaDestinos;
                }
            }
            return SumaDestinos;
        }
        #endregion

        #region Métodos-de-Aerolíneas
        public void AgregarAerolinea(string codigo, string pais)//Funcion para agregar aerolinea
        {
            Aerolinea aerolinea = new Aerolinea(codigo, pais);
            if (!this.ExisteAerolinea(aerolinea)) //Chequea que no exista ya
            {
                Aerolineas.Add(aerolinea);
            }
        }

        public bool ExisteAerolinea(Aerolinea aerolinea) //Chequea si ya existe la aerolinea
        {
            foreach (Aerolinea a in Aerolineas)
            {
                if (a.Codigo == aerolinea.Codigo) //Chequea que no exista ya
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Precarga-de-Datos
        public void PrecargarDatos()
        {
            //Alta de Destinos:
            AgregarDestino("Punta del Diablo", "Uruguay", 1, 80);
            AgregarDestino("La Pedrera", "Uruguay", 1, 80);
            AgregarDestino("Salto", "Uruguay", 2, 50);
            AgregarDestino("Paysandú", "Uruguay", 2, 50);
            AgregarDestino("Punta del Este", "Uruguay", 1, 100);
            AgregarDestino("Piriapolis", "Uruguay", 1, 100);
            AgregarDestino("Montevideo", "Uruguay", 1, 80);
            AgregarDestino("Colonia", "Uruguay", 1, 80);
            AgregarDestino("Cusco", "Perú", 5, 500);
            AgregarDestino("Lima", "Perú", 2, 200);
            AgregarDestino("La Paz", "Bolivia", 2, 200);
            AgregarDestino("Rio de Janeiro", "Brasil", 5, 700);
            AgregarDestino("Florianopolis", "Brasil", 2, 300);
            AgregarDestino("Cancún", "Mexico", 5, 800);
            AgregarDestino("La Habana", "Cuba", 5, 400);
            AgregarDestino("Tierra del Fuego", "Argentina", 3, 400);
            AgregarDestino("Ushuaia", "Argentina", 3, 400);

            //Alta de Aerolineas:
            AgregarAerolinea("AA", "Argentina");
            AgregarAerolinea("PLUNA", "Uruguay");
            AgregarAerolinea("LATAM", "Chile");

            //Fechas:
            string date = "27/10/2020";
            DateTime fecha;
            fecha = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);

            string date1 = "20/11/2020";
            DateTime fecha1;
            fecha1 = DateTime.ParseExact(date1, "d/M/yyyy", CultureInfo.InvariantCulture);

            string date2 = "2/12/2020";
            DateTime fecha2;
            fecha2 = DateTime.ParseExact(date2, "d/M/yyyy", CultureInfo.InvariantCulture);

            List<Destino> d = new List<Destino>();
            Destino des = new Destino("Punta del Diablo", "Uruguay", 12, 800);
            d.Add(Destinos[0]);
            d.Add(des);

            //Alta de excursiones: 
            AgregarENacional(true, "Fin de semana en Rocha", fecha, Destinos.GetRange(0, 2), 42);
            AgregarENacional(true, "Recorrida por el litoral", fecha, Destinos.GetRange(2, 2), 42);
            AgregarENacional(true, "Fin de semana en Maldonado", fecha1, Destinos.GetRange(4, 2), 42);
            AgregarENacional(true, "Recorrida por Colonia y Montevideo", fecha1, Destinos.GetRange(6, 2), 42);
            AgregarENacional(true, "Tour Uruguay Natural", fecha2, Destinos.GetRange(0, 8), 42);
            AgregarENacional(false, "Escapada de Surf", fecha1, Destinos.GetRange(0, 2), 15);
            AgregarEInternacional(Aerolineas[0], "Tour por Perú y Bolivia", fecha2, Destinos.GetRange(8, 3), 20);
            AgregarEInternacional(Aerolineas[2], "Semana en Brasil", fecha2, Destinos.GetRange(11, 2), 120);
            AgregarEInternacional(Aerolineas[2], "Crucero por el Caribe", fecha, Destinos.GetRange(13, 2), 100);
            AgregarEInternacional(Aerolineas[2], "Tour por el sur argentino", fecha, Destinos.GetRange(15, 2), 30);
        }
        #endregion
        #endregion
    }
}
