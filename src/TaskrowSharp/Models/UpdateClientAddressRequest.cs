using System;

namespace TaskrowSharp.Models
{
    public class UpdateClientAddressRequest
    {
        public int ClientID { get; private set; }
        public string ClientNickName { get; private set; }
        public int ClientAddressID { get; private set; }
        public bool NewClientAddress { get; private set; }

        public string SocialContractName { get; set; }

        public string? ProductListString { get; set; }
        public string? Location { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? ZipCode { get; set; }
        
        public bool NoCNPJ { get; private set; }
        public string? CNPJ { get; private set; }
        public string? CPF { get; private set; }

        public string? InscrEstad { get; set; }
        public string? InscrMunic { get; set; }
        public string? ExternalCode { get; set; }
        public bool Inactive { get; set; }

        public UpdateClientAddressRequest(int clientID, string clientNickName, int clientAddressID, string socialContractName, 
            string? cnpj, string? cpf,
            int countryID = 31, int cityID = 7352, string stateName = "SP", string cityName = "SAO PAULO", string street = "Av. Paulista", string number = "1578")
        {
            ClientID = clientID;
            ClientNickName = clientNickName;
            ClientAddressID = clientAddressID;
            NewClientAddress = false;

            SocialContractName = socialContractName;

            if (!string.IsNullOrEmpty(cnpj))
            {
                CNPJ = cnpj;
                NoCNPJ = false;
            }
            else if (!string.IsNullOrEmpty(cpf))
            {
                CPF = cpf;
                NoCNPJ = true;
            }
            else
            {
                throw new ArgumentException(null, nameof(cnpj));
            }

            CountryID = countryID;
            CityID = cityID;
            StateName = stateName;
            CityName = cityName;
            Street = street;
            Number = number;
        }
    }
}
