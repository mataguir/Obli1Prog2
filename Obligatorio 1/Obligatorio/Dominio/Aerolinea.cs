using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Aerolinea
    {
        #region Atributos
        private string codigo;
        private string pais;
        #endregion

        #region Propiedades
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Pais
        {
            get { return pais; }
            set { pais = value; }
        }
        #endregion

        #region Metodos
        public Aerolinea(string codigo, string pais)
        {
            this.codigo = codigo;
            this.pais = pais;
        }
        #endregion
    }
}
