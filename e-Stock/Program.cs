using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;


namespace e_Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var produtoCriado = new ProdutoCriado { Id = 101, Codigo = 1012, Quantidade = 0, Nome = "Kapoo", Preco = 2.25 };

            var serviceBusClient = new TopicClient("Endpoint = sb://e-venda.servicebus.windows.net/;SharedAccessKeyName=Policy_e_stock;SharedAccessKey=SSDQqghBDpRz3lG9Bwz77Y5huBkNKz3fG+qbwd6x1ts=", "produtocriado");


            var message = new Message(produtoCriado.ToJsonBytes());
            
	    Console.WriteLine("Start");

            while(true)
           {

            Console.ReadKey();
            serviceBusClient.SendAsync(message);
            Console.WriteLine("ProdutoCriado");
            
           }
                   
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }


    }

    internal class ProdutoCriado
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Quantidade { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        public override string ToString()
        {
            return $"Id{Id}, Codigo{Codigo}, Quantidade{Quantidade}, Nome{Nome}, Preco{Preco}";

        }
    }

    public static class Utils
    {
        private static readonly UTF8Encoding Utf8NoBom = new UTF8Encoding(false);

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
            Converters = new JsonConverter[] { new StringEnumConverter() }
        };

        /// <summary>
        /// Converts the object to json bytes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static byte[] ToJsonBytes(this object source)
        {
            if (source == null) 
                return null;
            var instring = JsonConvert.SerializeObject(source, Formatting.Indented, JsonSettings);
            return Utf8NoBom.GetBytes(instring);
        }
    }
}
