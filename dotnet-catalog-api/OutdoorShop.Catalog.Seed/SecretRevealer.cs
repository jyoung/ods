namespace OutdoorShop.Catalog.Seed
{
    using System;
    using Microsoft.Extensions.Options;
    
    public interface ISecretRevealer
    {
        void Reveal();
    }

    public class SecretRevealer : ISecretRevealer
    {
        private readonly AzureAccountDetails secrets;
        // I’ve injected <em>secrets</em> into the constructor as setup in Program.cs
        public SecretRevealer (IOptions<AzureAccountDetails> options)
        {
            // We want to know if secrets is null so we throw an exception if it is
            secrets = options.Value ?? throw new ArgumentNullException(nameof(secrets));
        }

        public void Reveal()
        {
            //I can now use my mapped secrets below.
            Console.WriteLine($"CosmosDb Endpoint: {secrets.CosmosDbEndpoint}");
            Console.WriteLine($"CosmosDb Key: {secrets.CosmosDbKey}");
        }
    }
}