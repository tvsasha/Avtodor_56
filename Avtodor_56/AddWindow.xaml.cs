using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Avtodor_56
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private Avtodor_56Entities db;

        public AddWindow()
        {
            InitializeComponent();
            db = new Avtodor_56Entities();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Materials newMaterial = new Materials
            {
                MaterialID = Convert.ToInt32(txtID.Text),
                Name = txtName.Text,
                Unit = txtUnit.Text,
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text)
            };

            db.Materials.Add(newMaterial);
            db.SaveChanges();
            this.Close();
        }
    }
}
