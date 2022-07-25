using System;
using System.Collections.Generic;
using System.Globalization;
using Basket.DataAccess.Interface;
using Basket.Models;
using Bogus;
using Bogus.DataSets;

namespace Basket.DataAccess
{
    public class DataGenerator : IDataGenerator
    {
        public List<Product> GenerateProducts(int count)
        {
            Randomizer.Seed = new Random(123456);

            var productGenerator = new Faker<Product>("uk")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                .RuleFor(o => o.Description, f => f.Commerce.ProductDescription())
                .RuleFor(o => o.Picture, f => f.Image.PlaceImgUrl())
                .RuleFor(o => o.Availability, f => f.Random.Bool(0.9f))
                .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(), NumberStyles.Currency))
                .RuleFor(o => o.CreateDate, f => f.Date.Recent(10))
                .RuleFor(o => o.ModifyDate, f => f.Date.Recent(3));

            return productGenerator.Generate(count);
        }

        public List<Buyer> GenerateBuyers(int count)
        {
            Randomizer.Seed = new Random(123456);

            var orderGenerator = new Faker<Buyer>("uk")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o => o.Address, f => f.Address.FullAddress())
                .RuleFor(o => o.Comment, f => f.Lorem.Sentence())
                .RuleFor(o => o.FirstName, f => f.Name.FirstName(Name.Gender.Female))
                .RuleFor(o => o.LastName, f => f.Name.LastName(Name.Gender.Female))
                .RuleFor(o => o.PhoneNumber, f => $"0{Randomizer.Seed.Next(910000000, 999999999)}");

            return orderGenerator.Generate(count);
        }

        public List<BuyerWithCart> GenerateBuyersWithCarts(int count)
        {
            throw new System.NotImplementedException();
        }
    }
}
