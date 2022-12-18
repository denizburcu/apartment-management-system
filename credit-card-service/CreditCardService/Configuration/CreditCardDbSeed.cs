using CreditCardService.Models;
using MongoDB.Driver;

namespace CreditCardService.Configuration
{
    public class CreditCardDbSeed
    {
        public static void SeedCreditCard(IMongoCollection<CreditCard> collection)
        {
            var isAnyCardExists = collection.Find(p => true).Any();
            if (isAnyCardExists)
                return;
            else
            {
                collection.InsertManyAsync(new List<CreditCard>()
                {
                    new CreditCard()
                    {
                        CardNumber = "378282246310005",
                        Balance = 5000,
                        CvvCvs = "123",
                        UserIdentityNumber = "1234567890",
                        ExpireDate = "12/30",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    },
                    new CreditCard()
                    {
                        CardNumber = "454582246312222",
                        Balance = 5000,
                        CvvCvs = "333",
                        UserIdentityNumber = "1234567890",
                        ExpireDate = "12/26",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now

                    },
                    new CreditCard()
                    {
                        CardNumber = "333382244444222",
                        Balance = 5000,
                        CvvCvs = "777",
                        UserIdentityNumber = "3334567890",
                        ExpireDate = "12/22",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new CreditCard()
                    {
                        CardNumber = "333382244444222",
                        Balance = 5000,
                        CvvCvs = "777",
                        UserIdentityNumber = "3334567890",
                        ExpireDate = "12/22",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new CreditCard()
                    {
                        CardNumber = "111182277777222",
                        Balance = 5000,
                        CvvCvs = "232",
                        UserIdentityNumber = "3334666890",
                        ExpireDate = "12/27",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new CreditCard()
                    {
                        CardNumber = "111182277777222",
                        Balance = 5000,
                        CvvCvs = "232",
                        UserIdentityNumber = "3334666890",
                        ExpireDate = "12/27",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    }
                });
            }
        }

    }
}
