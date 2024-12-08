using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
namespace Otomasyon
{
    internal class Db
    {

        public static SqlConnection con() 
        {
        
            SqlConnection baglan=new SqlConnection("server=.;database=okulotomasyon;integrated security=true");
            baglan.Open();
            return baglan;
        
        }



    }
}
