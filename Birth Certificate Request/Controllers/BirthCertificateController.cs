using Birth_Certificate_Request.Models;
using Birth_Certificate_Request.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Buffers.Text;

namespace Birth_Certificate_Request.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthCertificateController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetCertificateData([FromQuery] long BRNumber)
        {
            //Console.WriteLine(BRNumber);

            


            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "BirthCertificate",
                UserID = "root",
                Password = "password1234",
                SslMode = MySqlSslMode.None
            };

            var connection = new MySqlConnection(builder.ConnectionString);

            

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM birthdata WHERE BRNumber = {BRNumber}";

            command.ExecuteNonQuery();

            var reader = command.ExecuteReader();

            BirthCertificateDTO data = new BirthCertificateDTO();

            while (reader.Read())
            {
                int count = reader.FieldCount;

                

                for(int i = 0; i<count; i++)
                {

                    var value = reader.GetValue(i);

                    Console.WriteLine(value);

                    //data[i] = value;
                    
                }

            }

            connection.Close();


            return Ok(data);

        }

        [HttpPost]
        public async void PostBirthCertificate([FromBody] BirthCertificate data)
        {

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database= "BirthCertificate",
                UserID= "root",
                Password= "password1234",
                SslMode = MySqlSslMode.None
            };


            var connection = new MySqlConnection(builder.ConnectionString);

            await connection.OpenAsync();

            // create a DB command and set the SQL
            var command = connection.CreateCommand();
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
