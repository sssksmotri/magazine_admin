using Pr_magazin;
using System.Windows;

namespace UnitTest_LR_4
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int userId = 8; // Примерный идентификатор пользователя
            bool isProfileLoaded = false;

            // Act
            try
            {
                adminprofile adminProfile = new adminprofile(userId);
                isProfileLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке профиля администратора: {ex.Message}");
            }

            // Assert
            Assert.IsTrue(isProfileLoaded);
        }
    }
}