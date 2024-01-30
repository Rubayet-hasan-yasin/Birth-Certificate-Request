using Birth_Certificate_Request.Models;
using Birth_Certificate_Request.Models.DTO;
using Birth_Certificate_Request.Models.Response;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Buffers.Text;
using System.Data;

namespace Birth_Certificate_Request.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthCertificateController : ControllerBase
    {
        [NonAction]
        public MySqlConnection MysqlDBConnection()
        {
            //var builder = new MySqlConnectionStringBuilder
            //{
            //    Server = "sql6.freemysqlhosting.net",
            //    Database = "sql6679380",
            //    UserID = "sql6679380",
            //    Password = "dQfg6iDbqu",
            //    SslMode = MySqlSslMode.None
            //};

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysql-361cb314-yasinarafat12346-f563.a.aivencloud.com",
                Database = "defaultdb",
                UserID = "avnadmin",
                Password = "AVNS_GRZzi6LVsHD21VTzoog",
                SslMode = MySqlSslMode.Required,
                Port = 10957
            };


            var connection = new MySqlConnection(builder.ConnectionString);
            return connection;
        }





        [HttpGet]
        public BirthCertificateResponseGlobal GetCertificateData([FromQuery] long BRNumber)
        {

            

            var data = new BirthCertificateResponseGlobal();
            data.response = new Response();
            data.BirthCertificateRes = new BirthCertificateDTO();


           
                var connection = MysqlDBConnection();

                connection.Open();
                var command = connection.CreateCommand();
                //command.CommandText = $"SELECT * FROM birthdata WHERE BRNumber = {BRNumber}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BRCDataGet";

                command.Parameters.AddWithValue("@BRN", BRNumber);

            //command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();
            
                while (reader.Read())
                {


                    data.BirthCertificateRes.RegisterNo = Convert.ToInt16(reader["RegisterNo"].ToString());
                    data.BirthCertificateRes.DateOfIssue = Convert.ToDateTime(reader["DateOfIssue"].ToString());
                    data.BirthCertificateRes.DateOfRegistration = Convert.ToDateTime(reader["DateOfRegistration"].ToString());
                    data.BirthCertificateRes.BRNumber = Convert.ToInt64(reader["BRNumber"].ToString());
                    data.BirthCertificateRes.Name = reader["Name"].ToString();
                    data.BirthCertificateRes.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"].ToString());
                    data.BirthCertificateRes.PlaceOfBirth = reader["ImageData"].ToString();
                    data.BirthCertificateRes.PermanentAddress = reader["PermanentAddress"].ToString();
                    data.BirthCertificateRes.FatherName = reader["FatherName"].ToString();
                    data.BirthCertificateRes.FatherBRN = Convert.ToInt64(reader["FatherBRN"].ToString());
                    data.BirthCertificateRes.FatherNIDNumber = Convert.ToInt64(reader["FatherNIDNumber"].ToString());
                    data.BirthCertificateRes.FatherNationality = reader["FatherNationality"].ToString();
                    data.BirthCertificateRes.MotherName = reader["MotherName"].ToString();
                    data.BirthCertificateRes.MotherBRN = Convert.ToInt64(reader["MotherBRN"].ToString());
                    data.BirthCertificateRes.MotherNIDNumber = Convert.ToInt64(reader["MotherNIDNumber"].ToString());
                    data.BirthCertificateRes.MotherNationality = reader["MotherNationality"].ToString();



                    if (!string.IsNullOrEmpty(data.BirthCertificateRes.Name))
                    {
                        data.response.StatusCode = 200;
                        data.response.ResponseMessage = "Success";
                    }
                    else
                    {
                        data.response.StatusCode = 404;
                        data.response.ResponseMessage = "Data Not't Found";
                    }


                }
            


                connection.Close();


            return data;

        }

        [HttpPost]
        public Response PostBirthCertificate([FromBody] BirthCertificate data)
        {

           Response response = new Response();


            var connection = MysqlDBConnection();

            connection.Open();



            // create a DB command and set the SQL
            var command = connection.CreateCommand();


            /*
            command.CommandText = "CREATE TABLE IF NOT EXISTS BirthData(" +
                "RegisterNo INT NOT NULL," +
                "DateOfIssue DATE,"+
                "DateOfRegistration DATE,"+
                "BRNumber BIGINT,"+
                "Name VARCHAR(30)," +
                "DateOfBirth DATE," +
                "PlaceOfBirth VARCHAR(70)," +
                "ImageData MEDIUMTEXT," +
                "PermanentAddress VARCHAR(100)," +
                "FatherName VARCHAR(50),"+
                "FatherBRN BIGINT," +
                "FatherNIDNumber BIGINT,"+
                "FatherNationality VARCHAR(30),"+
                "MotherName VARCHAR(50),"+
                "MotherBRN BIGINT,"+
                "MotherNIDNumber BIGINT,"+
                "MotherNationality VARCHAR(30)"+
                ");";

            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO BirthData VALUES(@RegisterNo, @DateOfIssue, @DateOfRegistration, @BRNumber, @Name, @DateOfBirth, @PlaceOfBirth, @ImageData, @PermanentAddress, @FatherName, @FatherBRN, @FatherNIDNumber, @FatherNationality, @MotherName, @MotherBRN, @MotherNIDNumber, @MotherNationality)";

            command.Parameters.AddWithValue("RegisterNo", data.RegisterNo);
            command.Parameters.AddWithValue("DateOfIssue", data.DateOfIssue);
            command.Parameters.AddWithValue("DateOfRegistration", data.DateOfRegistration);
            command.Parameters.AddWithValue("BRNumber", data.BRNumber);
            command.Parameters.AddWithValue("Name", data.Name);
            command.Parameters.AddWithValue("DateOfBirth", data.DateOfBirth);
            command.Parameters.AddWithValue("PlaceOfBirth", data.PlaceOfBirth);
            command.Parameters.AddWithValue("ImageData", data.ImageData);
            command.Parameters.AddWithValue("PermanentAddress", data.PermanentAddress);
            command.Parameters.AddWithValue("FatherName", data.FatherName);
            command.Parameters.AddWithValue("FatherBRN", data.FatherBRN);
            command.Parameters.AddWithValue("FatherNIDNumber", data.FatherNIDNumber);
            command.Parameters.AddWithValue("FatherNationality", data.FatherNationality);
            command.Parameters.AddWithValue("MotherName", data.MotherName);
            command.Parameters.AddWithValue("MotherBRN", data.MotherBRN);
            command.Parameters.AddWithValue("MotherNIDNumber", data.MotherNIDNumber);
            command.Parameters.AddWithValue("MotherNationality", data.MotherNationality);
            
            */


            command.CommandType = CommandType.StoredProcedure;

            command.CommandText = "BRCDataInsert";

            foreach (var property in typeof(BirthCertificate).GetProperties())
            {
                //Console.WriteLine(property.Name);
                //Console.WriteLine(property.GetValue(data));

                command.Parameters.AddWithValue("@"+ property.Name, property.GetValue(data));

            }



            int result = command.ExecuteNonQuery();

            Console.WriteLine(result);

            if(result != 0)
            {
                response.StatusCode = 201;
                response.ResponseMessage = "Data insert Successfuly";
            }
            else
            {
                response.StatusCode = 406;
                response.ResponseMessage = "There is an error";
            }


            connection.Close();

            if (Base64.IsValid(data.ImageData))
            {
                

                Console.WriteLine("image data Base64");
            }

            return response;


        }
    }
}
