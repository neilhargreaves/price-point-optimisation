using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PricePointOptimisation
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void BasicTests()
        {
            var products = new List<IProduct>
            {
                new Product("A", 68.91, 68.85, 69.05),
                new Product("B", 71.92, 71.92, 71.98),
                new Product("C", 69.93, 69.96, 69.98),
                new Product("D", 68.54, 68.96, 69.98),
                new Product("E", 68.95, 68.85, 69.05),
                new Product("F", 68.96, 68.85, 69.05),
                new Product("G", 68.97, 68.85, 69.05),
                new Product("H", 68.98, 68.85, 69.05),
                new Product("I", 68.99, 68.85, 69.05),
                new Product("J", 69.00, 68.85, 69.95),
                new Product("K", 69.10, 68.85, 69.05),
                new Product("L", 68.50, 68.85, 69.95)
            };

            var pricePoints = new List<IDigit>
            {
                new Digit(3),
                new Digit(5),
                new Digit(9)
            };

            IProductProcessor productProcessor = ProductProcessorFactory
                .GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            Assert.AreEqual(68.93, products[0].NewPrice, 0.002, "Product A New Price incorrect");
            Assert.AreEqual("Price increased", products[0].Message, "Product A Message incorrect");

            Assert.AreEqual(71.93, products[1].NewPrice, 0.002, "Product B New Price incorrect");
            Assert.AreEqual("Price increased", products[1].Message, "Product B Message incorrect");

            Assert.AreEqual(69.93, products[2].NewPrice, 0.002, "Product C New Price incorrect");
            Assert.AreEqual("Unable to set price", products[2].Message, "Product C Message incorrect");

            Assert.AreEqual(68.99, products[3].NewPrice, 0.002, "Product D New Price incorrect");
            Assert.AreEqual("Price increased", products[3].Message, "Product D Message incorrect");

            Assert.AreEqual(68.95, products[4].NewPrice, 0.002, "Product E New Price incorrect");
            Assert.AreEqual("Current price is optimal", products[4].Message, "Product E Message incorrect");

            Assert.AreEqual(68.95, products[5].NewPrice, 0.002, "Product F New Price incorrect");
            Assert.AreEqual("Price reduced", products[5].Message, "Product F Message incorrect");

            Assert.AreEqual(68.99, products[6].NewPrice, 0.002, "Product G New Price incorrect");
            Assert.AreEqual("Price increased", products[6].Message, "Product G Message incorrect");

            Assert.AreEqual(68.99, products[7].NewPrice, 0.002, "Product H New Price incorrect");
            Assert.AreEqual("Price increased", products[7].Message, "Product H Message incorrect");

            Assert.AreEqual(68.99, products[8].NewPrice, 0.002, "Product I New Price incorrect");
            Assert.AreEqual("Current price is optimal", products[8].Message, "Product I Message incorrect");

            Assert.AreEqual(68.99, products[9].NewPrice, 0.002, "Product J New Price incorrect");
            Assert.AreEqual("Price reduced", products[9].Message, "Product J Message incorrect");

            Assert.AreEqual(69.05, products[10].NewPrice, 0.002, "Product K New Price incorrect");
            Assert.AreEqual("Price reduced", products[10].Message, "Product K Message incorrect");

            Assert.AreEqual(68.85, products[11].NewPrice, 0.002, "Product L New Price incorrect");
            Assert.AreEqual("Price increased", products[11].Message, "Product L Message incorrect");
        }

        [TestMethod]
        public void EdgeCaseTests()
        {
            var products = new List<IProduct>
            {
                new Product("A", 68.91, 68.85, 69.05),
                new Product("B", 71.92, 71.92, 71.98),
                new Product("C", 69.93, 69.96, 69.98),
                new Product("D", 68.54, 68.96, 69.98),
                new Product("E", 68.95, 68.85, 69.05),
                new Product("F", 68.96, 68.85, 69.05),
                new Product("G", 68.97, 68.85, 69.05),
                new Product("H", 68.98, 68.85, 69.05),
                new Product("I", 68.99, 68.85, 69.05),
                new Product("J", 69.00, 68.85, 69.95),
                new Product("K", 69.10, 68.85, 69.05),
                new Product("L", 68.50, 68.85, 69.95)
            };

            var pricePoints = new List<IDigit>
            {
                new Digit(3),
                new Digit(9)
            };

            IProductProcessor productProcessor = ProductProcessorFactory
                .GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            Assert.AreEqual(68.93, products[0].NewPrice, 0.002, "Product A New Price incorrect");
            Assert.AreEqual("Price increased", products[0].Message, "Product A Message incorrect");

            Assert.AreEqual(71.93, products[1].NewPrice, 0.002, "Product B New Price incorrect");
            Assert.AreEqual("Price increased", products[1].Message, "Product B Message incorrect");

            Assert.AreEqual(69.93, products[2].NewPrice, 0.002, "Product C New Price incorrect");
            Assert.AreEqual("Unable to set price", products[2].Message, "Product C Message incorrect");

            Assert.AreEqual(68.99, products[3].NewPrice, 0.002, "Product D New Price incorrect");
            Assert.AreEqual("Price increased", products[3].Message, "Product D Message incorrect");

            Assert.AreEqual(68.93, products[4].NewPrice, 0.002, "Product E New Price incorrect");
            Assert.AreEqual("Price reduced", products[4].Message, "Product E Message incorrect");

            Assert.AreEqual(68.99, products[5].NewPrice, 0.002, "Product F New Price incorrect");
            Assert.AreEqual("Price increased", products[5].Message, "Product F Message incorrect");

            Assert.AreEqual(68.99, products[6].NewPrice, 0.002, "Product G New Price incorrect");
            Assert.AreEqual("Price increased", products[6].Message, "Product G Message incorrect");

            Assert.AreEqual(68.99, products[7].NewPrice, 0.002, "Product H New Price incorrect");
            Assert.AreEqual("Price increased", products[7].Message, "Product H Message incorrect");

            Assert.AreEqual(68.99, products[8].NewPrice, 0.002, "Product I New Price incorrect");
            Assert.AreEqual("Current price is optimal", products[8].Message, "Product I Message incorrect");

            Assert.AreEqual(68.99, products[9].NewPrice, 0.002, "Product J New Price incorrect");
            Assert.AreEqual("Price reduced", products[9].Message, "Product J Message incorrect");

            Assert.AreEqual(69.03, products[10].NewPrice, 0.002, "Product K New Price incorrect");
            Assert.AreEqual("Price reduced", products[10].Message, "Product K Message incorrect");

            Assert.AreEqual(68.89, products[11].NewPrice, 0.002, "Product L New Price incorrect");
            Assert.AreEqual("Price increased", products[11].Message, "Product L Message incorrect");
        }

        [TestMethod]
        public void SinglePointTests()
        {
            var products = new List<IProduct>
            {
                new Product("A", 68.91, 68.85, 69.05),
                new Product("B", 71.92, 71.92, 71.98),
                new Product("C", 69.93, 69.96, 69.98),
                new Product("D", 68.54, 68.96, 69.98),
                new Product("E", 68.95, 68.85, 69.05),
                new Product("F", 68.96, 68.85, 69.05),
                new Product("G", 68.97, 68.85, 69.05),
                new Product("H", 68.98, 68.85, 69.05),
                new Product("I", 68.93, 68.85, 69.05),
                new Product("J", 69.00, 68.85, 69.95),
                new Product("K", 69.10, 68.85, 69.05),
                new Product("L", 68.50, 68.85, 69.95)
            };

            var pricePoints = new List<IDigit>
            {
                new Digit(3)
            };

            IProductProcessor productProcessor = ProductProcessorFactory
                .GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            Assert.AreEqual(68.93, products[0].NewPrice, 0.002, "Product A New Price incorrect");
            Assert.AreEqual("Price increased", products[0].Message, "Product A Message incorrect");

            Assert.AreEqual(71.93, products[1].NewPrice, 0.002, "Product B New Price incorrect");
            Assert.AreEqual("Price increased", products[1].Message, "Product B Message incorrect");

            Assert.AreEqual(69.93, products[2].NewPrice, 0.002, "Product C New Price incorrect");
            Assert.AreEqual("Unable to set price", products[2].Message, "Product C Message incorrect");

            Assert.AreEqual(69.03, products[3].NewPrice, 0.002, "Product D New Price incorrect");
            Assert.AreEqual("Price increased", products[3].Message, "Product D Message incorrect");

            Assert.AreEqual(68.93, products[4].NewPrice, 0.002, "Product E New Price incorrect");
            Assert.AreEqual("Price reduced", products[4].Message, "Product E Message incorrect");

            Assert.AreEqual(68.93, products[5].NewPrice, 0.002, "Product F New Price incorrect");
            Assert.AreEqual("Price reduced", products[5].Message, "Product F Message incorrect");

            Assert.AreEqual(68.93, products[6].NewPrice, 0.002, "Product G New Price incorrect");
            Assert.AreEqual("Price reduced", products[6].Message, "Product G Message incorrect");

            Assert.AreEqual(69.03, products[7].NewPrice, 0.002, "Product H New Price incorrect");
            Assert.AreEqual("Price increased", products[7].Message, "Product H Message incorrect");

            Assert.AreEqual(68.93, products[8].NewPrice, 0.002, "Product I New Price incorrect");
            Assert.AreEqual("Current price is optimal", products[8].Message, "Product I Message incorrect");

            Assert.AreEqual(69.03, products[9].NewPrice, 0.002, "Product J New Price incorrect");
            Assert.AreEqual("Price increased", products[9].Message, "Product J Message incorrect");

            Assert.AreEqual(69.03, products[10].NewPrice, 0.002, "Product K New Price incorrect");
            Assert.AreEqual("Price reduced", products[10].Message, "Product K Message incorrect");

            Assert.AreEqual(68.93, products[11].NewPrice, 0.002, "Product L New Price incorrect");
            Assert.AreEqual("Price increased", products[11].Message, "Product L Message incorrect");
        }

        [TestMethod]
        public void ErrorTests()
        {
            var products = new List<IProduct>
            {
                new Product("A", 68.91, 69.80, 69.05),
                new Product("B", 71.92, 71.92, 71.88)
            };

            var pricePoints = new List<IDigit>
            {
                new Digit(3)
            };

            IProductProcessor productProcessor = ProductProcessorFactory
                .GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            Assert.AreEqual(68.91, products[0].NewPrice, 0.002, "Product A New Price incorrect");
            Assert.AreEqual("An error occured :: Price Range Incorrect", products[0].Message, "Product A Message incorrect");

            Assert.AreEqual(71.92, products[1].NewPrice, 0.002, "Product B New Price incorrect");
            Assert.AreEqual("An error occured :: Price Range Incorrect", products[1].Message, "Product B Message incorrect");
        }          
    }
}
