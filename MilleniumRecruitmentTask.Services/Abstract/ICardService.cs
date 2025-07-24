using MilleniumRecruitmentTask.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilleniumRecruitmentTask.Services.Abstract
{
    /// <summary>
    /// Interfejs serwisu karty - udostępnia pobranie szczegółów i listy dozwolonych akcji.
    /// </summary>
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber);
        Task <IReadOnlyList<string>> GetAllowedActions(string userId, string cardNumber);
    }
}
