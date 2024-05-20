
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Pr_magazin
{
  
    public partial class products : UserControl
    {
        private int currentUserId;

        public products(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void rotate90Button2_Click(object sender, RoutedEventArgs e)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                var selectedProduct = db.tovar.FirstOrDefault(p => p.name == NameTextBlock.Text);

                if (selectedProduct != null)
                {
                    
                    var orders = new orders
                    {
                        tovar_id = selectedProduct.id,
                        users_id = currentUserId, 
                         
                    };

                    db.orders.Add(orders);
                    db.SaveChanges();

                    MessageBox.Show("Товар добавлен в корзину!");
                }
            }
        }
    }
}
