using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MilleniumRecruitmentTask.Model.Enums;
using MilleniumRecruitmentTask.Model.Exceptions;
using MilleniumRecruitmentTask.Model.Model;
using MilleniumRecruitmentTask.Model.Rules;
using MilleniumRecruitmentTask.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MilleniumRecruitmentTask.Services.Concrete
{
    /// <summary>
    /// Implementacja ICardService z przykładowym in-memory store i definicją reguł.
    /// </summary>
    public class CardService : ICardService
    {
        private readonly ILogger<CardService> _logger;
        private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();
        public CardService(ILogger<CardService> logger)
        {
            _logger = logger;
            // W konstruktorze inicjalizujemy store i logujemy
            _userCards = CreateSampleUserCards();
            _logger.LogDebug("Sample user cards: {UserCardsJson}", JsonSerializer.Serialize(_userCards));
        }
        public async Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
        {
            // At this point, we would typically make an HTTP call to an external service
            // to fetch the data. For this example we use generated sample data.
            await Task.Delay(1000);
            if (!_userCards.TryGetValue(userId, out var cards)
            || !cards.TryGetValue(cardNumber, out var cardDetails))
            {
                return null;
            }
            return cardDetails;
        }
        private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
        {
            var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
            for (var i = 1; i <= 3; i++)
            {
                var cards = new Dictionary<string, CardDetails>();
                var cardIndex = 1;
                foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
                {
                    foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                    {
                        var cardNumber = $"Card{i}{cardIndex}";
                        cards.Add(cardNumber,
                        new CardDetails(
                        CardNumber: cardNumber,
                        CardType: cardType,
                        CardStatus: cardStatus,
                        IsPinSet: cardIndex % 2 == 0));
                        cardIndex++;
                    }
                }
                var userId = $"User{i}";
                userCards.Add(userId, cards);
            }
            //Na potrzeby testów
            return userCards;
        }
        /// <summary>
        /// Zwraca listę dozwolonych akcji lub rzuca CardNotFoundException.
        /// </summary>
        public async Task<IReadOnlyList<string>> GetAllowedActions(string userId, string cardNumber)
        {
            var card = await GetCardDetailsAsync(userId, cardNumber)
                       ?? throw new CardNotFoundException(userId, cardNumber);

            var allowed = AllowedActionsRule.Rules
                .Where(r => r.IsAllowed(card))
                .Select(r => r.ActionName)
                .ToList();
            return allowed;
        }
    }
}
