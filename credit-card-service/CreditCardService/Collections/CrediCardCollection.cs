using CreditCardService.Configuration;
using CreditCardService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CreditCardService.Collections
{
    public class CrediCardCollection : ICreditCardCollection
    {
        private readonly IMongoCollection<CreditCard> collection;

        public CrediCardCollection(IOptions<CreditCardDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            collection = database.GetCollection<CreditCard>(settings.Value.CreditCardCollectionName);
            SeedCollections();
        }

        public void SeedCollections()
        {
            CreditCardDbSeed.SeedCreditCard(collection);
        }

        public async Task<CreditCard> FindByCondition(Expression<Func<CreditCard, bool>> expression)
        {
            var document = await collection.Find(expression).FirstOrDefaultAsync();
            return document;
        }

        public async Task UpdateByCondition(Expression<Func<CreditCard, bool>> expression, CreditCard document)
        {
            await collection.ReplaceOneAsync(expression, document);
        }
    }
}
