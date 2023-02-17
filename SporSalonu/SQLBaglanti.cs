using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SporSalonu
{
    class SQLBaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection bgl = new SqlConnection("Data Source=.;Initial Catalog=SSTS;Integrated Security=True");
            bgl.Open();
            return bgl;
        }


    }
}
