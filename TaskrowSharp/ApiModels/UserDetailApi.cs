using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserDetailApi
    {
        public object CityBirth { get; set; }
        public object StateBirth { get; set; }
        public object CountryBirth { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public object CivilStatus { get; set; }
        public string CivilStatusName { get; set; }
        public object DateBirth { get; set; }
        public object email2 { get; set; }
        public object PhoneNumber2 { get; set; }
        public object CellNumber2 { get; set; }
        public object CPF { get; set; }
        public object RG { get; set; }
        public object RGDate { get; set; }
        public object Reservist { get; set; }
        public object UserScholarityID { get; set; }
        public string UserScholarity { get; set; }
        public object FatherName { get; set; }
        public object MotherName { get; set; }
        public object HomeAddress { get; set; }
        public object HomeAddressDetail { get; set; }
        public object HomeAddressNumber { get; set; }
        public object HomeCity { get; set; }
        public object HomeDistrict { get; set; }
        public object HomeState { get; set; }
        public object HomeZipCode { get; set; }
        public object Information { get; set; }
    }
}
