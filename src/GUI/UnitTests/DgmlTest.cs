﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DgmlBuilder;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class DgmlTest
    {
        private readonly DebugViewParser _parser = new DebugViewParser();
        private string _template;

        [SetUp]
        public void Setup()
        {
            _template = GetTemplate();
        }


        [Test]
        public void ParseDebugViewSample1()
        {
            // Arrange
            var debugView = File.ReadAllLines("Aw2014Person.txt");

            // Act
            var result = _parser.Parse(debugView, "Test");

            // Assert
            Assert.AreEqual(110, result.Nodes.Count);
            Assert.AreEqual(122, result.Links.Count);

            Assert.AreEqual(2, result.Links.Count(n => n.Contains("IsUnique=\"True\"")));
        }

        [Test]
        public void ParseDebugViewFkBug()
        {
            // Arrange
            var debugView = File.ReadAllLines("Northwind.txt");

            // Act
            var result = _parser.Parse(debugView, "Test");

            // Assert
            Assert.AreEqual(129, result.Nodes.Count);
            Assert.AreEqual(141, result.Links.Count);

            Assert.AreEqual(0, result.Links.Count(n => n.Contains("IsUnique=\"True\"")));
        }

        [Test]
        public void ParseDebugViewMultiColFk()
        {
            // Arrange
            var debugView = File.ReadAllLines("Pfizer.txt");

            // Act
            var result = _parser.Parse(debugView, "Test");

            // Assert
            Assert.AreEqual(160, result.Nodes.Count);
            Assert.AreEqual(172, result.Links.Count);
        }

        [Test]
        public void ParseDebugViewIssue604()
        {
            // Arrange
            var debugView = File.ReadAllLines("Issue604.txt");

            // Act
            var result = _parser.Parse(debugView, "Test");

            // Assert
            Assert.AreEqual(124, result.Nodes.Count);
            Assert.AreEqual(150, result.Links.Count);
        }

        [Test]
        public void BuildSample1()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("Aw2014Person.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\Aw2014Person.dgml", result, Encoding.UTF8);
        }

        [Test]
        public void BuildNorthwind()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("Northwind.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\northwind.dgml", result, Encoding.UTF8);
        }

        [Test]
        public void BuildPfizer()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("Pfizer.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\pfizer.dgml", result, Encoding.UTF8);
        }

        [Test]
        public void BuildBNoFk()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("NoFk.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\nofk.dgml", result, Encoding.UTF8);
        }

        [Test]
        public void BuildSingleNav()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("SingleNav.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\singlenav.dgml", result, Encoding.UTF8);
        }

        [Test]
        public void BuildSamurai()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("Samurai.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\Samurai.dgml", result, Encoding.UTF8);
        }

        [Test]
        public void BuildIssue604()
        {
            // Act
            var builder = new DgmlBuilder.DgmlBuilder();
            var result = builder.Build(File.ReadAllText("Issue604.txt"), "test", _template);

            // Assert
            Assert.AreNotEqual(result, null);

            File.WriteAllText(@"C:\temp\Issue604.dgml", result, Encoding.UTF8);
        }

        private static string GetTemplate()
        {
            var resourceName = "UnitTests.template.dgml";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
