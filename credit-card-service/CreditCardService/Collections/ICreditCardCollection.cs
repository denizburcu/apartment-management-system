using CreditCardService.Models;
using System.Linq.Expressions;

namespace CreditCardService.Collections
{
    public interface ICreditCardCollection
    {
        Task<CreditCard> FindByCondition(Expression<Func<CreditCard, bool>> expression);
        Task UpdateByCondition(Expression<Func<CreditCard, bool>> expression, CreditCard document);
    }
}
