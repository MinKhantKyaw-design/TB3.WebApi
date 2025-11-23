using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TB3.ConsoleAppConnectWithApi.Dtos;
using static System.Net.Mime.MediaTypeNames;

namespace TB3.ConsoleAppConnectWithApi;

public class HttpClientService
{
    private readonly string _baseUrl = "https://localhost:7258";
    public async Task Read()
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{_baseUrl}/api/Product");
        if (response.IsSuccessStatusCode)
        {
            var lst = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
            //Console.WriteLine(json);

            Console.WriteLine("Product List:");
            Console.WriteLine("----------------------------");
            foreach (ProductDto item in lst)
            {
                Console.WriteLine($"Product Name    : {item.ProductName}");
                Console.WriteLine($"Product Price   : {item.Price.ToString("n2")}");
                Console.WriteLine($"Product Quantity: {item.Quantity.ToString("n0")}");
                Console.WriteLine("----------------------------");
            }
        }
    }

    public async Task Create()
    {
        Console.Write("Please enter product name: ");
        string productName = Console.ReadLine();

        Console.Write("Please enter price: ");
        decimal price = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Please enter quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        ProductCreateRequestDto requestDto = new ProductCreateRequestDto()
        {
            Price = price,
            ProductName = productName,
            Quantity = quantity
        }; // object to json

        string requestJson = JsonConvert.SerializeObject(requestDto);

        StringContent content = new StringContent(requestJson, Encoding.UTF8, Application.Json);

        HttpClient client = new HttpClient();
        var response = await client.PostAsync($"{_baseUrl}/api/Product", content);
        var message = await response.Content.ReadAsStringAsync();
        Console.WriteLine(message);
    }
}
