using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace requestapp
{
    class Program
    {
        static async Task Main()
        {
            using var client = new HttpClient();
            var sw = new Stopwatch();

            while (true)
            {
                Console.Write("URL to query: ");
                try
                {
                    var uri = Console.ReadLine();
                    sw.Restart();
                    using var resp = await client.GetAsync(uri);

                    sw.Stop();
                    Console.WriteLine($"Returned HTTP {resp.StatusCode} in {sw.ElapsedMilliseconds} ms");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Try again...");
                }

            }
        }
    }
}
