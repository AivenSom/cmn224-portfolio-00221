using System;
using System.Collections.Generic;
using System.Linq;

namespace FeeSystem;

public class FeeCalculator
{
    // Returns what the student still owes for the term. 
    public decimal OutstandingBalance(
        decimal termFee, IEnumerable<decimal> payments)
    {
        if (termFee < 0)
            throw new ArgumentException("Fee cannot be negative");

        var paid = payments.Sum();
        return termFee - paid;
    }

    // Rule: must pay at least half the term fee to sit exams. 
    public bool IsClearedForExams(
        decimal termFee, IEnumerable<decimal> payments)
    {
        var paid = payments.Sum();
        return paid >= termFee / 2;
    }
}
