﻿using Newtonsoft.Json;
using System.Numerics;
using TestPost.Models;

namespace TestPost.Services
{
    public class AddressService
    {
        public async Task<Address> RetrieveAdressAPI(AddressDTO addressDTO)
        {

            Address address = new();
            string _url = "https://viacep.com.br/ws";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_url}/{addressDTO.ZipCode}/json/";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        address = JsonConvert.DeserializeObject<Address>(json);
                        address.ZipCode = addressDTO.ZipCode;
                        address.Number = addressDTO.Number;
                    }
                    else
                    {
                        address = null;
                        Console.WriteLine("Erro no consumo do WS CEP.");
                        Console.WriteLine(response.StatusCode);
                    }
                }
                return address;
            }
            catch
            {
                throw;
            }
        }

    }
}