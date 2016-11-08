﻿using ExcelDna.Integration;
using QuantSA.Excel;
using QuantSA.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU = QuantSA.Excel.ExcelUtilities;

namespace QuantSA.Excel
{
    public class XLFX
    {
        [QuantSAExcelFunction(Description = "Create a curve to be used for FX rate forecasting.",
        Name = "QSA.CreateFXForecastCurve",
        Category = "QSA.FX",
        IsHidden = false,
        HelpTopic = "http://www.quantsa.org/CreateFXForecastCurve.html")]
        public static object CreateFXForecastCurve([ExcelArgument(Description = "Name of object")]String name,
            [ExcelArgument(Description = "The base currency.  Values are measured in units of counter currency per one base currency.(Currency)")]object[,] baseCurrency,
            [ExcelArgument(Description = "The counter currency.  Values are measured in units of counter currency per one base currency.(Currency)")]object[,] counterCurrency,
            [ExcelArgument(Description = "The rate at the anchor date of the two curves.")]object[,] fxRateAtAnchorDate,
            [ExcelArgument(Description = "A curve that will be used to obatin forward rates.")]object[,] baseCurrencyFXBasisCurve,
            [ExcelArgument(Description = "A curve that will be used to obtain forward rates.")]object[,] counterCurrencyFXBasisCurve)
        {
            try
            {
                FXForecastCurve fxForecastCurve = new FXForecastCurve(XU.GetCurrency0D(baseCurrency, "baseCurrency"),
                    XU.GetCurrency0D(counterCurrency, "counterCurrency"), XU.GetDouble0D(fxRateAtAnchorDate, "fxRateAtAnchorDate"),
                    XU.GetObject0D<IDiscountingSource>(baseCurrencyFXBasisCurve, "baseCurrencyFXBasisCurve"),
                    XU.GetObject0D<IDiscountingSource>(counterCurrencyFXBasisCurve, "counterCurrencyFXBasisCurve"));
                return XU.AddObject(name, fxForecastCurve);
            }
            catch (Exception e)
            {
                return XU.Error0D(e);
            }
        }

        [QuantSAExcelFunction(Description = "Get the FX rate at a date.  There is no spot settlement adjustment.",
        Name = "QSA.GetFXRate",
        Category = "QSA.FX",
        IsHidden = false,
        HelpTopic = "http://www.quantsa.org/GetFXRate.html")]
        public static object GetFXRate([ExcelArgument(Description = "Name of FX curve")]object[,] FXCurveName,
            [ExcelArgument(Description = "Date on which FX rate is required.")]object[,] date)
        {
            try
            {
                IFXSource fxCurve = XU.GetObject0D<IFXSource>(FXCurveName, "FXCurveName");
                return fxCurve.GetRate(XU.GetDate0D(date, "date"));
            }
            catch (Exception e)
            {
                return XU.Error0D(e);
            }
        }
    }
}
