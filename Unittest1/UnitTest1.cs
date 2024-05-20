namespace Unittest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void TestAdminProfileLoadProductData()
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