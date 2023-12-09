using System.Collections.Generic;

namespace TaxCalculator.Api.Tax.Details;

public class TaxDetailsResponse
{
    public List<TaxDetail> TaxDetailList { get; set; }
}