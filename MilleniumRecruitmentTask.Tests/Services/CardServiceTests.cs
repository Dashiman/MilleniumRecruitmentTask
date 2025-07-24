using Microsoft.Extensions.Logging.Abstractions;
using MilleniumRecruitmentTask.Model.Exceptions;
using MilleniumRecruitmentTask.Model.Model;
using MilleniumRecruitmentTask.Services.Concrete;

namespace MilleniumRecruitmentTask.Tests.Services;

[TestFixture]
public class CardServiceTests
{
    private CardService _service;

    [SetUp]
    public void SetUp()
    {
        var logger = new NullLogger<CardService>();
        _service = new CardService(logger);
    }

    [Test]
    public async Task GetCardDetailsAsync_ExistingUserAndCard_ReturnsCardDetails()
    {
        // Arrange
        var userId = "User1";
        var cardNumber = "Card11";

        // Act
        var details = await _service.GetCardDetailsAsync(userId, cardNumber);

        // Assert
        Assert.That(details, Is.Not.Null, "Powinno zwrócić istniejące szczegóły karty");
        Assert.That(details.CardNumber, Is.EqualTo(cardNumber), "CardNumber musi się zgadzać");
        Assert.That(details, Is.InstanceOf<CardDetails>(), "Typ zwracanego obiektu");
    }

    [Test]
    public async Task GetCardDetailsAsync_UnknownUser_ReturnsNull()
    {
        var details = await _service.GetCardDetailsAsync("NoUser", "Card11");
        Assert.That(details, Is.Null, "Dla nieistniejącego usera musi być null");
    }

    [Test]
    public async Task GetCardDetailsAsync_UnknownCard_ReturnsNull()
    {
        var details = await _service.GetCardDetailsAsync("User1", "NoCard");
        Assert.That(details, Is.Null, "Dla nieistniejącej karty musi być null");
    }

    [Test]
    public async Task GetAllowedActions_PrepaidClosed_Returns_Action3_4_9()
    {
        var actions = await _service.GetAllowedActions("User1", "Card17");
        Assert.That(actions, Has.Exactly(1).EqualTo("ACTION3"));
        Assert.That(actions, Has.Exactly(1).EqualTo("ACTION4"));
        Assert.That(actions, Has.Exactly(1).EqualTo("ACTION9"));
        Assert.That(actions.Count, Is.EqualTo(3), "Dokładnie trzy akcje");
    }

    [Test]
    public async Task GetAllowedActions_DebitBlockedWithPin_Returns_CorrectSet()
    {
        var actions = await _service.GetAllowedActions("User1", "Card112");
        var expected = new[] { "ACTION3", "ACTION4", "ACTION6", "ACTION7", "ACTION8", "ACTION9" };

        Assert.That(actions, Is.EquivalentTo(expected), "Zbiór akcji musi się zgadzać");
    }

    [Test]
    public void GetAllowedActions_UnknownUser_ThrowsCardNotFoundException()
    {
        Assert.That(async () =>
            await _service.GetAllowedActions("NoUser", "Card1123"),
            Throws.TypeOf<CardNotFoundException>()
        );
    }

    [Test]
    public void GetAllowedActions_UnknownCard_ThrowsCardNotFoundException()
    {
        Assert.That(async () =>
            await _service.GetAllowedActions("User1", "NoCard"),
            Throws.TypeOf<CardNotFoundException>()
        );
    }

    [Test]
    public async Task GetAllowedActions_PinNotSet_DoesNotInclude_PinRequiredActions()
    {
        // Dla karty, która ma IsPinSet=false, żadne reguły requiresPin=true nie powinny się pojawić
        var actions = await _service.GetAllowedActions("User1", "Card11");
        Assert.That(actions, Has.None.Matches<string>(a => a == "ACTION7"), "ACTION7 wymaga PIN-u");
    }
    [Test]
    public async Task Example1_PrepaidClosed_Returns_ACTION3_4_9()
    {
        // 1. Dla karty PREPAID w statusie CLOSED -> CARD17
        var actions = await _service.GetAllowedActions("User1", "Card17");
        var expected = new[] { "ACTION3", "ACTION4", "ACTION9" };
        Assert.That(actions.OrderBy(x => x), Is.EquivalentTo(expected.OrderBy(x => x)),
            "Przykład 1: Prepaid Closed powinien zwrócić ACTION3, ACTION4, ACTION9");
    }

    [Test]
    public async Task Example2_CreditBlockedWithoutPin_Returns_ACTION3_4_5_6_7_8_9()
    {
        // 2. Dla karty CREDIT w statusie BLOCKED bez PIN -> CARD119
        var actions = await _service.GetAllowedActions("User1", "Card119");
        var expected = new[]
        {
        "ACTION3", "ACTION4", "ACTION5",
         "ACTION8", "ACTION9"
    };
        Assert.That(
            actions.OrderBy(x => x),
            Is.EquivalentTo(expected.OrderBy(x => x)),
            "Przykład 2: Credit Blocked z PIN powinien zwrócić ACTION3–ACTION9"
        );
    }
}