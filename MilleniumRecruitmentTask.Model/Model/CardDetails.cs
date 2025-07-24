using MilleniumRecruitmentTask.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilleniumRecruitmentTask.Model.Model
{
    /// <summary>
    /// Szczegóły karty pobierane z zewnętrznego serwisu lub bazy.
    /// </summary>
    /// <param name="CardNumber">Unikalny numer karty</param>
    /// <param name="CardType">Typ karty (Prepaid/Debit/Credit)</param>
    /// <param name="CardStatus">Aktualny status karty</param>
    /// <param name="IsPinSet">Flaga informująca, czy PIN został ustawiony</param>
    public record CardDetails(
        string CardNumber,
        CardType CardType,
        CardStatus CardStatus,
        bool IsPinSet
    );
}
