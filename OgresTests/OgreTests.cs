
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ogres.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Ogres.Tests
{
    [TestClass()]
    public class OgreTests
    {
        private List<Plat> plats = new List<Plat>();


        [TestInitialize]
        public void TestInitialize()
        {
            var type1 = new TypePlat() { Nom = "V", Probabilite = 3 };
            var type2 = new TypePlat() { Nom = "C", Probabilite = 3 };
            var type3 = new TypePlat() { Nom = "S", Probabilite = 3 };

        plats = new List<Plat>
        {
        new Plat { PlatId = 1, DateCreation = DateTime.Now, Taille = 5, TypePlat = type1 },
        new Plat { PlatId = 2, DateCreation = DateTime.Now, Taille = 10, TypePlat = type2 },
        new Plat { PlatId = 3, DateCreation = DateTime.Now, Taille = 15, TypePlat = type3 }
        };
        }

        [TestMethod()]
        public void StrategieGrossePortionTest()
        {
            Assert.AreEqual(plats[1], Ogre.StrategieGrossePortion(plats.AsQueryable()));
        }

        [TestMethod()]
        public void StrategieCarnivoreTest()
        {
            Assert.AreEqual(plats[2], Ogre.StrategieCarnivore(plats.AsQueryable()));
        }

        [TestMethod()]
        public void StrategieVegetarienTest()
        {
            Assert.AreEqual(plats[2], Ogre.StrategieVegetarien(plats.AsQueryable()));
        }

        [TestMethod()]
        public void StrategieSnobTest()
        {
            Assert.AreEqual(plats[2], Ogre.StrategieSnob(plats.AsQueryable()));
        }

        [TestMethod()]
        public void RetraitDePlatTest()
        {
            var mockContexte = new Mock<BuffetBDContext>();
            mockContexte.Setup(c => c.Plats).ReturnsDbSet(plats);

            TableOgres t = new TableOgres
            {
                contexte = mockContexte.Object
            };

            var platSupp = t.ListePlat(p => p.FirstOrDefault());
            mockContexte.Verify(m => m.Plats.Remove(platSupp), Times.Once);
            mockContexte.Verify(m => m.Plats.Remove(It.IsAny<Plat>()), Times.Once);
            mockContexte.Verify(m => m.SaveChanges());
        }

        [TestMethod()]
        public void MangerTest()
        {
            var mockNextPlat = new Mock<Func<Plat>>();
            mockNextPlat.Setup(p => p()).Returns(plats[1]);

            var mockDormir = new Mock<Action<int>>();
            var mockAnnoncer = new Mock<Action>();

            Ogre o = new Ogre("TestOgre", mockNextPlat.Object);
            o.Dormir = mockDormir.Object;
            o.annoncer = mockAnnoncer.Object;
            o.Manger();

            mockNextPlat.Verify(p => p(), Times.Once);
            mockAnnoncer.Verify(a => a(), Times.Exactly(2));
            mockDormir.Verify(d => d(plats[1].Taille), Times.Once);
            Assert.IsNull(o.platSeFaisantManger);
        }
    }

}
