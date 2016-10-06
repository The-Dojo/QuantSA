using QuantSA;
using QuantSA.MonteCarlo;
using MonteCarlo;
using System;
using System.Collections.Generic;

public class EuropeanOptionScript : ProductWrapper
{
    private Date exerciseDate = new Date(2017, 08, 28);
    private Share share = new Share("AAA", Currency.ZAR);
    private double strike = 100.0;

    public EuropeanOptionScript() : base(Currency.ZAR)
    {
        Init();
    }
    
    public override List<Cashflow> GetCFs()
    {
        double amount = Math.Max(0, Get(share, exerciseDate) - strike);
        return new List<Cashflow>() {new Cashflow(exerciseDate, amount, share.currency) };        
    }
}