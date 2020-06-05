using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceHelper.Entidades;
using EcommerceHelper.Funciones.Persistencia;

namespace EcommerceHelper.DAL
{
    public class TipoDomicilioDAL
    {

        public List<TipoDireccionEntidad> SelectAllTipoDomicilio()
        {
            using (DataTable dt = SqlClientUtility.ExecuteDataTable(SqlClientUtility.connectionStringName, CommandType.StoredProcedure, "ListarTipoDomicilio"))
            {
                List<TipoDireccionEntidad> TipoDomicilios = new List<TipoDireccionEntidad>();
                TipoDomicilios = Mapeador.Mapear<TipoDireccionEntidad>(dt);

                return TipoDomicilios;
            }
        }
    }
}
