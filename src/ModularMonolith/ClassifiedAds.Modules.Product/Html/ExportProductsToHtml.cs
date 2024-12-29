﻿using ClassifiedAds.CrossCuttingConcerns.Html;
using System.Collections.Generic;

namespace ClassifiedAds.Modules.Product.Html;

public record ExportProductsToHtml : IHtmlRequest
{
    public List<Entities.Product> Products { get; set; }
}