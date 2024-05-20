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
            int userId = 8; // ��������� ������������� ������������
            bool isProfileLoaded = false;

            // Act
            try
            {
                adminprofile adminProfile = new adminprofile(userId);
                isProfileLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� ������� ��������������: {ex.Message}");
            }

            // Assert
            Assert.IsTrue(isProfileLoaded);
        }
    }
}