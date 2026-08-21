using NUnit.Framework;
using FeeSystem;

[TestFixture]
public class FeeCalculatorTests
{
    // Checklist 1
    [Test]
    public void OutstandingBalance_NoPayments_ReturnsFullFee()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal>();

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(600m));
    }

    // Checklist 2
    [Test]
    public void OutstandingBalance_PartialPayment_ReturnsRemainingBalance()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 200m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(400m));
    }

    // Checklist 3
    [Test]
    public void OutstandingBalance_SeveralInstalments_ReturnsRemainingBalance()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 200m, 200m, 100m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(100m));
    }

    // Checklist 4
    [Test]
    public void OutstandingBalance_FullyPaid_ReturnsZero()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 600m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    // Checklist 5
    [Test]
    public void OutstandingBalance_Overpayment_ReturnsNegativeBalance()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 700m };

        // Act
        var result = calc.OutstandingBalance(600m, payments);

        // Assert
        Assert.That(result, Is.EqualTo(-100m));
    }

    // Checklist 6
    [Test]
    public void OutstandingBalance_NegativeFee_ThrowsArgumentException()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal>();

        // Act and Assert
        Assert.That(
            () => calc.OutstandingBalance(-1m, payments),
            Throws.ArgumentException);
    }

    // Checklist 7
    [Test]
    public void IsClearedForExams_ExactlyHalfPaid_ReturnsTrue()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 300m };

        // Act
        var result = calc.IsClearedForExams(600m, payments);

        // Assert
        Assert.That(result, Is.True);
    }

    // Checklist 8
    [Test]
    public void IsClearedForExams_OneToeaUnderHalf_ReturnsFalse()
    {
        // Arrange
        var calc = new FeeCalculator();
        var payments = new List<decimal> { 299.99m };

        // Act
        var result = calc.IsClearedForExams(600m, payments);

        // Assert
        Assert.That(result, Is.False);
    }
}
