using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;



namespace e_Sales
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var serviceBusClient =
               new SubscriptionClient(
                   "Endpoint = sb://e-venda.servicebus.windows.net/;SharedAccessKeyName=Policy_e_stock;SharedAccessKey=SSDQqghBDpRz3lG9Bwz77Y5huBkNKz3fG+qbwd6x1ts=",
                   "produtocriado",
                   "produtovendido"
                   );

            MessageHandlerOptions messageHandlerOptions1 = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            MessageHandlerOptions messageHandlerOptions = messageHandlerOptions1;

            serviceBusClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            while (true)
            {

            }


        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private static Task ProcessMessageAsync(Message message, CancellationToken arg2)
        {
            var produtoCriado = message.Body.ParseJson<ProdutoCriado>();

            Console.WriteLine(produtoCriado.ToString());

            return Task.CompletedTask;
        }

       
        internal class ProdutoVendido
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
            /// Parses a Utf8 byte json to a specific object.
            /// </summary>
            /// <typeparam name="T">type of object to be parsed.</typeparam>
            /// <param name="json">The json bytes.</param>
            /// <returns>the object parsed from json.</returns>
            public static T ParseJson<T>(this byte[] json)
            {
                if (json == null || json.Length == 0) return default;
                var result = JsonConvert.DeserializeObject<T>(Utf8NoBom.GetString(json), JsonSettings);
                return result;
            }

        }
    }
}

