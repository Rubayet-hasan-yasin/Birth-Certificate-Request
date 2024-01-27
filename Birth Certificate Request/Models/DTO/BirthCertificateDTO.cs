using System.ComponentModel.DataAnnotations;

namespace Birth_Certificate_Request.Models.DTO
{
    public class BirthCertificateDTO
    {
        // registerNo
        
        public int RegisterNo { get; set; }


        //date Of Issue
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }


        //Date of Registration

        [DataType(DataType.Date)]
        public DateTime DateOfRegistration { get; set; }


        // Birth Registration Number
       
        [Range(10000000000000000, 99999999999999999, ErrorMessage = "BR Number must be 17 digits")]
        public long BRNumber { get; set; }


        //Name 
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;


        //Date of Birth
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        //Place Of Birth
        public string PlaceOfBirth { get; set; } = string.Empty;



        //image in base64                SGVsbG8sIHRoaXMgaXMgYSBiYXNlNjQtZW5jb2RlZCBzdHJpbmcu
        [Base64String(ErrorMessage = "Image data is must be a valid base64 string")]
        public string ImageData { get; set; } = string.Empty;

        //Permanent Address
        [MinLength(5)]
        public string PermanentAddress { get; set; } = string.Empty;


        //Father's Name
        public string FatherName { get; set; } = string.Empty;

        //Father's Birth Registration Number
        public long? FatherBRN { get; set; }

        //Father's NID number
        public long? FatherNIDNumber { get; set; }


        //Father's Nationality
        public string FatherNationality { get; set; } = string.Empty;




        //Mother's Name
        public string MotherName { get; set; } = string.Empty;

        //Mother's Birht Registration Number
        public long? MotherBRN { get; set; }


        //Mother's NID Number
        public long? MotherNIDNumber { get; set; }


        //Mother's Nationality
        public string MotherNationality { get; set; } = string.Empty;
    }
}
