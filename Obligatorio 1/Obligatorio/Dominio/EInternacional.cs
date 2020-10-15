using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EInternacional : Excursion
    {
        #region Atributos
        private Aerolinea aerolinea;
        #endregion
        #region Propiedades
        public Aerolinea Aerolinea
        {
            get { return aerolinea; }
            set { aerolinea = value; }
        }
        #endregion
        #region Metodos
        public EInternacional(Aerolinea aerolinea, string descripcion, DateTime comienzo, List<Destino> destinos, int stock) : base(descripcion, comienzo, destinos, stock)
        {
            this.aerolinea = aerolinea;
        }
        #endregion
    }
}
