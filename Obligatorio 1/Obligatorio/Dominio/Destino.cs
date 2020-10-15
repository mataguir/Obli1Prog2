using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Destino
    {
        #region Atributos
        private string ciudad;
        private string pais;
        private int dias;
        private double costoDiario;
        #endregion

        #region Propiedades
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }
        public string Pais
        {
            get { return pais; }
            set { pais = value; }
        }
        public int Dias
        {
            get { return dias; }
            set { dias = value; }
        }
        public double CostoDiario
        {
            get { return costoDiario; }
            set { costoDiario = value; }
        }
        #endregion

        #region Metodos
        public Destino(string ciudad, string pais, int dias, double costoDiario)//Constructor de Destino
        {
            this.ciudad = ciudad;
            this.pais = pais;
            this.dias = dias;
            this.costoDiario = costoDiario;
        }
        #endregion

    }
}
