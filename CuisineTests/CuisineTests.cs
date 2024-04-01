using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cuisine;
using Cuisine.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cuisine.Tests
{
    [TestClass()]
    public class CuisineTests
    {
        private Mock<BuffetBDContext> mockContexte = new Mock<BuffetBDContext>();
        private Mock<IRandomiseur> mockRandom = new Mock<IRandomiseur>();
        private Mock<Func<DateTime>> mockDateTime = new Mock<Func<DateTime>>();


        [TestInitialize]
        public void TestInitialize()
        {
            mockContexte = new Mock<BuffetBDContext>();
            mockRandom = new Mock<IRandomiseur>();
            mockDateTime = new Mock<Func<DateTime>>();
        }

        [TestMethod()]
        public void NouveauPlatTest()
        {
            var type1 = new TypePlat() { Nom = "V", Probabilite = 5 };
            var dateDeCreationAttendue = new DateTime(2022, 03, 27, 2, 34, 55, 392);
            var platDuTest = new Plat(dateDeCreationAttendue, 2000, type1);

            ConfigurerLesMocks(dateDeCreationAttendue, type1);

            var cuisine = CreerUneInstanceDeCuisine();
            var platCree = cuisine.NouveauPlat();

            Assert.AreEqual(dateDeCreationAttendue, platCree.DateCreation);
            Assert.AreEqual(2000, platCree.Taille);
        }

        [TestMethod()]
        public void AjoutPlatTest()
        {
            var type2 = new TypePlat() { Nom = "C", Probabilite = 10 };
            var dateDeCreationAttendue = new DateTime(2022, 03, 27, 2, 34, 55, 392);
            var platDuTest = new Plat(dateDeCreationAttendue, 2000, type2);

            ConfigurerLesMocks(dateDeCreationAttendue, type2);

            var platAjoutes = new List<Plat>();
            mockContexte.Setup(m => m.Plats.Add(It.IsAny<Plat>())).Callback<Plat>(plat => platAjoutes.Add(plat));

            var cuisine = CreerUneInstanceDeCuisine();
            cuisine.Tour();

            Assert.AreEqual(1, platAjoutes.Count);

            var platAjoute = platAjoutes.FirstOrDefault();
            Assert.IsNotNull(platAjoute);
            Assert.AreEqual(platDuTest.DateCreation, platAjoute.DateCreation);
            Assert.AreEqual(platDuTest.Taille, platAjoute.Taille);
            Assert.AreEqual(platDuTest.TypePlat.Nom, platAjoute.TypePlat.Nom);
            Assert.AreEqual(platDuTest.TypePlat.Probabilite, platAjoute.TypePlat.Probabilite);
        }

        private void ConfigurerLesMocks(DateTime dateDeCreationAttendue, TypePlat typePlat)
        {
            mockDateTime.Setup(m => m()).Returns(dateDeCreationAttendue);
            mockRandom.Setup(r => r.ExpoRandom(It.IsAny<double>())).Returns(2000);
            mockRandom.Setup(r => r.DistributionProbabiliteDiscrete(It.IsAny<Dictionary<TypePlat, int>>())).Returns(typePlat);
        }

        private Cuisine CreerUneInstanceDeCuisine()
        {
            return new CuisineBuilder()
                .SetContext(mockContexte.Object)
                .SetAlea(mockRandom.Object)
                .SetFournisseurMaintenant(mockDateTime.Object)
                .Build();
        }
    }
}
