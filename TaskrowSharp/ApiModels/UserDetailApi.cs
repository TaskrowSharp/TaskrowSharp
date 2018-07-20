using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserDetailApi
    {
        public string CityBirth { get; set; }
        public string StateBirth { get; set; }
        public string CountryBirth { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public string CivilStatus { get; set; }
        public string CivilStatusName { get; set; }
        public string DateBirth { get; set; }
        public string email2 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string CellNumber2 { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string RGDate { get; set; }
        public string Reservist { get; set; }
        public string UserScholarityID { get; set; }
        public string UserScholarity { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HomeAddress { get; set; }
        public string HomeAddressDetail { get; set; }
        public string HomeAddressNumber { get; set; }
        public string HomeCity { get; set; }
        public string HomeDistrict { get; set; }
        public string HomeState { get; set; }
        public string HomeZipCode { get; set; }
        public string Information { get; set; }

        public List<UserFunctionPeriodApi> UserFunctionPeriod { get; set; }
    }
}
