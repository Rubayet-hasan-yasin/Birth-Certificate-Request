using Microsoft.VisualBasic;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace Birth_Certificate_Request.Models
{
    public class BirthCertificate
    {
        // registerNo
        [Required(ErrorMessage = "RegisterNo is Required")]
        public required int RegisterNo { get; set; }


        //date Of Issue
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Issue is required")]
        //[DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public required DateTime DateOfIssue { get; set; }


        //Date of Registration

        [DataType(DataType.Date)]
        [Required(ErrorMessage = " Date of Registration is Required")]
        public required DateTime DateOfRegistration { get; set; }


        // Birth Registration Number
        [Required(ErrorMessage = "BR Number is Required")]
        [Range(10000000000000000, 99999999999999999, ErrorMessage = "BR Number must be 17 digits")]
        public required long BRNumber { get; set; }


        //Name 
        
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3)]
        public required string Name { get; set; }


        //Date of Birth
        [Required(ErrorMessage = "Dete of Birth is Required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        //Place Of Birth
        [Required(ErrorMessage = "Place of Birth is Required")]
        public required string PlaceOfBirth { get; set; }



        //image in base64                SGVsbG8sIHRoaXMgaXMgYSBiYXNlNjQtZW5jb2RlZCBzdHJpbmcu
        [Required(ErrorMessage = "Image Data is Required")]
        [Base64String(ErrorMessage = "Image data is must be a valid base64 string")]
        public required string ImageData { get; set; }

        //Permanent Address
        [Required(ErrorMessage ="Permanent Address is Required")]
        [MinLength(5)]
        public required string PermanentAddress { get; set; }


        //Father's Name
        public string FatherName { get; set; }= string.Empty;

        //Father's Birth Registration Number
        public long? FatherBRN { get; set; }

        //Father's NID number
        public long? FatherNIDNumber { get; set;}


        //Father's Nationality
        public string FatherNationality { get; set; } = string.Empty;




        //Mother's Name
        public string MotherName { get; set; } = string.Empty;

        //Mother's Birht Registration Number
        public long? MotherBRN { get; set; }


        //Mother's NID Number
        public long? MotherNIDNumber { get;set; }


        //Mother's Nationality
        public string MotherNationality { get; set; } = string.Empty;

    }
}
