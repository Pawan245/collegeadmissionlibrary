using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Split_app;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using ZXing.QrCode;



namespace Registration_process
{
    public class Class1 : Connections
    {
        String report;
        StringBuilder builder = new StringBuilder();

        public String Page_1_Registration(String fg)
        {

            string ch = "http://www.webmazy.com/?" + fg;


            Uri myUri = new Uri(ch);

            String code = HttpUtility.ParseQueryString(myUri.Query).Get("txtCode");

            int STUDENT_ID;

            String First_Name = HttpUtility.ParseQueryString(myUri.Query).Get("FirstName");


            String MIDDLE_Name = HttpUtility.ParseQueryString(myUri.Query).Get("MiddleName");
            String Last_Name = HttpUtility.ParseQueryString(myUri.Query).Get("LastName");


            String DOB = HttpUtility.ParseQueryString(myUri.Query).Get("dob");
            String GENDER = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGender");
            String BLOODGROUP = HttpUtility.ParseQueryString(myUri.Query).Get("ddlBloodgroup");
            String MT = HttpUtility.ParseQueryString(myUri.Query).Get("mothertoungetxtName");


            String nationality = HttpUtility.ParseQueryString(myUri.Query).Get("ty");
            String email = HttpUtility.ParseQueryString(myUri.Query).Get("txtEmail");

            String MotherName = HttpUtility.ParseQueryString(myUri.Query).Get("MotherName");

            String parentoccupations = HttpUtility.ParseQueryString(myUri.Query).Get("ddlpocc");

            String pemail = HttpUtility.ParseQueryString(myUri.Query).Get("Txtpemail");

            String adhar = HttpUtility.ParseQueryString(myUri.Query).Get("txtAdhar");

            String acno = HttpUtility.ParseQueryString(myUri.Query).Get("txtAc");

            String txtifsc = HttpUtility.ParseQueryString(myUri.Query).Get("txtifsc");

            String photo = HttpContext.Current.Session["profilephotoid"].ToString();


            String sign = HttpContext.Current.Session["signhotoid"].ToString();


            String mobilee = HttpUtility.ParseQueryString(myUri.Query).Get("mobilenum");



            String castee = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGendert");
            String diasb  = HttpUtility.ParseQueryString(myUri.Query).Get("ddlBloodgroupt");


            String area = HttpUtility.ParseQueryString(myUri.Query).Get("tkk");
            String fathername = HttpUtility.ParseQueryString(myUri.Query).Get("FatherName");

            String bankname = HttpUtility.ParseQueryString(myUri.Query).Get("bankname");


            String regid = null;
            String pass = null;


            pass = Encryptx(getorderidy());

            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                STUDENT_ID = getmaxst(cnn);


                regid = time2.ToString("yyyyMMdd").Trim() + getorderid().Trim() + STUDENT_ID;



                myCommand.CommandText = "INSERT INTO `studentrecord`( `STUDENT_ID` , `First_Name`, `MIDDLE_Name`,`Last_Name` , `DOB` ,  `GENDER` ,  `BLOODGROUP` ,  `MT` ,  `NATION` ,  `EMAIL` ,  `MOTHERN` ,  `POCCU` ,  `PEMAIL` ,  `AD` ,  `AC` , `IFSC` ,  `PHOTO`,  `SIGN` ,  `REGDATE` ,  `PAYMENTST`,`REGID`,`PASS`,`STEPCOMPLETED`,`LastLogin_Date` ,`mobilenum`,`casterec`,`dis`,`area`,`fathername`,`bankname`) VALUES ('" + STUDENT_ID + "', '" + First_Name + "', '" + MIDDLE_Name + "', '" + Last_Name + "', '" + DOB + "', '" + GENDER + "', '" + BLOODGROUP + "', '" + MT + "', '" + nationality + "', '" + email + "', '" + MotherName + "', '" + parentoccupations + "', '" + pemail + "', '" + adhar + "', '" + acno + "', '" + txtifsc + "', '" + photo + "', '" + sign + "', '" + mytime + "', '0', '" + regid.Trim() + "', '" + pass + "','1', '" + mytime + "','" + mobilee+ "','" +castee+ "','" +diasb+ "','" + area+ "','" +fathername+ "','"+bankname+"')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();
                /***********************************************************/
               sendemail(regid, Decryptx(pass), email);
                HttpContext.Current.Session["regid"] = regid;
                HttpContext.Current.Session["pwd"] = Decryptx(pass);
                return "success";

            }
            catch (Exception e)
            {
                try
                {

                    HttpContext.Current.Session["regid"] = null;
                    HttpContext.Current.Session["pwd"] = null;
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

             //   return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);






            return null;








        }

        public int getmaxatt(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(ATTACHEMENT_ID) from attachement";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }






        public int getmaxidofexp(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(Photo_ID) from tblprofilephoto";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }


        /****************For getting max student **********************/









        public int getmaxst(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(STUDENT_ID) from studentrecord";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }


        public String ProcessRequest(HttpContext context)
        {





            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";










                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;


                    decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
                    if (size > 150)
                    {

                        //context.Response.Write("errory");
                        return "errory";
                        // Response.Write("<script language=\"javascript\"> alert('File is too large(File Should Be upto  50 kb and jpg/jpeg ')</script>");

                    }
                    else
                    {


                        string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                        if (FileExtension == "jpg" || FileExtension == "jpeg" || FileExtension == "png")

                        {
                            if (!string.IsNullOrEmpty(fileName))
                            {

                                String st;
                                int E_ID = 0;
                                fileExtension = Path.GetExtension(fileName);
                                str_image = "MyPHOTO_" + numFiles.ToString() + getpicid().ToString() + fileExtension;
                                string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                                file.SaveAs(pathToSave_100);
                                // context.Response.Write(str_image);

                                MySqlConnection cnn;
                                cnn = conn();


                                cnn.Open();

                                //  System.Diagnostics.Debug.Write(maxID);

                                MySqlCommand myCommand = cnn.CreateCommand();
                                MySqlTransaction myTrans;


                                myTrans = cnn.BeginTransaction();

                                myCommand.Connection = cnn;
                                myCommand.Transaction = myTrans;





                                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                                string format = "yyyy-MM-dd HH:mm:ss ";
                                var mytime = time.ToString(format);
                                try
                                {

                                    E_ID = getmaxidofexp(cnn);
                                    myCommand.CommandText = "INSERT INTO `tblprofilephoto`(`Photo_ID`, `Processed`, `Cancelled`, `Start_Date`, `End_Date`, `Location_Data`, `Title`) VALUES ('" + E_ID + "', '0', '0', '" + mytime + "', '" + mytime + "', '" + str_image + "', 'na')";
                                    myCommand.ExecuteNonQuery();

                                    myTrans.Commit();
                                    st = "success";

                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        myTrans.Rollback();
                                    }
                                    catch (SqlException ex)
                                    {
                                        if (myTrans.Connection != null)
                                        {
                                            st = "error";
                                        }
                                    }

                                    st = "error";
                                    //  System.Diagnostics.Debug.Write(e.ToString());
                                    // return e.ToString();

                                }
                                finally
                                {
                                    cnn.Close();
                                }
                                if (st.Equals("success"))
                                {
                                    //   HttpContext.Current.Session["profilephotoid"] = "";
                                    try
                                    {


                                        HttpContext.Current.Session["profilephotoid"] = "" + E_ID + "";

                                        // context.Session["profilephotoid"]= E_ID;


                                    }
                                    catch (Exception ex)
                                    {
                                        System.Diagnostics.Debug.Write(ex.ToString());
                                    }
                                    return str_image;
                                }
                                else
                                {
                                    return "";
                                }
                            }

                        }
                        else
                        {
                            //  context.Response.Write("errorz");
                            return "errorz";
                        }

                    }





                }
                //  database record update logic here  ()


                //context.Response.Write("errory");

            }
            catch (Exception ac)
            {

            }



            return null;
        }



        public String ProcessRequest2(HttpContext context)
        {





            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";










                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;


                    decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
                    if (size > 150)
                    {

                        //context.Response.Write("errory");
                        return "errory";
                        // Response.Write("<script language=\"javascript\"> alert('File is too large(File Should Be upto  50 kb and jpg/jpeg ')</script>");

                    }
                    else
                    {


                        string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                        if (FileExtension == "jpg" || FileExtension == "jpeg" || FileExtension == "png")

                        {
                            if (!string.IsNullOrEmpty(fileName))
                            {

                                String st;
                                int E_ID2 = 0;
                                fileExtension = Path.GetExtension(fileName);
                                str_image = "MyPHOTO_" + numFiles.ToString() + getpicid().ToString() + fileExtension;
                                string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                                file.SaveAs(pathToSave_100);
                                // context.Response.Write(str_image);

                                MySqlConnection cnn;
                                cnn = conn();


                                cnn.Open();

                                //  System.Diagnostics.Debug.Write(maxID);

                                MySqlCommand myCommand = cnn.CreateCommand();
                                MySqlTransaction myTrans;


                                myTrans = cnn.BeginTransaction();

                                myCommand.Connection = cnn;
                                myCommand.Transaction = myTrans;





                                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                                string format = "yyyy-MM-dd HH:mm:ss ";
                                var mytime = time.ToString(format);
                                try
                                {

                                    E_ID2 = getmaxidofexp(cnn);
                                    // System.Diagnostics.Debug.Write(E_ID2);
                                    myCommand.CommandText = "INSERT INTO `tblprofilephoto`(`Photo_ID`, `Processed`, `Cancelled`, `Start_Date`, `End_Date`, `Location_Data`, `Title`) VALUES ('" + E_ID2 + "', '0', '0', '" + mytime + "', '" + mytime + "', '" + str_image + "', 'na')";
                                    myCommand.ExecuteNonQuery();

                                    myTrans.Commit();
                                    st = "success";

                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        myTrans.Rollback();
                                    }
                                    catch (SqlException ex)
                                    {
                                        if (myTrans.Connection != null)
                                        {
                                            st = "error";
                                        }
                                    }

                                    st = "error";
                                    //  System.Diagnostics.Debug.Write(e.ToString());
                                    // return e.ToString();

                                }
                                finally
                                {
                                    cnn.Close();
                                }
                                if (st.Equals("success"))
                                {


                                    try
                                    {



                                        // Session["FirstName"] = E_ID2;
                                        //  System.Diagnostics.Debug.Write((string)(context.Session["FirstName"]));

                                        HttpContext.Current.Session["signhotoid"] = "" + E_ID2 + "";
                                    }
                                    catch (Exception ex)
                                    {
                                        System.Diagnostics.Debug.Write(ex.ToString());
                                    }

                                    // HttpContext.Current.Session["signhotoid"] = E_ID2;

                                    //  System.Diagnostics.Debug.Write(E_ID2);
                                    return str_image;
                                }
                                else
                                {
                                    return "";
                                }
                            }

                        }
                        else
                        {
                            //  context.Response.Write("errorz");
                            return "errorz";
                        }

                    }





                }
                //  database record update logic here  ()


                //context.Response.Write("errory");

            }
            catch (Exception ac)
            {

            }



            return null;
        }





        public String ProcessRequest3(HttpContext context, String regid)
        {





            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploaxcv/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";











                HttpPostedFile file = context.Request.Files["file"];
                string fileName = file.FileName;

                System.Diagnostics.Debug.Write(fileName);
                string fileExtension = file.ContentType;
                string title = context.Request.Form["title"];
                System.Diagnostics.Debug.Write(title);

                decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
                if (size > 1024)
                {

                    //context.Response.Write("errory");
                    return "errory";
                    // Response.Write("<script language=\"javascript\"> alert('File is too large(File Should Be upto  50 kb and jpg/jpeg ')</script>");

                }
                else
                {


                    string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                    if (FileExtension == "jpg" || FileExtension == "jpeg" || FileExtension == "png" || FileExtension == "bmp" || FileExtension == "pdf" || FileExtension == "doc" || FileExtension == "docx")

                    {
                        if (!string.IsNullOrEmpty(fileName))
                        {

                            String st;
                            int ATTACH_ID = 0;
                            fileExtension = Path.GetExtension(fileName);
                            str_image = "MyPHOTO_" + numFiles.ToString() + getpicid().ToString() + fileExtension;
                            string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploaxcv/") + str_image;
                            file.SaveAs(pathToSave_100);
                            // context.Response.Write(str_image);

                            MySqlConnection cnn;
                            cnn = conn();


                            cnn.Open();

                            //  System.Diagnostics.Debug.Write(maxID);

                            MySqlCommand myCommand = cnn.CreateCommand();
                            MySqlTransaction myTrans;


                            myTrans = cnn.BeginTransaction();

                            myCommand.Connection = cnn;
                            myCommand.Transaction = myTrans;





                            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                            string format = "yyyy-MM-dd HH:mm:ss ";
                            var mytime = time.ToString(format);
                            try
                            {

                                ATTACH_ID = getmaxatt(cnn);
                                // System.Diagnostics.Debug.Write(E_ID2);
                                myCommand.CommandText = "INSERT INTO `attachement` (`ATTACHEMENT_ID`, `STUDENT_ID`, `REGID_ID`, `ATTACHEMENTID`, `ATTACHDOCTITLE`, `ISEDITED`, `DATE`,`SESSION`,`TITLE`) VALUES ('" + ATTACH_ID + "', '" + regid + "', '" + regid + "', '" + ATTACH_ID + "', '" + str_image + "', '0', '" + mytime + "','" + mytime + "','" + title + "')";
                                myCommand.ExecuteNonQuery();

                                myTrans.Commit();
                                st = "success";

                            }
                            catch (Exception e)
                            {
                                try
                                {
                                    myTrans.Rollback();
                                }
                                catch (SqlException ex)
                                {
                                    if (myTrans.Connection != null)
                                    {
                                        st = "error";
                                    }
                                }

                                st = "error";


                            }
                            finally
                            {
                                cnn.Close();
                            }
                            if (st.Equals("success"))
                            {


                                return st;
                            }
                            else
                            {
                                return "error";
                            }
                        }

                    }
                    else
                    {
                        //  context.Response.Write("errorz");
                        return "errorz";
                    }

                }






                //  database record update logic here  ()


                //context.Response.Write("errory");

            }
            catch (Exception ac)
            {
                System.Diagnostics.Debug.Write(ac.ToString());
            }



            return null;
        }


        public string getpicid()
        {


            builder.Append(RandomNumberd(10000, 99999));
            return builder.ToString();
        }

        private int RandomNumberd(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string getorderid()
        {


            builder.Append(RandomNumber(100, 999));
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        public string getorderidy()
        {


            builder.Append(RandomNumbery(1000, 9999));
            return builder.ToString();
        }

        private int RandomNumbery(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }



        public string Encryptx(string clearText)
        {
            string EncryptionKey = "MDKV3SPRNI79Q82";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76, 0x7, 0x0 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }




        public string Decryptx(string cipherText)
        {
            string EncryptionKey = "MDKV3SPRNI79Q82";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76, 0x7, 0x0 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }






        public string Student_Login(String passcode, string Userid, string password)
        {

            if (passcode != String.Empty && Userid != String.Empty && password != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = Userid.Trim();
                passwordx = Encryptx(password.Trim());

                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM studentrecord  AS t  WHERE      t.REGID = '" + Useridx.Trim().ToString() + "' AND t.PASS='" + passwordx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ju = ds.Tables[0].Rows[0][5].ToString();
                        mk = ds.Tables[0].Rows[0][23].ToString();


                        Class1 yt = new Class1();

                        yt.UpdateLoginDate(ju);

                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session["TYPEX_UOXX"] = mk;
                        HttpContext.Current.Session["TYPEX_UOKKXX"] = ju;


                        HttpContext.Current.Session["PRTU_UOkT"] = "WATER_HUB@56_834599T";

                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = mk;
                        dr.regid = ju;
                        dr.status = "success";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        cnn.Close();
                        return datan;


                    }
                    else
                    {
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "na";
                        dr.status = "error";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);

                        return datan;
                    }
                }
                catch
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = "errorx";



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }



        public void UpdateLoginDate(String adminId)
        {
            MySqlConnection cnn;
            cnn = conn();

            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytimes = time.ToString(format);

            string cmdString = "Update studentrecord  Set LastLogin_Date ='" + mytimes + "' where REGID =" + adminId.Trim() + " ";
            MySqlCommand cmd = new MySqlCommand(cmdString, cnn);


            cnn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }

        }






        public string Selectn(String passcode, string regid)
        {

            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM studentrecord  AS t  WHERE      t.REGID = '" + Useridx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {




                        List<Studentrecord> er = new List<Studentrecord>();

                        Studentrecord dr = new Studentrecord();





                        dr.firstname = ds.Tables[0].Rows[0][1].ToString();
                        dr.middlename = ds.Tables[0].Rows[0][2].ToString();
                        dr.lastname = ds.Tables[0].Rows[0][3].ToString();
                        dr.dob = ds.Tables[0].Rows[0][7].ToString();
                        dr.gender = ds.Tables[0].Rows[0][8].ToString();
                        dr.bllodgroup = ds.Tables[0].Rows[0][9].ToString();
                        dr.mt = ds.Tables[0].Rows[0][10].ToString();
                        dr.nation = ds.Tables[0].Rows[0][11].ToString();
                        dr.eid = ds.Tables[0].Rows[0][12].ToString();
                        dr.mothername = ds.Tables[0].Rows[0][13].ToString();
                        dr.poccu = ds.Tables[0].Rows[0][14].ToString();
                        dr.pemail = ds.Tables[0].Rows[0][15].ToString();
                        dr.aadhar = ds.Tables[0].Rows[0][16].ToString();
                        dr.bank = ds.Tables[0].Rows[0][17].ToString();
                        dr.ifsc = ds.Tables[0].Rows[0][18].ToString();

                        dr.mobilenumber = ds.Tables[0].Rows[0][25].ToString();
                        dr.area= ds.Tables[0].Rows[0][28].ToString();
                        dr.fathername = ds.Tables[0].Rows[0][29].ToString();
                        dr.bankname = ds.Tables[0].Rows[0][30].ToString();
                        dr.caste = ds.Tables[0].Rows[0][26].ToString();

                        dr.dis = ds.Tables[0].Rows[0][27].ToString();






                        String photo = ds.Tables[0].Rows[0][19].ToString();

                        string ph = getph(photo, cnn);

                        string TargetLocation = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                        dr.photo = MapURL(TargetLocation) + ph;
                        String sign = ds.Tables[0].Rows[0][20].ToString();


                        String sig = getph(sign, cnn);
                        dr.sign = MapURL(TargetLocation) + sig;

                        dr.status = ds.Tables[0].Rows[0][23].ToString();
                        HttpContext.Current.Session["st"] = ds.Tables[0].Rows[0][23].ToString();
                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        //System.Diagnostics.Debug.Write(datan);

                        cnn.Close();
                        return datan;


                    }
                    else
                    {
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "na";
                        dr.status = "error";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);

                        cnn.Close();

                        String datan = JsonConvert.SerializeObject(er, serializer);

                        return datan;
                    }
                }
                catch
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = "errorx";



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }





        public string getph(String sign, MySqlConnection cnn)
        {
            string nm = null;
            try
            {



                String cd4 = "SELECT `Location_Data` FROM `tblprofilephoto` WHERE `Photo_ID`='" + sign + "' ";




                MySqlCommand cmd340 = new MySqlCommand(cd4, cnn);


                nm = Convert.ToString(cmd340.ExecuteScalar());

            }
            catch (Exception ex)
            {
                nm = null;
            }
            return nm;

        }



        private string MapURL(string path)
        {
            string appPath = HttpContext.Current.Server.MapPath("/").ToLower();
            String h = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
            return string.Format("" + h + "/{0}", path.ToLower().Replace(appPath, "").Replace(@"\", "/"));
        }



        /*************************************** Creteria fillup start ***************************************/


        public String Page_2_Registration(String fg, String regid)
        {

            string ch = "http://www.webmazy.com/?" + fg;


            Uri myUri = new Uri(ch);



            int CREID;

            String lastqulaficationn = HttpUtility.ParseQueryString(myUri.Query).Get("lastqulaficationn");


            String examname = HttpUtility.ParseQueryString(myUri.Query).Get("FirstName");

            String prvedu = HttpUtility.ParseQueryString(myUri.Query).Get("boardorunivercity");

            String totalmarks = HttpUtility.ParseQueryString(myUri.Query).Get("LastName2");

            String obtainmarks = HttpUtility.ParseQueryString(myUri.Query).Get("datepickert");


            String percentage = HttpUtility.ParseQueryString(myUri.Query).Get("mothertoungetxtName");
            String passingmonth = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGender");
            String passingyear = HttpUtility.ParseQueryString(myUri.Query).Get("txtEmail");
            String passingclass = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGender2");
            String marksheetnum = HttpUtility.ParseQueryString(myUri.Query).Get("txtEmail2");

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();
            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getmaxctid(cnn);
                myCommand.CommandText = "INSERT INTO `criteriadetails`(" +
                    " `CriteriaDetails_ID` ," +
                    " `STUDENT_ID`," +
                    " `Last_qualification`," +
                    "`REGID` ," +
                    " `Exam_Name` ," +
                    "  `ADMINCRETAREA` ," +
                    "  `Board_University_Name` ," +
                    "  `Total_Marks` ," +
                    "  `Obtain_Marks` , " +

                    "  `Percentage` , " +
                    "  `passingclass` , " +
                    "  `marksheetnumber` , " +
                    "  `passingyear` , " +
                    "  `passingmonth` , " +



                    " `DATE` " +
                    ") VALUES (" +
                    "'" + CREID + "', " +
                    "'" + regid.Trim() + "', " +
                    "'" + lastqulaficationn + "', " +
                    "'" + regid.Trim() + "', " +
                    "'" + examname + "'," +
                    " '" + prvedu + "'," +
                    " '" + prvedu + "'," +
                    " '" +totalmarks+ "'," +
                    " '" + obtainmarks + "'," +

                     " '" +percentage+ "'," +
                      " '" +passingclass + "'," +
                       " '" + marksheetnum + "'," +
                        " '" + passingyear + "'," +
                         " '" +passingmonth + "'," +


                    " '" + mytime + "'" +
                    ")";
                myCommand.ExecuteNonQuery();
                UpdateStatus(regid, cnn, "2", myTrans);
                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getmaxctid(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(CriteriaDetails_ID) from criteriadetails";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }


        public void UpdateStatus(String regid, MySqlConnection cnn, String status, MySqlTransaction myTrans)
        {





            string cmdString = " Update studentrecord AS t  Set t.STEPCOMPLETED ='" + status.Trim() + "'  where t.REGID ='" + regid.Trim() + "' ";
            MySqlCommand cmd = new MySqlCommand(cmdString, cnn);



            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex3)
                {
                    if (myTrans.Connection != null)
                    {



                    }
                }
            }
            finally
            {

            }

        }



        public string Selectcreteria(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM criteriadetails  AS t  WHERE      t.REGID = '" + Useridx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    System.Diagnostics.Debug.Write("count" + ds.Tables[0].Rows.Count + "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {




                        List<Creteria> er = new List<Creteria>();

                        Creteria dr = new Creteria();





                        dr.lastqulaficationn = ds.Tables[0].Rows[0][2].ToString();
                        dr.examname = ds.Tables[0].Rows[0][4].ToString();



                        dr.boardunivn = ds.Tables[0].Rows[0][5].ToString();
                        dr.totalmarks = ds.Tables[0].Rows[0][7].ToString();
                        dr.obtainmarks = ds.Tables[0].Rows[0][8].ToString();



                        dr.percentage = ds.Tables[0].Rows[0][10].ToString();
                        dr.passingmonth = ds.Tables[0].Rows[0][13].ToString();
                        dr.passingyear = ds.Tables[0].Rows[0][14].ToString();
                        dr.passingclass = ds.Tables[0].Rows[0][11].ToString();
                        dr.marksheetno = ds.Tables[0].Rows[0][12].ToString();



                        String d = getstatus(Useridx, cnn);
                        HttpContext.Current.Session["st"] = d;

                        dr.status = d;
                        System.Diagnostics.Debug.Write("ety" + d + "");

                        er.Add(dr);

                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                        cnn.Close();
                        return datan;


                    }


                    else
                    {


                        return "sd";
                    }


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }





        public String getstatus(String regid, MySqlConnection cnn)
        {





            //  cnn.Open();

            try
            {

                string cmdString = "select STEPCOMPLETED from studentrecord where REGID  ='" + regid.Trim() + "' ";
                MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                return Convert.ToString(cmd.ExecuteScalar());



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("ex" + ex.ToString() + "");

                return ex.ToString();
            }
            finally
            {


            }

        }








        /*********************************creteria Stop **********************************************************/



        /***********************Postal setup ***************************/
        public String Page_3_Registration(String fg, String regid)
        {

            string ch = "http://www.webmazy.com/?" + fg;


            Uri myUri = new Uri(ch);



            int POSTALID;

            String currentaddress = HttpUtility.ParseQueryString(myUri.Query).Get("dob");


            String country = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGender");

            String state = HttpUtility.ParseQueryString(myUri.Query).Get("ddlBloodgroup");

            String city = HttpUtility.ParseQueryString(myUri.Query).Get("city");

            String pin = HttpUtility.ParseQueryString(myUri.Query).Get("pin");

            String land = HttpUtility.ParseQueryString(myUri.Query).Get("land");

            String mobile = HttpUtility.ParseQueryString(myUri.Query).Get("mobile");



            String pcurrentaddress = HttpUtility.ParseQueryString(myUri.Query).Get("pdob");


            String pcountry = HttpUtility.ParseQueryString(myUri.Query).Get("pddlGender");

            String pstate = HttpUtility.ParseQueryString(myUri.Query).Get("pddlBloodgroup");

            String pcity = HttpUtility.ParseQueryString(myUri.Query).Get("pcity");

            String ppin = HttpUtility.ParseQueryString(myUri.Query).Get("ppin");

            String pland = HttpUtility.ParseQueryString(myUri.Query).Get("pland");

            String pmobile = HttpUtility.ParseQueryString(myUri.Query).Get("pmobile");



            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                POSTALID = getmaxpostal(cnn);
                myCommand.CommandText = "INSERT INTO `postaldetails`(`PostalDetails_ID`,`STUDENT_ID`,`CURRENTADDRESS`,`COUNTRY`,`STATE`,`REGID`,`CITY`,	`ZIPCODE` ,	`LANDLINENUMBER` ,	`MOBILE`,	`PCURRENTADDRESS` ,	`PCOUNTRY`,	`PSTATE` ,	`PCITY`,`PZIPCODE` ,`PLANDLINENUMBER` ,	`PMOBILE` ,	`DATE`) VALUES ('" + POSTALID + "','" + regid.Trim() + "','" + currentaddress + "','" + country + "','" + state + "','" + regid.Trim() + "','" + city + "','" + pin + "' ,'" + land + "' ,'" + mobile + "',	'" + pcurrentaddress + "','" + pcountry + "','" + pstate + "','" + pcity + "','" + ppin + "' ,'" + pland + "' ,'" + pmobile + "','" + mytime + "')";
                myCommand.ExecuteNonQuery();
                UpdateStatus(regid.Trim(), cnn, "3", myTrans);
                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getmaxpostal(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(PostalDetails_ID) from postaldetails";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }

        public string Selectpostal(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM postaldetails  AS t  WHERE      t.REGID = '" + Useridx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    System.Diagnostics.Debug.Write("count" + ds.Tables[0].Rows.Count + "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {




                        List<Postaldetails> er = new List<Postaldetails>();

                        Postaldetails dr = new Postaldetails();


                        dr.pcadd = ds.Tables[0].Rows[0][10].ToString(); ;
                        dr.cadd = ds.Tables[0].Rows[0][2].ToString();
                        dr.pcountry = ds.Tables[0].Rows[0][11].ToString();
                        dr.country = ds.Tables[0].Rows[0][3].ToString();
                        dr.pstate = ds.Tables[0].Rows[0][12].ToString();
                        dr.state = ds.Tables[0].Rows[0][4].ToString();
                        dr.pcity = ds.Tables[0].Rows[0][13].ToString();
                        dr.city = ds.Tables[0].Rows[0][6].ToString();
                        dr.ppinn = ds.Tables[0].Rows[0][14].ToString();
                        dr.pin = ds.Tables[0].Rows[0][7].ToString();
                        dr.plandd = ds.Tables[0].Rows[0][15].ToString();
                        dr.lan = ds.Tables[0].Rows[0][8].ToString();
                        dr.pmob = ds.Tables[0].Rows[0][16].ToString();
                        dr.mob = ds.Tables[0].Rows[0][9].ToString();




                        String d = getstatus(Useridx, cnn);
                        HttpContext.Current.Session["st"] = d;

                        dr.status = d;
                        System.Diagnostics.Debug.Write("ety" + d + "");

                        er.Add(dr);

                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                        cnn.Close();
                        return datan;


                    }


                    else
                    {


                        return "sd";
                    }


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /**************Postal **************************************************/




        public String getstatusx(String regid)
        {



            MySqlConnection cnn;
            cnn = conn();

            cnn.Open();

            try
            {

                string cmdString = "select STEPCOMPLETED from studentrecord where REGID  ='" + regid.Trim() + "' ";
                MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                return Convert.ToString(cmd.ExecuteScalar());



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("ex" + ex.ToString() + "");

                return ex.ToString();
            }
            finally
            {
                cnn.Close();

            }

        }



        /*****get education details *****************************/





        public string geteducationrecord(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM educationdetailsbystudent  AS t  WHERE      t.REGID_ID = '" + Useridx.Trim().ToString() + "' AND t.ISEDITED='0'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    List<educationdetails> er = new List<educationdetails>();

                    while (rdrt.Read())
                    {





                        educationdetails dr = new educationdetails();


                        dr.exam = rdrt[4].ToString(); ;
                        dr.pmonth = rdrt[5].ToString();
                        dr.pyear = rdrt[16].ToString();
                        dr.id = rdrt[10].ToString();
                        dr.id2 = rdrt[0].ToString();





                        er.Add(dr);
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;





                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        public string geteducationrecordt(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM educationdetailsbystudent  AS t  WHERE      t.REGID_ID = '" + Useridx.Trim().ToString() + "' AND t.ISEDITED='1'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    List<educationdetails> er = new List<educationdetails>();

                    while (rdrt.Read())
                    {





                        educationdetails dr = new educationdetails();


                        dr.exam = rdrt[4].ToString(); ;
                        dr.pmonth = rdrt[5].ToString();
                        dr.pyear = rdrt[16].ToString();
                        dr.id = rdrt[10].ToString();
                        dr.id2 = rdrt[0].ToString();





                        er.Add(dr);
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;





                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /**************************************Page 4 Setup*********************************************/


        public String setupedu(String fg, String regid)
        {

            string ch = "http://www.webmazy.com/?" + fg;


            Uri myUri = new Uri(ch);

            //  lastqulaficationn = SSC & FirstName = VVC & MiddleName = VV & LastName = VV & dob = 33 & mothertoungetxtName = 33 & ddlGender = November & txtEmail = 33333 & ddlGender2 = 3 & txtEmail2 = 333320count0sdcount0The thread 0x205c has exited with code 0(0x0).


            int MAXSUBID;

            String p1 = HttpUtility.ParseQueryString(myUri.Query).Get("lastqulaficationn");


            String p2 = HttpUtility.ParseQueryString(myUri.Query).Get("FirstName");

            String p3 = HttpUtility.ParseQueryString(myUri.Query).Get("MiddleName");

            String p4 = HttpUtility.ParseQueryString(myUri.Query).Get("LastName");

            String p5 = HttpUtility.ParseQueryString(myUri.Query).Get("dob");

            String p6 = HttpUtility.ParseQueryString(myUri.Query).Get("mothertoungetxtName");

            String p7 = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGender");

            double pER = (Convert.ToDouble(p5) / Convert.ToDouble(p6)) * 100;

            String p8 = HttpUtility.ParseQueryString(myUri.Query).Get("txtEmail");


            String p9 = HttpUtility.ParseQueryString(myUri.Query).Get("ddlGender2");

            String p10 = HttpUtility.ParseQueryString(myUri.Query).Get("txtEmail2");




            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                MAXSUBID = getmaxsubbyst(cnn);
                myCommand.CommandText = "INSERT INTO `educationdetailsbystudent` (`EducationDetailsbystudent_ID`, `STUDENT_ID`, `REGID_ID`, `LASTQUALIFICATION`, `EXAMNAME`, `BOARDNAMEUNIVERCITY`, `SPECIALSUBJECT`, `OBTMARKS`, `TOTALMARKS`, `PERCENTAGE`, `PASSINGYEAR`,`PASSINGCLASS`, `SEATNUMBER`, `ISEDITED`, `DATE`, `SESSION`,`PASSINGMONTH`) VALUES('" + MAXSUBID + "', '" + regid + "', '" + regid + "', '" + p1 + "', '" + p2 + "', '" + p3 + "', '" + p4 + "', '" + p5 + "', '" + p6 + "', '" + pER + "', '" + p8 + "','" + p9 + "','" + p10 + "','0','" + mytime + "','" + mytime + "','" + p7 + "')";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();



                // UpdateStatus(regid.Trim(), cnn, "3");

                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }





        public int getmaxsubbyst(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(EducationDetailsbystudent_ID) from educationdetailsbystudent";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        public String Page_4_Registration(String fg, String regid)
        {




            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                myCommand.CommandText = "Update educationdetailsbystudent Set ISEDITED ='1' where REGID_ID =" + regid.Trim() + " ";
                myCommand.ExecuteNonQuery();
                UpdateStatus(regid.Trim(), cnn, "4", myTrans);
                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }





        /***************************************get attachment***********************************************************/


        public string getattach(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM attachement  AS t  WHERE      t.REGID_ID = '" + Useridx.Trim().ToString() + "' AND t.ISEDITED='0'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<attach> er = new List<attach>();
                    while (rdrt.Read())
                    {


                        attach dr = new attach();


                        dr.sr = i;
                        String gh = rdrt[8].ToString();
                        switch (gh)
                        {
                            case "1":

                                dr.title = "Gradution Marksheet";
                                break;

                            case "2":
                                dr.title = "Medical Certificate-Physically Challenged Students";
                                break;

                            case "3":
                                dr.title = "Non-Creamy Layer Certificate";
                                break;

                            case "4":
                                dr.title = "Post Gradution Marksheet";
                                break;
                            case "5":
                                dr.title = "SSC Marksheet";
                                break;



                        }


                        dr.id2 = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }



        public string getattachx(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM attachement  AS t  WHERE      t.REGID_ID = '" + Useridx.Trim().ToString() + "' AND t.ISEDITED='1'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<attach> er = new List<attach>();
                    while (rdrt.Read())
                    {


                        attach dr = new attach();


                        dr.sr = i;
                        String gh = rdrt[8].ToString();
                        switch (gh)
                        {
                            case "1":

                                dr.title = "Gradution Marksheet";
                                break;

                            case "2":
                                dr.title = "Medical Certificate-Physically Challenged Students";
                                break;

                            case "3":
                                dr.title = "Non-Creamy Layer Certificate";
                                break;

                            case "4":
                                dr.title = "Post Gradution Marksheet";
                                break;
                            case "5":
                                dr.title = "SSC Marksheet";
                                break;



                        }


                        dr.id2 = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }

        /*************************************************Page 5 Regis **********************************************/

        public String Page_5_Registration(String fg, String regid)
        {

            string ch = "http://www.webmazy.com/?" + fg;


            Uri myUri = new Uri(ch);


            int MAXOTHER;

            String Prizes = HttpUtility.ParseQueryString(myUri.Query).Get("win");


            String NCC = HttpUtility.ParseQueryString(myUri.Query).Get("FirstName");

            String Social_ork = HttpUtility.ParseQueryString(myUri.Query).Get("MiddleName");

            String Computing = HttpUtility.ParseQueryString(myUri.Query).Get("LastName");


            String Sports = HttpUtility.ParseQueryString(myUri.Query).Get("FirstName1");

            String Elocution = HttpUtility.ParseQueryString(myUri.Query).Get("MiddleName1");

            String other = HttpUtility.ParseQueryString(myUri.Query).Get("LastName1");


            String Periodical = HttpUtility.ParseQueryString(myUri.Query).Get("FirstName2");

            String How_do = HttpUtility.ParseQueryString(myUri.Query).Get("MiddleName2");

            String Reason = HttpUtility.ParseQueryString(myUri.Query).Get("LastName2");




            String How_this = HttpUtility.ParseQueryString(myUri.Query).Get("LastName3");





            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                MAXOTHER = getmaxsother(cnn);
                myCommand.CommandText = "INSERT INTO `otherdetails` (`OTHERDETAILS_ID`, `STUDENT_ID`, `REGID_ID`, `AWARD`, `NCC`, `SOCIALWORK`, `COMPUTING`, `SPORTS`, `WRITING`, `OTHERS`, `PERODICAL`, `CURRENTAFF`, `PARTCOURSE`, `CARRER`, `DATE`) VALUES ('" + MAXOTHER + "', '" + regid + "', '" + regid + "', '" + Prizes + "', '" + NCC + "', '" + Social_ork + "', '" + Computing + "', '" + Sports + "', '" + Elocution + "', '" + other + "', '" + Periodical + "', '" + How_do + "', '" + Reason + "', '" + How_this + "', '" + mytime + "')";
                myCommand.ExecuteNonQuery();

                Updateattach(regid.Trim(), cnn);
                UpdateStatus(regid.Trim(), cnn, "5", myTrans);

                myTrans.Commit();

                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }



        public int getmaxsother(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(ATTACHEMENT_ID) from attachement";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }



        public void Updateattach(String regid, MySqlConnection cnn)
        {





            MySqlCommand myCommand = cnn.CreateCommand();





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                myCommand.CommandText = "Update attachement Set ISEDITED ='1' where REGID_ID =" + regid.Trim() + " ";
                myCommand.ExecuteNonQuery();



            }
            catch (Exception e)
            {


            }
            finally
            {

            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }



        /*************************************Select all other **********************************/






        public string Selectother(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM otherdetails  AS t  WHERE      t.REGID_ID = '" + Useridx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    System.Diagnostics.Debug.Write("count" + ds.Tables[0].Rows.Count + "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {




                        List<Otherdetails> er = new List<Otherdetails>();

                        Otherdetails dr = new Otherdetails();




                        dr.Prizes = ds.Tables[0].Rows[0][3].ToString();


                        dr.NCC = ds.Tables[0].Rows[0][4].ToString();

                        dr.Social_ork = ds.Tables[0].Rows[0][5].ToString();

                        dr.Computing = ds.Tables[0].Rows[0][6].ToString();


                        dr.Sports = ds.Tables[0].Rows[0][7].ToString();

                        dr.Elocution = ds.Tables[0].Rows[0][8].ToString();

                        dr.other = ds.Tables[0].Rows[0][9].ToString();


                        dr.Periodical = ds.Tables[0].Rows[0][10].ToString();

                        dr.How_do = ds.Tables[0].Rows[0][11].ToString();

                        dr.Reason = ds.Tables[0].Rows[0][12].ToString();




                        dr.How_this = ds.Tables[0].Rows[0][10].ToString();








                        String d = getstatus(Useridx, cnn);
                        HttpContext.Current.Session["st"] = d;

                        dr.status = d;
                        System.Diagnostics.Debug.Write("ety" + d + "");

                        er.Add(dr);

                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                        cnn.Close();
                        return datan;


                    }


                    else
                    {


                        return "sd";
                    }


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }



        public void sendemail(String id, String paass, String txtemailid)
        {
            try
            {

                string maill = "it@one9.online";

                //string maill = "support@iamevent.in";
                string pas = "";

                

                MailMessage mail = new MailMessage("it@one9.online", txtemailid);
                //mail.To.Add("pawank467@gmail.com");
                // mail.To.Add("iamevent16@gmail.com");
                mail.IsBodyHtml = false;
                mail.Subject = " Registration information";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;//587
                smtp.Host = "relay-hosting.secureserver.net";
                // smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = false;
                mail.Body = "Your Record Submitted Successfully your id as : " + id + " \n and  Password is:" + paass + "  \n       Thank you.";

                NetworkCredential NetworkCred = new NetworkCredential(maill, pas);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Send(mail);
                //g++;
            }
            catch (Exception fgk)
            {
                //Response.Write("<script language=\"javascript\"> alert('Network Error ')</script>");

                //   fg=  fgk.ToString();
            }
        }






        /******************************************Get User Status************************************************/



        public string getstatusreport(String passcode, string regid)
        {






            if (passcode != String.Empty && regid != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = regid.Trim();


                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  studentrecord.REGID, studentrecord.First_Name , studentrecord.MIDDLE_Name, studentrecord.Last_Name,  studentrecord.PAYMENTST, studentrecord.FINALSUBMIT  ,tblprofilephoto.Location_Data, criteriadetails.ADMINCRETAREA FROM tblprofilephoto INNER JOIN studentrecord ON tblprofilephoto.Photo_ID=studentrecord.PHOTO   INNER JOIN criteriadetails ON studentrecord.REGID=criteriadetails.REGID     WHERE studentrecord.REGID='" + Useridx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    System.Diagnostics.Debug.Write("count" + ds.Tables[0].Rows.Count + "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {




                        List<StatusCreteria> er = new List<StatusCreteria>();

                        StatusCreteria dr = new StatusCreteria();




                        dr.usernameappno = ds.Tables[0].Rows[0][0].ToString();

                        String name = "" + ds.Tables[0].Rows[0][1].ToString() + " " + ds.Tables[0].Rows[0][2].ToString() + " " + ds.Tables[0].Rows[0][3].ToString() + "";



                        dr.username = name;
                        string TargetLocation = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                        dr.imgsrc = MapURL(TargetLocation) + ds.Tables[0].Rows[0][6].ToString();



                        dr.paymentstatus = ds.Tables[0].Rows[0][4].ToString();
                        dr.faculty = "NA";
                        dr.program = ds.Tables[0].Rows[0][7].ToString();


                        dr.appstatus = ds.Tables[0].Rows[0][5].ToString();
                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                        cnn.Close();
                        return datan;


                    }


                    else
                    {


                        return "sd";
                    }


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }






        /************************************************Admin Login ***********************************************/


        public string Admin_Login(String passcode, string Userid, string password)
        {

            if (passcode != String.Empty && Userid != String.Empty && password != String.Empty)
            {



                string Useridx, passwordx;
                Useridx = Userid.Trim();
                passwordx = Encryptx(password.Trim());

                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM adminrecord  AS t  WHERE      t.REGID = '" + Useridx.Trim().ToString() + "' AND t.PASS='" + passwordx.Trim().ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ju = ds.Tables[0].Rows[0][5].ToString();
                        mk = ds.Tables[0].Rows[0][25].ToString();


                        Class1 yt = new Class1();

                        yt.UpdateLoginDate2(ju);

                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session["TYPEX_UOXX_mm"] = mk;
                        HttpContext.Current.Session["TYPEX_UOKKXX_gg"] = ju;


                        HttpContext.Current.Session["PRTU_UOkT_vv"] = "WATER_HUB@56_834599T_RRR";

                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = mk;
                        dr.regid = ju;
                        dr.status = "success";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        cnn.Close();
                        return datan;


                    }
                    else
                    {
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "na";
                        dr.status = "error";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);

                        return datan;
                    }
                }
                catch
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = "errorx";



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }



        public void UpdateLoginDate2(String adminId)
        {
            MySqlConnection cnn;
            cnn = conn();

            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytimes = time.ToString(format);

            string cmdString = "Update adminrecord  Set LastLogin_Date ='" + mytimes + "' where REGID =" + adminId.Trim() + " ";
            MySqlCommand cmd = new MySqlCommand(cmdString, cnn);


            cnn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }

        }










        /***************************************************************************************************************/



        /**************************************Create Program***********************************************/

        public String createprogram(String programname, String session, String maxsubject)
        {



            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getmaprogramid(cnn);
                myCommand.CommandText = "INSERT INTO `programdetails`( `PROGRAM_ID` , `SESSION`, `MAX_SUBJECT`,`PROGRAM_NAME` ,`COLLEGE`,`SECTION`, `STATUS`, `DATE`) VALUES ('" + CREID + "', '" + session + "', '" + maxsubject + "', '" + programname + "', '', '', '', '" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getmaprogramid(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(PROGRAM_ID) from programdetails";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }







        /************************Get All Program Name *********************/

        public string getallprogram(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM programdetails";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<programdetalis> er = new List<programdetalis>();
                    while (rdrt.Read())
                    {


                        programdetalis dr = new programdetalis();


                        dr.programname = rdrt[3].ToString();

                        dr.session = rdrt[1].ToString();

                        dr.maxsubject = rdrt[2].ToString();

                        dr.id = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }



        /*****************************************************************/
        /**********************GetAllSubject***********************/


        public string getallsubject(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM subjecttable";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<programdetalis> er = new List<programdetalis>();
                    while (rdrt.Read())
                    {


                        programdetalis dr = new programdetalis();


                        dr.programname = rdrt[2].ToString();

                        dr.session = rdrt[6].ToString();

                        dr.maxsubject = rdrt[7].ToString();

                        dr.id = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }







        /********************************************************************/
        /************************************Subject Setup************************************/




        public String subjectsetup(String subjectname, String fullmarks, String passmarks)
        {



            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getsubjectsetup(cnn);
                myCommand.CommandText = "INSERT INTO `subjecttable`(`SUBJECT_ID` , `SESSION`,`SUBJECTNAME`, `SECTION`, `STATUS`, `DATE`,`FULLMARKS`,`PASSMARKS`) VALUES ('" + CREID + "', '" + mytime + "', '" + subjectname + "', '', '', '" + mytime + "', '" + fullmarks + "', '" + passmarks + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getsubjectsetup(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(SUBJECT_ID) from subjecttable";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }













        /******************************************Delete Program********************************************************/



        public String deleteprogram(String OR_CODE)


        {


            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            try
            {


                myCommand.CommandText = "DELETE FROM `registrations`.`programdetails` WHERE  `PROGRAM_ID`='" + OR_CODE + "';";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }


        }



























        /********************************************************Insert lang************************************************/


        public String deleteprogramr(String subjectid, String programid)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getlangid(cnn);
                myCommand.CommandText = "INSERT INTO `program_language_details` (`LANGUAGE_ID`,`SESSION`, `NAME`, `PROGRAM_ID`, `SECTION`, `STATUS`, `DATE`, `FULLMARKS`, `PASSMARKS`) VALUES ('"+CREID+"', '', '"+subjectid+"', '"+programid+"', '', '', '"+mytime+"', '','')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getlangid(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(LANGUAGE_ID) from program_language_details";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }







        /********************************end status ******************************************************/




        public String deleteprogramrm(String subjectid, String programid)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getlangidv(cnn);
                myCommand.CommandText = "INSERT INTO `program_hpaper_details` (`PAPER_ID`,`SESSION`, `NAME`, `PROGRAM_ID`, `SECTION`, `STATUS`, `DATE`, `FULLMARKS`, `PASSMARKS`) VALUES ('" + CREID + "', '', '" + subjectid + "', '" + programid + "', '', '', '" + mytime + "', '','')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getlangidv(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(PAPER_ID) from program_hpaper_details";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }








        /********************************Add hons 1*****************************************/


        public String deleteprogramrn(String subjectid, String programid)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getlangidv1(cnn);
                myCommand.CommandText = "INSERT INTO `program_spaper_details` (`PAPER_ID`,`SESSION`, `NAME`, `PROGRAM_ID`, `SECTION`, `STATUS`, `DATE`, `FULLMARKS`, `PASSMARKS`) VALUES ('" + CREID + "', '', '" + subjectid + "', '" + programid + "', '', '', '" + mytime + "', '','')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return e.ToString();

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getlangidv1(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(PAPER_ID) from program_spaper_details";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }









        /**************************************/

        /***********************************************Add hons 2**************************************************/


        public String deleteprogramrt(String subjectid, String programid)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getlangidv2(cnn);
                myCommand.CommandText = "INSERT INTO `program_spaper_details2` (`PAPER_ID`,`SESSION`, `NAME`, `PROGRAM_ID`, `SECTION`, `STATUS`, `DATE`, `FULLMARKS`, `PASSMARKS`) VALUES ('" + CREID + "', '', '" + subjectid + "', '" + programid + "', '', '', '" + mytime + "', '','')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int getlangidv2(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(PAPER_ID) from program_spaper_details2";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /**************************************************/


        /*****************************Get All Langpaerbyid**********************/



        public string getlangsubjectbyprogramid(String passcode,String Type)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();
                    String cmdString = null;


                    if (Type.Equals("1"))
                        {

                        cmdString = "  SELECT  program_language_details.NAME,subjecttable.SUBJECTNAME FROM subjecttable INNER JOIN program_language_details ON subjecttable.SUBJECT_ID=program_language_details.NAME     WHERE program_language_details.PROGRAM_ID='" + passcode + "'";

                    }





                    if (Type.Equals("2"))
                        {

                        cmdString = "SELECT program_hpaper_details.NAME,subjecttable.SUBJECTNAME FROM subjecttable INNER JOIN program_hpaper_details ON subjecttable.SUBJECT_ID = program_hpaper_details.NAME     WHERE program_hpaper_details.PROGRAM_ID = '" + passcode + "'";
                    }

                    if (Type.Equals("3"))
                        {

                        cmdString = "SELECT program_spaper_details2.NAME,subjecttable.SUBJECTNAME FROM subjecttable INNER JOIN program_spaper_details2 ON subjecttable.SUBJECT_ID = program_spaper_details2.NAME     WHERE program_spaper_details2.PROGRAM_ID = '" + passcode + "'";

                    }

                    if (Type.Equals("4"))
                    {

                        cmdString = "SELECT program_spaper_details.NAME,subjecttable.SUBJECTNAME FROM subjecttable INNER JOIN program_spaper_details ON subjecttable.SUBJECT_ID = program_spaper_details.NAME     WHERE program_spaper_details.PROGRAM_ID = '" + passcode + "'";




                    }


                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<languageprg> er = new List<languageprg>();
                    while (rdrt.Read())
                    {


                        languageprg dr = new languageprg();


                        dr.LangCode = rdrt[0].ToString();

                        dr.Title = rdrt[1].ToString();

                       




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }




        /*************************************************************/



        /*********************************Create Caste (19/01/2019) *****************/

        public String createcaste(String castename)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getcastemaxid(cnn);
                myCommand.CommandText = "INSERT INTO `caste_tb` (`CASTE_ID`,`CASTE_NAME`, `STATUS`, `DATE`) VALUES ('" + CREID + "','" + castename+ "', 'na', '" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        /************************get id of caste**********************************************************/

        public int getcastemaxid(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(CASTE_ID) from caste_tb";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /****************************************************************************************************/

        /*************************************Fetch All Caste Record***********************************************************/


        public string getallcastedata(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM caste_tb";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<castelistall> er = new List<castelistall>();
                    while (rdrt.Read())
                    {


                        castelistall dr = new castelistall();


                        dr.programname = rdrt[1].ToString();

                        dr.session = rdrt[2].ToString();

                        dr.maxsubject = rdrt[3].ToString();

                        dr.id = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /**************************************************************************************************************************/



        /************************************Add Caste & Program with id  Start Here   **********************************/



        public String setcastewithdb(String subjectid, String programid)


        {
           int CREID;
            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = seletmaxidofcastedb(cnn);
                myCommand.CommandText = "INSERT INTO  caste_table_with_admission_session(`CS_ID`,`CSS_ID`,`STATUS`,`PROGRAM_ID`,`DATE`) VALUES ('" + CREID+"','"+subjectid+"','na','"+programid+"','"+mytime+"')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return e.ToString();

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int seletmaxidofcastedb(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(CS_ID) from caste_table_with_admission_session";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }






        /**********************************************************************************************************************/








        /**********************************************Create Blood Group ***************************************************************************/




        /*********************************Create Caste (19/01/2019) *****************/

        public String createbloddgroup(String castename)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getcastemaxidbloodgroup(cnn);
                myCommand.CommandText = "INSERT INTO `blood_group_table` (`BLOOD_GROUP_ID`,`BLOOD_GROUP_NAME`, `STATUS`, `DATE`) VALUES ('" + CREID + "','" + castename + "', 'na', '" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        /************************get id of caste**********************************************************/

        public int getcastemaxidbloodgroup(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(BLOOD_GROUP_ID) from blood_group_table";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /****************************************************************************************************/

        /*************************************Fetch All Caste Record***********************************************************/


        public string getallbloodgroupdata(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM blood_group_table";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<castelistall> er = new List<castelistall>();
                    while (rdrt.Read())
                    {


                        castelistall dr = new castelistall();


                        dr.programname = rdrt[1].ToString();

                        dr.session = rdrt[2].ToString();

                        dr.maxsubject = rdrt[3].ToString();

                        dr.id = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /**************************************************************************************************************************/



        /************************************Add Caste & Program with id  Start Here   **********************************/



        public String setbloodgroupwithdb(String subjectid, String programid)


        {
            int CREID;
            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = seletmaxidofbloodgroupdb(cnn);
                myCommand.CommandText = "INSERT INTO  blood_group_table_with_program(`BLO_GROUP_ID`,`BLOOD_GROUPID`,`STATUS`,`PROGRAM_ID`,`DATE`) VALUES ('" + CREID + "','" + subjectid + "','na','" + programid + "','" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return e.ToString();

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int seletmaxidofbloodgroupdb(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(BLO_GROUP_ID) from blood_group_table_with_program";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }






        /**********************************************************************************************************************/




        /**************************************************************************************************************************************************/



        /*************************************            Add Gender      ****************************************************************************************/




        public String creategender(String castename)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getcastemaxidgender(cnn);
                myCommand.CommandText = "INSERT INTO `gender_table` (`GENDER_ID`,`GENDER_NAME`, `STATUS`, `DATE`) VALUES ('" + CREID + "','" + castename + "', 'na', '" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        /************************get id of caste**********************************************************/

        public int getcastemaxidgender(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(GENDER_ID) from gender_table";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /****************************************************************************************************/

        /*************************************Fetch All Caste Record***********************************************************/


        public string getallgender(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM gender_table";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<castelistall> er = new List<castelistall>();
                    while (rdrt.Read())
                    {


                        castelistall dr = new castelistall();


                        dr.programname = rdrt[1].ToString();

                        dr.session = rdrt[2].ToString();

                        dr.maxsubject = rdrt[3].ToString();

                        dr.id = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /**************************************************************************************************************************/



        /************************************Add Caste & Program with id  Start Here   **********************************/



        public String setgenderwithdb(String subjectid, String programid)


        {
            int CREID;
            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = seletmaxidofgenderwithprogram(cnn);
                myCommand.CommandText = "INSERT INTO  gender_table_with_admission_session(`GEN_ID`,`GENDR_ID`,`STATUS`,`PROGRAM_ID`,`DATE`) VALUES ('" + CREID + "','" + subjectid + "','na','" + programid + "','" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return e.ToString();

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int seletmaxidofgenderwithprogram(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(GEN_ID) from gender_table_with_admission_session";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }












        /*********************************************************************************************************************************************************/
        /****************************************************    Add disability*****************************************************************************/



        public String createdisability(String castename)


        {




            int CREID;








            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = getcastemaxiddisability(cnn);
                myCommand.CommandText = "INSERT INTO `disability` (`CS_ID`,`CSS_ID`, `STATUS`, `DATE`) VALUES ('" + CREID + "','" + castename + "', 'na', '" + mytime + "')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        /************************get id of caste**********************************************************/

        public int getcastemaxiddisability(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(CS_ID) from disability";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /****************************************************************************************************/

        /*************************************Fetch All Caste Record***********************************************************/


        public string getalldisability(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM disability";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<castelistall> er = new List<castelistall>();
                    while (rdrt.Read())
                    {


                        castelistall dr = new castelistall();


                        dr.programname = rdrt[1].ToString();

                        dr.session = rdrt[2].ToString();

                        dr.maxsubject = rdrt[3].ToString();

                        dr.id = rdrt[0].ToString();




                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /**************************************************************************************************************************/



        /************************************Add Caste & Program with id  Start Here   **********************************/



        public String setdisabilitywithdb(String subjectid, String programid)


        {
            int CREID;
            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);
            try
            {

                CREID = seletmaxidofdisabilitydb(cnn);
                myCommand.CommandText = "INSERT INTO  disabilitywith_session(CS_ID_ID,CSS_ID_D,STATUS,PROGRAM_ID,DATE) VALUES ('"+CREID + "','"+subjectid+"','na','"+programid+"','"+mytime+"')";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();





                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return e.ToString();

            }
            finally
            {
                cnn.Close();
            }













            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);















        }


        public int seletmaxidofdisabilitydb(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(CS_ID_ID) from disabilitywith_session";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }








        /******************************************************************************************************************************************************/


        /***********************************************Get All Gender by Program id************************************************************/




                    public string getallgenderbyprogramid(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT gender_table.GENDER_ID, gender_table.GENDER_NAME FROM gender_table INNER JOIN gender_table_with_admission_session ON gender_table_with_admission_session.GENDR_ID = gender_table.GENDER_ID WHERE gender_table_with_admission_session.PROGRAM_ID= "+passcode+"";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<genderdtl> er = new List<genderdtl>();
                    while (rdrt.Read())
                    {


                        genderdtl dr = new genderdtl();


                        dr.id = rdrt[0].ToString();

                        dr.name = rdrt[1].ToString();               

                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }


        /*****************************************Select Bloodgroup*****************************************************************************/


        public string getallbloodbyprogramid(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  blood_group_table.BLOOD_GROUP_ID, blood_group_table.BLOOD_GROUP_NAME FROM blood_group_table INNER JOIN blood_group_table_with_program ON blood_group_table_with_program.BLOOD_GROUPID = blood_group_table.BLOOD_GROUP_ID WHERE blood_group_table_with_program.PROGRAM_ID="+passcode+"";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<bloodgrpdtl> er = new List<bloodgrpdtl>();
                    while (rdrt.Read())
                    {


                        bloodgrpdtl dr = new bloodgrpdtl();


                        dr.id = rdrt[0].ToString();
                        dr.name = rdrt[1].ToString();



                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }





        /****************************************************************************************************************************************/

        /*********************************************Select Caste by programid*************************************************************************/



        public string getallcastebyprogramid(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  caste_tb.CASTE_ID, caste_tb.CASTE_NAME FROM caste_tb INNER JOIN caste_table_with_admission_session ON caste_table_with_admission_session.CSS_ID = caste_tb.CASTE_ID WHERE caste_table_with_admission_session.PROGRAM_ID="+passcode+"";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<castedtldtl> er = new List<castedtldtl>();
                    while (rdrt.Read())
                    {


                        castedtldtl dr = new castedtldtl();


                        dr.id = rdrt[0].ToString();
                        dr.name = rdrt[1].ToString();



                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }





        /********************************************************************************************************************************************************/

        /***********************************************************************************************************************************************************/

        public string getalldisabilityprogramid(String passcode)
        {






            if (passcode != String.Empty)
            {






                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  disability.CS_ID, disability.CSS_ID FROM disability INNER JOIN disabilitywith_session ON disabilitywith_session.CSS_ID_D = disability.CS_ID WHERE disabilitywith_session.PROGRAM_ID="+passcode+"";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();

                    MySqlDataReader rdrt = cmd.ExecuteReader();


                    int i = 1;
                    List<disability> er = new List<disability>();
                    while (rdrt.Read())
                    {


                        disability dr = new disability();


                        dr.id = rdrt[0].ToString();
                        dr.name = rdrt[1].ToString();



                        er.Add(dr);
                        i++;
                    }
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);
                    System.Diagnostics.Debug.Write("data" + datan.ToString() + "");
                    cnn.Close();
                    return datan;


                }
                catch (Exception ex)
                {



                    return ex.ToString();
                }



            }


            else
            {
                return "error";
            }






        }



        /*****************************************************************************************************************************************/




        /***************************************************Get All program basis of student previous dtl************************************************/





        /**************************************************************************************************************************************************/






        /***************************************************Save applied data*************************************************************************************/



        public String applieddata(String regid,String honssubject,String honsmarks,String collegenum,String totalm, String compsub, String nrbsub, String subsone, String subs2)


        {





            String totalmarks="", caste = "", gender = "", preboard = "", disability = "", regfee = "", paymentid = "", percentage = "", obtainmarks = "";



            try
            {


                string mk, nk, ju, typ;

                MySqlConnection cnnt;
                cnnt = conn();



                String cmdString = "SELECT studentrecord.GENDER, studentrecord.casterec, studentrecord.dis, criteriadetails.Last_qualification, criteriadetails.Total_Marks,criteriadetails.Obtain_Marks,criteriadetails.Percentage FROM criteriadetails INNER JOIN studentrecord ON criteriadetails.REGID = studentrecord.REGID   WHERE criteriadetails.REGID="+regid+"";
                MySqlCommand cmd = new MySqlCommand(cmdString, cnnt);
                cnnt.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cnnt.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gender = ds.Tables[0].Rows[0][0].ToString();
                    caste = ds.Tables[0].Rows[0][1].ToString();




                  

                    disability= ds.Tables[0].Rows[0][2].ToString();
                    preboard =  ds.Tables[0].Rows[0][3].ToString();
                    totalmarks =  ds.Tables[0].Rows[0][4].ToString();
                    obtainmarks = ds.Tables[0].Rows[0][5].ToString();
                    percentage = ds.Tables[0].Rows[0][6].ToString();


                }






                int idofst;








                MySqlConnection cnn;
                cnn = conn();


                cnn.Open();

                //  System.Diagnostics.Debug.Write(maxID);

                MySqlCommand myCommand = cnn.CreateCommand();
                MySqlTransaction myTrans;


                myTrans = cnn.BeginTransaction();

                myCommand.Connection = cnn;
                myCommand.Transaction = myTrans;





                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string format = "yyyy-MM-dd HH:mm:ss ";
                var mytime = time.ToString(format);
                try
                {
                    

                    idofst = getcastemaxiddisabilityt(cnn);
                    myCommand.CommandText = "INSERT INTO `studentrecord_applied`(`applied_id`,`hons_subject`,`hons_marks`,`total_marks`,`caste`,`regid`,`gender`,`preboard`,`disability`,`regfee`,`paymentid`,`paymentstatus`,`percentage`,`date`,`clgname`,`honstotal`,`cssubject`,`nrbsubject`,`subsone`,`substwo`,`obtainmarksall`,`issortlisted`) VALUES ('" + idofst+"','"+honssubject+"','"+honsmarks+"','"+totalmarks+"','"+caste+"','"+regid+"','"+gender+"','"+preboard+"','"+disability+"','1000','na','na','"+percentage+"','"+mytime+ "','" + collegenum + "','" +totalm+ "','" +compsub+ "','" +nrbsub+ "','" +subsone+ "','"+subs2+ "','" +obtainmarks+ "','OSM')";
                    myCommand.ExecuteNonQuery();
                    UpdateStatus(regid.Trim(), cnn, "4", myTrans);
                    myTrans.Commit();





                    return "success";







                }
                catch (Exception e)
                {
                    try
                    {


                        myTrans.Rollback();
                    }
                    catch (SqlException ex)
                    {
                        if (myTrans.Connection != null)
                        {
                            return "error";

                            //st = "error";
                        }
                    }

                    //   st = "error";
                    System.Diagnostics.Debug.Write(e.ToString());
                    // return e.ToString();

                    return e.ToString();

                }
                finally
                {
                    cnn.Close();
                }













                //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
                // System.Diagnostics.Debug.Write(fgr);





            }
            catch (Exception ex)
            {
                return "errr";


              //  return ex.ToString();
            }



















        }






        /***********************************************************************************************************************************************************/


        /*****************************Get max id of applyid******************************************/




        public int getcastemaxiddisabilityt(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(applied_id) from studentrecord_applied";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /******************************Select * shortlisted candidates****************************************************************/

        public string selectallshort(String passcode, string regid)
        {

            if (passcode != String.Empty && regid != String.Empty)
            {



                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DataTable fgt = new DataTable();
                    da.Fill(fgt);
                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                    int dft = fgt.Rows.Count;
                    for (int ik = 0; ik < dft; ik++)
                    {





                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());
                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                        dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());
                        dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString()=="df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "");


                        double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString()=="df"?"0": ""+ds.Tables[0].Rows[ik][15].ToString()+""));

                        fg = fg * 100;


                        dr.honsper = Convert.ToDouble(fg.ToString("00.00"));



                        dr.caste = ds.Tables[0].Rows[ik][21].ToString();
                        dr.gender = ds.Tables[0].Rows[ik][22].ToString();
                        dr.disability = ds.Tables[0].Rows[ik][23].ToString();
                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                        er.Add(dr);

                    }
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        //System.Diagnostics.Debug.Write(datan);

                        cnn.Close();
                        return datan;


                   
                }
                catch(Exception ex)
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = ex.ToString();



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }





        /****************************************************************************************************************************/





        /********************************Get Meritlist*********************************************************************/

        public string selectallshortmeritlist(String passcode, string regid,String honstype,String category)
        {

            if (passcode != String.Empty && regid != String.Empty)
            {



                switch(honstype.ToUpper())
                {

                    case "BOT":


                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Botany' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Botany' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Botany' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Botany' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Botany' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Botany' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Botany' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;

                    case "CHE":

                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Chemistry' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Chemistry' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Chemistry' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Chemistry' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Chemistry' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Chemistry' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Chemistry' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }


                        break;

                    case "ECO":




                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Economics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Economics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Economics' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Economics' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Economics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Economics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Economics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;

                    case "ENG":



                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'English' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'English' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'English' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='English' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'English' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'English' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'English' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;

                    case "HIN":


                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Hindi' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Hindi' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Hindi' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Hindi' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Hindi' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Hindi' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Hindi' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;

                    case "HISTORY":

                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'History' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

//                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'History' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                           dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

//                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {
                                    



                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'History' AND issortlisted='OSM'";

                                   // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='History' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                           dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq ;
                                        dr.passinyy= Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

//                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'History' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

//                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'History' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

//                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'History' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

//                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }


                        break;

                    case "MATH":


                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Math' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Math' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Math' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Math' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Math' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Math' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Math' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;


                    case "PHY":


                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Physics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Physics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Physics' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Physics' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Physics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Physics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Physics' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;

                    case "POLSCIENCE":


                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Polscience' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Polscience' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Polscience' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Polscience' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Polscience' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Polscience' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Polscience' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;

                    case "PSY":



                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Psychology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Psychology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Psychology' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Psychology' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Psychology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Psychology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Psychology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }
                        break;

                    case "ZOO":

                        switch (category.ToUpper())
                        {
                            case "EBC":

                                try
                                {
                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();



                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Zoology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'EBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;



                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();




                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "FEMALE":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();










                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Zoology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'FEMALE'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;
                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;






                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();




                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;
                            case "GENERAL":

                                try
                                {




                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Zoology' AND issortlisted='OSM'";

                                    // String cmdString = "SELECT  studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID  FROM studentrecord_applied  inner join  caste_tb on  studentrecord_applied.caste=caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender=gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability=disability.CS_ID where studentrecord_applied.hons_subject='Zoology' AND   issortlisted IS  NULL ";
                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;


                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;







                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "OBC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();

















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Zoology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'OBC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "SC":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Zoology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'SC'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";






                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                            case "ST":

                                try
                                {


















                                    string mk, nk, ju, typ;

                                    MySqlConnection cnn;
                                    cnn = conn();
















                                    String cmdString = "SELECT studentrecord_applied.*  ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID ,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.DOB,criteriadetails.Last_qualification,criteriadetails.passingyear FROM studentrecord_applied inner join criteriadetails on studentrecord_applied.regid = criteriadetails.REGID inner join studentrecord on  studentrecord_applied.regid = studentrecord.REGID join caste_tb on studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID where studentrecord_applied.hons_subject = 'Zoology' AND issortlisted='OSM' AND upper(caste_tb.CASTE_NAME) = 'ST'";





                                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                                    cnn.Open();
                                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    DataTable fgt = new DataTable();
                                    da.Fill(fgt);
                                    List<shortlistedcandidatesrecord> er = new List<shortlistedcandidatesrecord>();


                                    int dft = fgt.Rows.Count;
                                    for (int ik = 0; ik < dft; ik++)
                                    {





                                        shortlistedcandidatesrecord dr = new shortlistedcandidatesrecord();





                                        dr.srno = ik + 1;

                                        String name = "" + ds.Tables[0].Rows[ik][36].ToString() + " " + ds.Tables[0].Rows[ik][37].ToString() + " " + ds.Tables[0].Rows[ik][38].ToString() + "";
                                        String fname = "" + ds.Tables[0].Rows[ik][39].ToString() + "";
                                        String mother = "" + ds.Tables[0].Rows[ik][40].ToString() + "";
                                        String Lastq = "" + ds.Tables[0].Rows[ik][42].ToString() + "";
                                        String Passingy = "" + ds.Tables[0].Rows[ik][43].ToString() + "";


                                        dr.names = name + " / " + ds.Tables[0].Rows[ik][41].ToString() + "";
                                        dr.fname = fname;
                                        dr.mname = mother;
                                        dr.lastqq = Lastq;
                                        dr.passinyy = Passingy;

                                        dr.regid = ds.Tables[0].Rows[ik][5].ToString();
                                        dr.honss = ds.Tables[0].Rows[ik][1].ToString();
                                        dr.totalobt = Convert.ToDouble(ds.Tables[0].Rows[ik][20]);
                                        dr.fullmark = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                        dr.percentage = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());


                                        if (ds.Tables[0].Rows[ik][15].ToString() == "df")
                                        {

                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString());

                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][3].ToString());
                                            // double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][20].ToString()));
                                            dr.honsper = Convert.ToDouble(ds.Tables[0].Rows[ik][12].ToString());
                                        }
                                        else
                                        {
                                            dr.honsmarks = Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString());


                                            dr.honstotal = Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString());
                                            double fg = (Convert.ToDouble(ds.Tables[0].Rows[ik][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[ik][15].ToString() == "df" ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + ""));

                                            fg = fg * 100;

                                            dr.honsper = Convert.ToDouble(fg.ToString("00.00"));
                                        }





                                        //  dr.honstotal = ds.Tables[0].Rows[ik][15].ToString() == "df"






                                        //  ? "0" : "" + ds.Tables[0].Rows[ik][15].ToString() + "";





                                        dr.caste = ds.Tables[0].Rows[ik][33].ToString();
                                        dr.gender = ds.Tables[0].Rows[ik][34].ToString();
                                        dr.disability = ds.Tables[0].Rows[ik][35].ToString();


                                        dr.id = ds.Tables[0].Rows[ik][0].ToString();









                                        er.Add(dr);

                                    }
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);
                                    //  er.Sort()
                                    //er.OrderBy(c => c.totalobt).ToList();

                                    //                                    er.Sort((x, y) => x.totalobt.CompareTo(y.totalobt));
                                    // List < shortlistedcandidatesrecord> ft=  er.OrderByDescending(h => h.totalobt).ToList();
                                    String datan = JsonConvert.SerializeObject(er, serializer);
                                    //System.Diagnostics.Debug.Write(datan);

                                    cnn.Close();
                                    return datan;



                                }
                                catch (Exception ex)
                                {
                                    List<Loginrecord> er = new List<Loginrecord>();

                                    Loginrecord dr = new Loginrecord();


                                    dr.processcomp = "na";
                                    dr.regid = "na";
                                    dr.status = ex.ToString();



                                    er.Add(dr);
                                    var serializer = new JsonSerializerSettings();
                                    var ghk = new DefaultContractResolver();
                                    ghk.IgnoreSerializableAttribute = true;

                                    serializer.ContractResolver = ghk;

                                    // data = JsonConvert.SerializeObject(strForm);



                                    String datan = JsonConvert.SerializeObject(er, serializer);

                                    return datan;
                                }

                                break;

                        }

                        break;


                }





















            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }



            return null;
        }




        /**********************************************************************************************/











        /**************************Get All Data************************************************************/



        public string getdetailsbyid(String passcode, string regid)
        {

            if (passcode != String.Empty && regid != String.Empty)
            {



                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();
                    String cmdString = "select criteriadetails.Last_qualification,criteriadetails.Total_Marks,criteriadetails.Obtain_Marks,criteriadetails.Board_University_Name, criteriadetails.passingyear,studentrecord.DOB,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.GENDER,studentrecord.BLOODGROUP,studentrecord.MT,studentrecord.NATION,studentrecord.casterec,studentrecord.dis,studentrecord.EMAIL,studentrecord.mobilenum,studentrecord_applied.hons_subject,studentrecord_applied.subsone,studentrecord_applied.substwo ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID,tblprofilephoto.Location_Data , blood_group_table.BLOOD_GROUP_NAME ,postaldetails.CURRENTADDRESS,postaldetails.COUNTRY,postaldetails.STATE,postaldetails.CITY,postaldetails.ZIPCODE ,studentrecord_applied.regfeetransactionid,studentrecord_applied.admissionfeepaidid from studentrecord inner join criteriadetails on criteriadetails.REGID = studentrecord.REGID inner join studentrecord_applied on studentrecord_applied.regid = studentrecord.REGID    inner join  caste_tb on  studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID  INNER JOIN tblprofilephoto ON tblprofilephoto.Photo_ID = studentrecord.PHOTO  inner join blood_group_table on studentrecord.BLOODGROUP = blood_group_table.BLOOD_GROUP_ID   inner join  postaldetails on studentrecord_applied.regid = postaldetails.REGID   where studentrecord.REGID ='" + regid+"' ";

                    //String cmdString = "select criteriadetails.Last_qualification,criteriadetails.Total_Marks,criteriadetails.Obtain_Marks,criteriadetails.Board_University_Name, criteriadetails.passingyear,studentrecord.DOB,studentrecord.First_Name,studentrecord.MIDDLE_Name,studentrecord.Last_Name,studentrecord.fathername,studentrecord.MOTHERN,studentrecord.GENDER,studentrecord.BLOODGROUP,studentrecord.MT,studentrecord.NATION,studentrecord.casterec,studentrecord.dis,studentrecord.EMAIL,studentrecord.mobilenum,studentrecord_applied.hons_subject,studentrecord_applied.subsone,studentrecord_applied.substwo ,caste_tb.CASTE_NAME,gender_table.GENDER_NAME,disability.CSS_ID,tblprofilephoto.Location_Data from studentrecord inner join criteriadetails on criteriadetails.REGID = studentrecord.REGID inner join studentrecord_applied on studentrecord_applied.regid = studentrecord.REGID    inner join  caste_tb on  studentrecord_applied.caste = caste_tb.CASTE_ID  inner join gender_table on  studentrecord_applied.gender = gender_table.GENDER_ID   inner join disability on  studentrecord_applied.disability = disability.CS_ID  INNER JOIN tblprofilephoto ON tblprofilephoto.Photo_ID = studentrecord.PHOTO  where studentrecord.REGID = '"+regid+"'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {




                        List<shortlistedcandidatesrecord_all> er = new List<shortlistedcandidatesrecord_all>();

                        shortlistedcandidatesrecord_all dr = new shortlistedcandidatesrecord_all();





                        dr.lastqualification = ds.Tables[0].Rows[0][0].ToString();
                        dr.yearofpassing = ds.Tables[0].Rows[0][4].ToString();
                        dr.dobdata= ds.Tables[0].Rows[0][5].ToString();

                        String name = "" + ds.Tables[0].Rows[0][6].ToString() + " " + ds.Tables[0].Rows[0][7].ToString() + " " + ds.Tables[0].Rows[0][8].ToString() + "";
                        dr.name = name;
                        dr.fathername = ds.Tables[0].Rows[0][9].ToString();
                        dr.mothername = ds.Tables[0].Rows[0][10].ToString();
                        dr.honsmarks = ds.Tables[0].Rows[0][2].ToString();
                        dr.honstotal = ds.Tables[0].Rows[0][15].ToString();


                      /*  double fg = (Convert.ToDouble(ds.Tables[0].Rows[0][2].ToString()) / Convert.ToDouble(ds.Tables[0].Rows[0][15].ToString()));
                        dr.honsper = fg.ToString("0.00%");*/



                        dr.lastqualification = ds.Tables[0].Rows[0][0].ToString();
                        dr.totalmarks = ds.Tables[0].Rows[0][1].ToString();
                        dr.marksobtained = ds.Tables[0].Rows[0][2].ToString();
                        dr.gender = ds.Tables[0].Rows[0][23].ToString();

                        dr.mothertounge = ds.Tables[0].Rows[0][13].ToString();


                        dr.nationality = ds.Tables[0].Rows[0][14].ToString();

                        dr.religion = "N/A";
                        dr.bloodgroup = ds.Tables[0].Rows[0][0].ToString();


                        dr.mobile = ds.Tables[0].Rows[0][18].ToString();

                        dr.email = ds.Tables[0].Rows[0][17].ToString();

                        dr.honsubject = ds.Tables[0].Rows[0][19].ToString();
                        dr.subone = ds.Tables[0].Rows[0][20].ToString();
                        dr.subtwo = ds.Tables[0].Rows[0][21].ToString();
                        dr.disability = ds.Tables[0].Rows[0][24].ToString();
                        dr.caste = ds.Tables[0].Rows[0][22].ToString();



                        string TargetLocation = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                        dr.imgsrc = MapURL(TargetLocation) + ds.Tables[0].Rows[0][25].ToString();

                        dr.boardorunv= ds.Tables[0].Rows[0][3].ToString();

                        dr.bloodgroupp = ds.Tables[0].Rows[0][26].ToString();
                        dr.curruntaddress = ds.Tables[0].Rows[0][27].ToString();
                        dr.country = ds.Tables[0].Rows[0][28].ToString();
                        dr.state = ds.Tables[0].Rows[0][29].ToString();
                        dr.city = ds.Tables[0].Rows[0][30].ToString();
                        dr.pin = ds.Tables[0].Rows[0][31].ToString();
                        dr.district = ds.Tables[0].Rows[0][30].ToString();

                        dr.regfeetr = ds.Tables[0].Rows[0][32].ToString();
                        dr.admissionfeetr = ds.Tables[0].Rows[0][33].ToString();


                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        //System.Diagnostics.Debug.Write(datan);

                        cnn.Close();
                        return datan;


                    }
                    else
                    {
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "na";
                        dr.status = "error";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);

                        cnn.Close();

                        String datan = JsonConvert.SerializeObject(er, serializer);

                        return datan;
                    }
                }
                catch (Exception ex)
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = ex.ToString();



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }




        /********************************************************************************************************/



        /***********************************************************************************************/




        /***********************************************************Add Verify student *******************************************************/




        public String setmarks(String OR_CODE,String mess)


        {


            MySqlConnection cnn;
            cnn = conn();


            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();

            MySqlCommand myCommand2 = cnn.CreateCommand();


            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;

            myCommand2.Connection = cnn;
            myCommand2.Transaction = myTrans;




            try
            {


                myCommand.CommandText = "UPDATE studentrecord_applied set issortlisted='YES'  where regid='" + OR_CODE.Trim() + "'";
                myCommand.ExecuteNonQuery();



               // myCommand2.CommandText = "select EMAIL from studentrecord   where REGID='" + OR_CODE.Trim() + "'";
             //   String txtemailid = Convert.ToString(myCommand2.ExecuteScalar());


              //  sendemailforshortlist(mess, txtemailid);

                return "success";

            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                // return e.ToString();

                return "error";

            }
            finally
            {
                cnn.Close();
            }


        }




        /******************************Send message for shortlist & make payment id*********************************************/

        public void sendemailforshortlist(String message, String txtemailid)
        {
            try
            {

                string maill = "it@one9.online";

                //string maill = "support@iamevent.in";
                string pas = "";

               
                MailMessage mail = new MailMessage("it@one9.online", txtemailid);
                //mail.To.Add("pawank467@gmail.com");
                // mail.To.Add("iamevent16@gmail.com");
                mail.IsBodyHtml = false;
                mail.Subject = " Registration information";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;//587
                smtp.Host = "relay-hosting.secureserver.net";
                // smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = false;
                mail.Body = "You are shortlisted      Thank you.";

                NetworkCredential NetworkCred = new NetworkCredential(maill, pas);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Send(mail);
                //g++;
            }
            catch (Exception fgk)
            {
                //Response.Write("<script language=\"javascript\"> alert('Network Error ')</script>");

                //   fg=  fgk.ToString();
            }
        }







        /*****************************************************************************************************************************/







        /***********************************************************************************************************************************/

        /**************************************Check user is or not in shortlisted data*********************************************/

        public string issortlistedin(String passcode, string Userid)
        {

            if (passcode != String.Empty && Userid != String.Empty)
            {



               // string Useridx, passwordx;
               // Useridx = Userid.Trim();
               // passwordx = Encryptx(password.Trim());

                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "SELECT  * FROM studentrecord_applied  AS t  WHERE      t.regid = '"+Userid.Trim().ToString()+"'  and t.issortlisted='YES'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                      //  ju = ds.Tables[0].Rows[0][5].ToString();
                       // mk = ds.Tables[0].Rows[0][23].ToString();


                      
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "studentverifydtl?sid="+Encryptx(Userid)+"";
                        dr.status = "success";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);
                        cnn.Close();
                        return datan;


                    }
                    else
                    {
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "na";
                        dr.status = "error";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);

                        return datan;
                    }
                }
                catch
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = "errorx";



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }



        /*****************************************************************************************************************************/






         /****************Generate QR Code *****************************************************************/

        public String  createqr(String content,int widthk,int heightk, int margink)
        {
            var width = widthk; // width of the Qr Code

            var height = heightk; // height of the Qr Code

            var margin = margink;

            var qrCodeWriter = new ZXing.BarcodeWriterPixelData

            {

                Format = ZXing.BarcodeFormat.QR_CODE,

                Options = new QrCodeEncodingOptions { Height = height, Width = width, Margin = margin }

            };

            var pixelData = qrCodeWriter.Write(content);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference

            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB

            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))

            using (var ms = new MemoryStream())

            {

                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),

                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                try

                {

                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image

                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,

                    pixelData.Pixels.Length);

                }

                finally

                {

                    bitmap.UnlockBits(bitmapData);

                }

                // save to stream as PNG

                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                String Src = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
                return Src;

            }
        }


        /****************************************Select Student Applied status status  ********************************************************************/



        public string appliddatabyid(String passcode, string Userid)
        {

            if (passcode != String.Empty && Userid != String.Empty)
            {



                // string Useridx, passwordx;
                // Useridx = Userid.Trim();
                // passwordx = Encryptx(password.Trim());

                try
                {


                    string mk, nk, ju, typ;

                    MySqlConnection cnn;
                    cnn = conn();



                    String cmdString = "select issortlisted ,isregfeepaid,isadmissionfeepaid,veryfiedforadmission from studentrecord_applied where regid='" + Userid.Trim().ToString()+"' ";
                    MySqlCommand cmd = new MySqlCommand(cmdString, cnn);
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //  ju = ds.Tables[0].Rows[0][5].ToString();
                        // mk = ds.Tables[0].Rows[0][23].ToString();



                        List<studentapplieddata> er = new List<studentapplieddata>();

                        studentapplieddata dr = new studentapplieddata();
                        dr.issort = ds.Tables[0].Rows[0][0].ToString().Trim()=="OSM"?"NOT":""+ ds.Tables[0].Rows[0][0].ToString()+"";
                        dr.isregfeepaidd = ds.Tables[0].Rows[0][1].ToString() == "" ? "NOT" : "" + ds.Tables[0].Rows[0][1].ToString() + "";
                        dr.isadmissionfeepaidd = ds.Tables[0].Rows[0][2].ToString() == ""? "NOT" : "" + ds.Tables[0].Rows[0][2].ToString() + "";
                        dr.isverify = ds.Tables[0].Rows[0][3].ToString() == "" ? "NOT" : "" + ds.Tables[0].Rows[0][3].ToString() + "";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;
                        serializer.ContractResolver = ghk;
                        // data = JsonConvert.SerializeObject(strForm);


                        String datan = JsonConvert.SerializeObject(er, serializer);
                        cnn.Close();
                        return datan;


                    }
                    else
                    {
                        List<Loginrecord> er = new List<Loginrecord>();

                        Loginrecord dr = new Loginrecord();


                        dr.processcomp = "na";
                        dr.regid = "na";
                        dr.status = "error";



                        er.Add(dr);
                        var serializer = new JsonSerializerSettings();
                        var ghk = new DefaultContractResolver();
                        ghk.IgnoreSerializableAttribute = true;

                        serializer.ContractResolver = ghk;

                        // data = JsonConvert.SerializeObject(strForm);



                        String datan = JsonConvert.SerializeObject(er, serializer);

                        return datan;
                    }
                }
                catch
                {
                    List<Loginrecord> er = new List<Loginrecord>();

                    Loginrecord dr = new Loginrecord();


                    dr.processcomp = "na";
                    dr.regid = "na";
                    dr.status = "errorx";



                    er.Add(dr);
                    var serializer = new JsonSerializerSettings();
                    var ghk = new DefaultContractResolver();
                    ghk.IgnoreSerializableAttribute = true;

                    serializer.ContractResolver = ghk;

                    // data = JsonConvert.SerializeObject(strForm);



                    String datan = JsonConvert.SerializeObject(er, serializer);

                    return datan;
                }



            }


            else
            {
                List<Loginrecord> er = new List<Loginrecord>();

                Loginrecord dr = new Loginrecord();


                dr.processcomp = "na";
                dr.regid = "na";
                dr.status = "errorx";



                er.Add(dr);
                var serializer = new JsonSerializerSettings();
                var ghk = new DefaultContractResolver();
                ghk.IgnoreSerializableAttribute = true;

                serializer.ContractResolver = ghk;

                // data = JsonConvert.SerializeObject(strForm);



                String datan = JsonConvert.SerializeObject(er, serializer);

                return datan;
            }
        }





        /***************************************************************************************************************************************************/












        /****************************************************************************************************************/





        /******************************Insert transaction table *************************************************************/

        /****************************Insert Transaction Table **************************************************************************/
        public String inserttransactiontable(string studentid,  String pricee)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;


            int idm = getmaxtt(cnn);



            String txnid = getorderid();

            HttpContext.Current.Session["txniddd"] = "";
            HttpContext.Current.Session["txnm"] = "";


            HttpContext.Current.Session["stid"] = "";
           


            HttpContext.Current.Session["txniddd"] = txnid;
            HttpContext.Current.Session["txnm"] = idm;
            HttpContext.Current.Session["stid"] = studentid;
            





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "INSERT INTO `tblattempt_paymenttransactiontestid` (`Attempt_ID`, `Student_ID`, `Test_ID`, `Processed`, `Submitted`, `Start_Datetime`, `End_Datetime`, `transactionid`, `transactiondate`, `status`, `totalamount`, `modeofpayment`, `final_marks`, `final_percentage`, `status_p`) VALUES    ('" + idm + "','" + studentid + "', 'na','0','0','" + mytime + "','" + mytime + "','" + txnid + "','" + mytime + "', 'na', '" + pricee + "', 'modeofpayment', 'na', 'na','na')";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "newreg";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }


        /*****************************getmax transaction ***************************************/



        public int getmaxtt(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(Attempt_ID) from tblattempt_paymenttransactiontestid";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /************************************update table transaction *****************************************************/



        public String updatepaymenttable(string studentid, String id, String trsn)
        {





            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `tblattempt_paymenttransactiontestid`   set `Processed`='1', `Submitted`='1', `Start_Datetime`='" + mytime + "',  `status`='success',  `status_p`='" + trsn + "' where `Attempt_ID`='" + id + "'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }



        /**********************************************************************************************************************/



        /********************************update transaction table data on failure transaction **********************************************************/
        public String updatepaymenttablek(string studentid, String id, String trsn)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `tblattempt_paymenttransactiontestid`   set `Processed`='1', `Submitted`='1', `Start_Datetime`='" + mytime + "',  `status`='fail',  `status_p`='" + trsn + "' where `Attempt_ID`='" + id + "'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }






        /**********************************************************************************************************************************/






        /********************************************Update table applied data *****************************************************/

        public String updateforregfeepaid(string studentid,  String trsn)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `studentrecord_applied`   set `isregfeepaid`='YES', `regfeetransactionid`='"+trsn+"'  where `regid`='"+studentid+"'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }


        /******************************************************************************************************************************/






        /********************************Insert data for admission fee ***************************************************************************************/



        /******************************Insert transaction table *************************************************************/

        /****************************Insert Transaction Table **************************************************************************/
        public String inserttransactiontableadmission(string studentid, String pricee)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;


            int idm = getmaxttadmission(cnn);



            String txnid = getorderid();

            HttpContext.Current.Session["txnidddq"] = "";
            HttpContext.Current.Session["txnmq"] = "";


            HttpContext.Current.Session["stidq"] = "";



            HttpContext.Current.Session["txnidddq"] = txnid;
            HttpContext.Current.Session["txnmq"] = idm;
            HttpContext.Current.Session["stidq"] = studentid;






            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "INSERT INTO `tblattempt_paymenttransactiontestidadmission` (`Attempt_ID`, `Student_ID`, `Test_ID`, `Processed`, `Submitted`, `Start_Datetime`, `End_Datetime`, `transactionid`, `transactiondate`, `status`, `totalamount`, `modeofpayment`, `final_marks`, `final_percentage`, `status_p`) VALUES    ('" + idm + "','" + studentid + "', 'na','0','0','" + mytime + "','" + mytime + "','" + txnid + "','" + mytime + "', 'na', '" + pricee + "', 'modeofpayment', 'na', 'na','na')";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "newreg";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }


        /*****************************getmax transaction ***************************************/



        public int getmaxttadmission(MySqlConnection cnn)
        {
            object maxID;
            int a;
            int x = 0;



            /*MySqlConnection cnn;
            cnn = conn();





            cnn.Open();

          
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans; */


            MySqlCommand myCommand = cnn.CreateCommand();

            try
            {
                myCommand.CommandText = "SELECT MAX(Attempt_ID) from tblattempt_paymenttransactiontestidadmission";
                maxID = myCommand.ExecuteScalar();

                //  System.Diagnostics.Debug.Write(maxID);



                if (Convert.IsDBNull(maxID))
                {
                    a = 1;
                    //int id = 1;
                    //return id; 
                    a = x + a;
                }
                else
                {
                    a = Convert.ToInt32(maxID) + 1;
                    //return id;
                    a = x + a;
                }



                return a;


            }
            catch (Exception e)
            {

                return 0;
            }
            finally
            {
                //cnn.Close();
            }







        }




        /************************************update table transaction *****************************************************/



        public String updatepaymenttableadmission(string studentid, String id, String trsn)
        {





            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `tblattempt_paymenttransactiontestidadmission`   set `Processed`='1', `Submitted`='1', `Start_Datetime`='" + mytime + "',  `status`='success',  `status_p`='" + trsn + "' where `Attempt_ID`='" + id + "'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }



        /**********************************************************************************************************************/



        /********************************update transaction table data on failure transaction **********************************************************/
        public String updatepaymenttablekadmission(string studentid, String id, String trsn)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `tblattempt_paymenttransactiontestidadmission`   set `Processed`='1', `Submitted`='1', `Start_Datetime`='" + mytime + "',  `status`='fail',  `status_p`='" + trsn + "' where `Attempt_ID`='" + id + "'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }






        /**********************************************************************************************************************************/






        /********************************************Update table applied data *****************************************************/

        public String updateforregfeepaidadmission(string studentid, String trsn)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `studentrecord_applied`   set `isadmissionfeepaid`='YES', `admissionfeepaidid`='" + trsn + "'  where `regid`='" + studentid + "'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }


        /******************************************************************************************************************************/




        /**********************************Veryfy student for admission *************************************/

        public String verifydtl(string studentid, String trsn)
        {

            MySqlConnection cnn;
            cnn = conn();
            cnn.Open();

            //  System.Diagnostics.Debug.Write(maxID);

            MySqlCommand myCommand = cnn.CreateCommand();
            MySqlTransaction myTrans;


            myTrans = cnn.BeginTransaction();

            myCommand.Connection = cnn;
            myCommand.Transaction = myTrans;





            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string format = "yyyy-MM-dd HH:mm:ss ";
            var mytime = time.ToString(format);

            DateTime time2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



            try
            {

                myCommand.CommandText = "update `studentrecord_applied`   set `veryfiedforadmission`='YES-" + trsn + "'  where `regid`='" + studentid + "'";
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return "success";


            }
            catch (Exception e)
            {
                try
                {


                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        return "error";

                        //st = "error";
                    }
                }

                //   st = "error";
                System.Diagnostics.Debug.Write(e.ToString());
                return e.ToString();

                //   return "error";

            }
            finally
            {
                cnn.Close();
            }

            //String fgr = HttpContext.Current.Session["signhotoid"].ToString();
            // System.Diagnostics.Debug.Write(fgr);
            return null;




        }



        /******************************************************************************************************/


        /********************************************************************************************************************************************************/










    }








}




