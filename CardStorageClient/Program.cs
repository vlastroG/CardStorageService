using Grpc.Net.Client;
using System.Net.WebSockets;
using static CardStorageServiceProtos.CardService;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2UnencryptedSupport", true);

            //CardServiceClient

            //ClientServiceClient 

            using var channel = GrpcChannel.ForAddress("http://localhost:5001");

            ClientServiceClient clientService = new ClientServiceClient(channel);

            var createClientResponse = clientService.Create(new CardStorageServiceProtos.CreateClientRequest
            {
                FirstName = "Роман",
                Surname = "Чехов",
                Patronymic = "Николаевич"
            });

            Console.WriteLine($"Client {createClientResponse.ClientId} created successfully\n=============================");

            CardServiceClient cardService = new CardServiceClient(channel);

            var getByClientIdResponse = cardService.GetByClientId(new CardStorageServiceProtos.GetByClientIdRequest
            {
                ClientId = 1
            });
            foreach (var card in getByClientIdResponse.Cards)
            {
                Console.WriteLine($"{card.CardN}; {card.Name}; {card.CVV2}; {card.ExpDate}");
            }
            Console.ReadKey(true);
        }
    }
}