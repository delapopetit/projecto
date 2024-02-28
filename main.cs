
            if ((priceModel.CallForPrice || priceModel.CustomerEntersPrice) && !isBundleItemPricing)
            {
                if (priceModel.CallForPrice)
                {
                    model.HotlineTelephoneNumber = _contactDataSettings.HotlineTelephoneNumber.NullEmpty();
                }
                return;
            }

            var tierPriceModels = await tierPrices
                .SelectAwait(async (tierPrice) =>
                {
                    calculationContext.Quantity = tierPrice.Quantity;

                    var price = await _priceCalculationService.CalculatePriceAsync(calculationContext);

                    var tierPriceModel = new TierPriceModel
                    {
                        Quantity = tierPrice.Quantity,
                        Price = price.FinalPrice
                    };

                    return tierPriceModel;
                })
                .AsyncToList();
