using Pr_magazin;

namespace TestProject2
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Test1()
        {
            // Arrange
            var adminprofile = new adminprofile(8);

            // Act
            adminprofile.LoadProductData(8);

            // Assert
            Assert.IsNotNull(adminprofile.userList);
            // Добавьте дополнительные проверки при необходимости
        }
    }
}
	
