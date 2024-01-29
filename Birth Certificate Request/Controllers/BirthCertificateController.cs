using Birth_Certificate_Request.Models;
using Birth_Certificate_Request.Models.DTO;
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
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "sql6.freemysqlhosting.net",
                Database = "sql6679380",
                UserID = "sql6679380",
                Password = "dQfg6iDbqu",
                SslMode = MySqlSslMode.None
            };

            var connection = new MySqlConnection(builder.ConnectionString);
            return connection;
        }





        [HttpGet]
        public ActionResult GetCertificateData([FromQuery] long BRNumber)
        {

            var connection = MysqlDBConnection();




            connection.Open();
            var command = connection.CreateCommand();
            //command.CommandText = $"SELECT * FROM birthdata WHERE BRNumber = {BRNumber}";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "BRCDataGet";

            command.Parameters.AddWithValue("@BRNumber", BRNumber);

            command.ExecuteNonQuery();

            var reader = command.ExecuteReader();

            //List<BirthCertificateDTO> dataList =new List<BirthCertificateDTO>();

            //BirthCertificateDTO data = new BirthCertificateDTO();

            //var dataList = typeof(BirthCertificateDTO).GetProperties().Select(p=> p.Name).ToList();


            string[] data = { };

            while (reader.Read())
            {
                //BirthCertificateDTO data = new BirthCertificateDTO();

                int count = reader.FieldCount;

                
                for(int i = 0; i<count; i++)
                {
                    var value = reader.GetValue(i);
                    data.Append(value);

                    //data[i] = value;
                    
                }

            }

            connection.Close();


            return Ok(data);

        }

        [HttpPost]
        public async void PostBirthCertificate([FromBody] BirthCertificate data)
        {

           


            var connection = MysqlDBConnection();

            await connection.OpenAsync();



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



            int result = await command.ExecuteNonQueryAsync();

            Console.WriteLine(result);


            await connection.CloseAsync();

            if (Base64.IsValid(data.ImageData))
            {
                

                Console.WriteLine("image data Base64");
            }


        }
    }
}
