namespace TaskrowSharp.Models.ClientModels;

public class ClientAddressInsertRequest
{
    public int ClientID { get; private set; }
    public string? ClientNickName { get; private set; }
    public int ClientAddressID { get; private set; }
    public bool NewClientAddress { get; private set; }

    public string? SocialContractName { get; set; }

    public string? ProductListstring { get; set; }
    public string? Location { get; set; }
    public int CountryID { get; set; }
    public int CityID { get; set; }
    public string? StateName { get; set; }
    public string? CityName { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
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

    public ClientAddressInsertRequest(
        int clientID, 
        string clientNickName, 
        int countryID = 31,
        int cityID = 1, 
        string street = "há definir",
        string? number = null)
    {
        ClientID = clientID;
        ClientAddressID = 0;
        ClientNickName = clientNickName;
        NewClientAddress = true;
        
        CountryID = countryID;
        CityID = cityID;
        Street = street;
        Number = number;

        Inactive = false;
    }

    public ClientAddressInsertRequest(int clientID, string clientNickName, string socialContractName,
        string? cnpj = null, string? cpf = null,
        int countryID = 31, int cityID = 1, string? stateName = null, string? cityName = null, string? street = null, string? number = null, string? complement = null)
    {
        ClientID = clientID;
        ClientAddressID = 0;
        ClientNickName = clientNickName;
        NewClientAddress = true;

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
            NoCNPJ = true;
        }
        
        CountryID = countryID;
        CityID = cityID;
        StateName = stateName;
        CityName = cityName;
        Street = street;
        Number = number;
        Complement = complement;

        Inactive = false;
    }
}
