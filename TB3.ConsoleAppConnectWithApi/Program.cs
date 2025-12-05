

using System.Net.Http.Json;
using TB3.ConsoleAppConnectWithApi.Dtos;

namespace TB3.ConsoleAppConnectWithApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

        // API - Server
        // Console - Client

        //HttpClient client = new HttpClient();
        //var response = await client.GetAsync("https://localhost:7258/api/Product");
        //if (response.IsSuccessStatusCode)
        //{
        //    var lst = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        //    //Console.WriteLine(json);

        //    Console.WriteLine("Product List:");
        //    Console.WriteLine("----------------------------");
        //    foreach (ProductDto item in lst)
        //    {
        //        Console.WriteLine($"Product Name    : {item.ProductName}");
        //        Console.WriteLine($"Product Price   : {item.Price.ToString("n2")}");
        //        Console.WriteLine($"Product Quantity: {item.Quantity.ToString("n0")}");
        //        Console.WriteLine("----------------------------");
        //    }
        //}
        Start:
            Console.WriteLine("-- Welcome to Product API --");
            Console.WriteLine("Choose Menu: 1 - Read, 2 - Create, 3 - Update, 4 - Patch, 5 - Exit");
            int num = Convert.ToInt32(Console.ReadLine());

            HttpClientService httpClientService = new HttpClientService();

            switch (num)
            {
                case 1:
                    await httpClientService.Read();
                    goto Start;
                case 2:
                    await httpClientService.Create();
                    goto Start;
                case 3:
                    await httpClientService.Update(); 
                    goto Start;
                case 4:
                    await httpClientService.Patch();
                    goto Start;
                case 5:
                default:
                    Console.WriteLine("Exit app...");
                    goto Exit;
            }

        Exit:
            Console.ReadLine();
        }
    }
}
