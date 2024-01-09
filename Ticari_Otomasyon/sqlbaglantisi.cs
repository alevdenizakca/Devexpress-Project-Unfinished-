using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticari_Otomasyon
{
    public class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            //SqlConnection baglan = new SqlConnection(@"Data Source=DENIZ;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-J3UPQAI;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
