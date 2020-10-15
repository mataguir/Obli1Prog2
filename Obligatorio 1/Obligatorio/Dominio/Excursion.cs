using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Excursion
    {
        #region Atributos
        private int codigo;
        private static int ultCodigo = 1000;
        private string descripcion;
        private DateTime comienzo;
        private List<Destino> destinos;
        private int stock;
        #endregion

        #region Propiedades
        public int Codigo {
            get {return codigo;}
        }
        public string Descripcion {
            get {return descripcion;}
            set {descripcion = value;}
        }
        public DateTime Comienzo {
            get { return comienzo;}
            set { comienzo = value;}
        }
        public List<Destino> Destinos {
            get {return destinos;}
        }
        public int Stock {
            get { return stock;}
            set { stock = value;}
        }
        #endregion

        #region Metodos
        public Excursion(string pdescripcion, DateTime pcomienzo, List<Destino> pdestinos, int pstock)
        {
            ultCodigo = ultCodigo +100;
            this.codigo = ultCodigo;
            this.descripcion = pdescripcion;
            this.comienzo = pcomienzo;
            this.destinos = pdestinos;
            this.stock = pstock;
        }
        #endregion
    }
}
