using Pr_magazin;
using Moq;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools;
using System.Linq;
namespace TestProject3
{
    

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_SaveTov_Successfully()
        {
            // Arrange
            var dbMock = new Mock<magazinEntities14>();
            var izmenenieTov = new izmenenie_tov(1)
            {
                selectedProduct = new tovar
                {
                    id = 1,
                    name = "TestProduct",
                    color = "Red",
                    size = 10,
                    gender = "�������",
                    brend = "TestBrend",
                    price = 1002,
                },
                db = dbMock.Object
            };

            // Setup ��� �������� ����������� ������� � ����
            dbMock.Setup(db => db.SaveChanges()).Returns(() => 1); // ���������� ����� ��������, �.�. ����� ��������� ������ ����� SaveChanges()

            // Act
            izmenenieTov.save_tov();

            // Assert
            dbMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Test_SaveTov_Exception()
        {
            // Arrange
            var dbMock = new Mock<magazinEntities14>(); // �������� magazinEntities14 �� ��� �������� ����� ��������� ���� ������
            var izmenenieTov = new izmenenie_tov(8) // ��������������, ��� ����������� ��������� ID ������������
            {
                selectedProduct = new tovar // �����������, ��� ��� ��� ����� ������
                {
                    id = 2,
                    name = "TestProduct",
                    color = "Red",
                    size = 48,
                    gender = "�������",
                    price = 1230,
                    brend = "TestBrend"
                },
                db = dbMock.Object
            };

            dbMock.Setup(db => db.SaveChanges()).Throws(new Exception("Test Exception"));

            // Act
            izmenenieTov.save_tov();

            // Assert
            dbMock.Verify(db => db.SaveChanges(), Times.Once); // ���������, ��� ����� SaveChanges ������ ���� ���
        }
    }
    [TestMethod]
        public void Test_AddProduct_Successfully()
        {
            // Arrange
            var dbMock = new Mock<IMagazinContext>();
            var dobavTovar = new dobav_tovar(8);
            tovar newProduct = new tovar
            {
                name = "TestProduct",
                color = "�������",
                size = 48,
                gender = "�������",
                price = decimal.Parse("1002"),
                brend = "TestBrend",
            };

            // Setup ��� ��������� ����������
            dbMock.Setup(db => db.SaveChanges()).Returns(() => 1);

            // Act
            dobavTovar.create_tov();

            // Assert
            dbMock.Verify(db => db.tovar.Add(It.IsAny<tovar>()), Times.Once);
            dbMock.Verify(db => db.SaveChanges(), Times.Once);
        }
    }
}
