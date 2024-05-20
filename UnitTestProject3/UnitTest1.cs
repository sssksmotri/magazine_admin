using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pr_magazin;
using System;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;


namespace UnitTestProject3
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_SaveTov_Successfully()
        {
            // Arrange
            var dbMock = new Mock<magazinEntities14>();
            var izmenenieTov = new izmenenie_tov(8)
            {
                selectedProduct = new tovar
                {
                    name = "jeens",
                    color = "Красный",
                    size = 48,
                    gender = "Мужской",
                    brend = "KJ",
                    price = 1002,
                    image_tovar = "jeens.jpg"
                },
                db = dbMock.Object
            };

            
            izmenenieTov.save_tov();

            dbMock.Verify(db => db.SaveChanges(), Times.Once);
        }


        [TestMethod]
        public void Test_CreateTov_Successfully()
        {
            // Arrange
            var fakeDbSet = new Mock<DbSet<tovar>>();
            var dbMock = new Mock<magazinEntities14>();
            dbMock.Setup(db => db.tovar).Returns(fakeDbSet.Object); 

            var dobavTovar = new dobav_tovar(8)
            {
                db = dbMock.Object 
            };

            
            dobavTovar.newProduct.name = "jeens";
            dobavTovar.newProduct.color = "Синий";
            dobavTovar.newProduct.size = 49;
            dobavTovar.newProduct.gender = "Мужской";
            dobavTovar.newProduct.brend = "Nike";
            dobavTovar.newProduct.image_tovar = "jeens.jpg";
            dobavTovar.newProduct.price = 2500;

            // Act
            dobavTovar.create_tov();

            // Assert
            fakeDbSet.Verify(db => db.Add(It.IsAny<tovar>()), Times.Once); 
            dbMock.Verify(db => db.SaveChanges(), Times.Once); 
        }
        [TestMethod]
        public void Test_DeleteTov_Successfully()
        {
            
            var dbMock = new Mock<magazinEntities14>();
            var delete_Tovar = new delete_tovar(8)
            {
                db = dbMock.Object
            };

            
            var fakeProductList = new List<tovar>
            {
                new tovar
                {
                id = 21,
                name = "TestProduct",
                color = "Red",
                size = 48,
                gender = "Мужской",
                price = 1002,
                brend = "TestBrend"
                }
            };

            var fakeDbSet = new Mock<DbSet<tovar>>();
            fakeDbSet.As<IQueryable<tovar>>().Setup(m => m.Provider).Returns(fakeProductList.AsQueryable().Provider);
            fakeDbSet.As<IQueryable<tovar>>().Setup(m => m.Expression).Returns(fakeProductList.AsQueryable().Expression);
            fakeDbSet.As<IQueryable<tovar>>().Setup(m => m.ElementType).Returns(fakeProductList.AsQueryable().ElementType);
            fakeDbSet.As<IQueryable<tovar>>().Setup(m => m.GetEnumerator()).Returns(fakeProductList.AsQueryable().GetEnumerator());

            dbMock.Setup(db => db.tovar).Returns(fakeDbSet.Object);
            delete_Tovar.delete_tov(21);
            dbMock.Verify(db => db.SaveChanges(), Times.Once);
            dbMock.Verify(db => db.tovar.Remove(It.IsAny<tovar>()), Times.Once);

        }
        [TestMethod]
        public void LoadProductData_ShouldPopulateTovarCollection()
        {
            
            int userId = 8; 
            adminprofile adminProfile = new adminprofile(userId);

            
            adminProfile.LoadProductData(userId);

            
            Assert.IsNotNull(adminProfile.userList, "Product collection is null");
        }
    }
    
}
