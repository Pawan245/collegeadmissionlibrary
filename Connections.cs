using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Split_app
{
 public  class Connections
    {
        public MySqlConnection conn()
        {
            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd";
            var mytimes = time.ToString(format);
            DateTime date1 = DateTime.Parse("2020-12-25");
            DateTime date2 = DateTime.Parse(mytimes);
            if (date2 < date1)
            {

                // string c = "server = 182.50.133.89;  user id = admissiondb;   password= tZl9^22u;  database = ph16224652691_; port=3306; Character Set=utf8";
              // string c = "server=127.0.0.1;user id=root;database=ttt;password=root;port=3309 ;Character Set=utf8";
               string c = "server = 50.62.209.88;  user id = clgappdb;   password= F1!uux93;  database = ph20622465521_clgapp; port=3306; Character Set=utf8";
                MySqlConnection cnn;
                cnn = new MySqlConnection(c);
                return cnn;

            }
            else
            {
                return null;
            }
        }

    }
}
