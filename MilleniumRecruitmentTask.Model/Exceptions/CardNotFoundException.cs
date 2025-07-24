using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilleniumRecruitmentTask.Model.Exceptions
{
    /// <summary>
    /// Wyjątek rzucany, gdy nie znaleziono karty dla użytkownika.
    /// </summary>
    /// 
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException(string userId, string cardNumber)
            : base($"Card '{cardNumber}' for user '{userId}' not found.")
        {
        }
    }
}
