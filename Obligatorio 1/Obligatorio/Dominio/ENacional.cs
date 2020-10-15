using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ENacional : Excursion
    {
        #region Atributos
        private bool interesNacional;
        #endregion
        #region Propiedades
        public bool InteresNacional
        {
            get { return interesNacional; }
            set { interesNacional = value; }
        }
        #endregion
        #region Metodos
        public ENacional(bool interesNacional, string descripcion, DateTime comienzo, List<Destino> destinos, int stock) : base(descripcion, comienzo, destinos, stock)
        {
            this.interesNacional = interesNacional;
        }
        #endregion
    }
}
