using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Avtodor_56
{
    /// <summary>
    /// Логика взаимодействия для OneWindow.xaml
    /// </summary>
    public partial class OneWindow : Window
    {
        private Materials _selectedMaterial;
        private Avtodor_56Entities db;

        public OneWindow(Materials material)
        {
            InitializeComponent();
            db = new Avtodor_56Entities();
            _selectedMaterial = material;
            txtID.Text = _selectedMaterial.MaterialID.ToString();
            txtName.Text = _selectedMaterial.Name;
            txtUnit.Text = _selectedMaterial.Unit;
            txtUnitPrice.Text = _selectedMaterial.UnitPrice.ToString();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _selectedMaterial.Name = txtName.Text;
            _selectedMaterial.Unit = txtUnit.Text;
            _selectedMaterial.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            db.SaveChanges();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMaterial != null)
            {
                var materialToDelete = db.Materials.Find(_selectedMaterial.MaterialID);

                if (materialToDelete != null)
                {
                    db.Materials.Remove(materialToDelete);
                    db.SaveChanges();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Запись не найдена в базе данных.");
                }
            }
        }
    }
}
