
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Pr_magazin
{
 
    public partial class products2 : UserControl
    {
        private int currentUserId;
        public products2(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void rotate90Button2_Click(object sender, RoutedEventArgs e)
        {
            
            var parentCanvas = this.Parent as Canvas;
            if (parentCanvas != null)
            {
                parentCanvas.Children.Remove(this);
            }

       
            using ( magazinEntities14 db = new magazinEntities14()) 
            {
                
                var orderToDelete = db.orders.FirstOrDefault(o => o.users_id == currentUserId);
                if (orderToDelete != null)
                {
                    db.orders.Remove(orderToDelete);
                    db.SaveChanges();
                }
            }
        }





    }
}
