using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;



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

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

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
    }
}

