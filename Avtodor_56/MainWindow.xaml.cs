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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace Avtodor_56
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Avtodor_56Entities db;

        public MainWindow()
        {
            InitializeComponent();
            db = new Avtodor_56Entities();
            var combinedData = (from orderDetail in db.OrderDetails
                                join material in db.Materials on orderDetail.MaterialID equals material.MaterialID
                                join order in db.Orders on orderDetail.OrderID equals order.OrderID
                                join employee in db.Employee on order.EmployeeID equals employee.EmployeeID into employeeOrders
                                from employeeOrder in employeeOrders.DefaultIfEmpty() // LEFT JOIN для связи с сотрудниками
                                join client in db.Client on order.ClientID equals client.ClientID into clientOrders
                                from clientOrder in clientOrders.DefaultIfEmpty() // LEFT JOIN для связи с клиентами
                                select new 
                                {
                                    MaterialID = material.MaterialID,
                                    MaterialName = material.Name,
                                    Unit = material.Unit,
                                    UnitPrice = material.UnitPrice,

                                    OrderID = order.OrderID,
                                    OrderDate = order.OrderDate,
                                    OrderStatus = order.Status,

                                    EmployeeID = employeeOrder != null ? employeeOrder.EmployeeID : 0,
                                    EmployeeFullName = employeeOrder != null ? employeeOrder.FullName : "N/A",
                                    EmployeePhone = employeeOrder != null? employeeOrder.Phone : "N/A",
                                    Position = employeeOrder != null ? employeeOrder.Position : "N/A",

                                    ClientID = clientOrder != null ? clientOrder.ClientID : 0,
                                    ClientFullName = clientOrder != null ? clientOrder.FullName : "N/A",
                                    ClientPhone = clientOrder != null ? clientOrder.Phone : "N/A",
                                    Email = clientOrder != null ? clientOrder.Email : "N/A"
                                }).ToList();

            // Установка данных для DataGrid
            dgUnifiedData.ItemsSource = combinedData;
        }

        public class UnifiedData
        {
            public int? EmployeeID { get; set; }
            public string EmployeeFullName { get; set; }
            public string Position { get; set; }
            public string EmployeePhone { get; set; }

            public int? ClientID { get; set; }
            public string ClientFullName { get; set; }
            public string ClientPhone { get; set; }
            public string Email { get; set; }

            public int? MaterialID { get; set; }
            public string MaterialName { get; set; }
            public string Unit { get; set; }
            public decimal? UnitPrice { get; set; }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Materials Mtr = new Materials();
            //Mtr.MaterialID = Convert.ToInt32(oId.Text);
            //Mtr.Name = oD.Text;
            //Mtr.Unit = oS.Text;
            //Mtr.UnitPrice = Convert.ToDecimal(oC.Text);
            //db.Materials.Add(Mtr);
            //db.SaveChanges();
            //dgMaterials.ItemsSource = db.Materials.ToList();
              AddWindow addWindow = new AddWindow();
              addWindow.ShowDialog();
            dgUnifiedData.ItemsSource = db.Materials.ToList();

            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int OdId = Convert.ToInt32(oId.Text);
            var SelectID = db.Materials.Where(w => w.MaterialID == OdId).FirstOrDefault();
            db.Materials.Remove(SelectID);
            db.SaveChanges();
            dgUnifiedData.ItemsSource = db.Materials.ToList();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int UdId = Convert.ToInt32(oId.Text);
            var SelectUID = db.Materials.Where(w => w.MaterialID == UdId).FirstOrDefault();
            SelectUID.MaterialID = Convert.ToInt32(oId.Text);
            SelectUID.Name = oD.Text;
            SelectUID.Unit = oS.Text;
            SelectUID.UnitPrice = Convert.ToDecimal(oC.Text);
            db.SaveChanges();
            dgUnifiedData.ItemsSource = db.Materials.ToList();
        }
        
        private void dgMaterials_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMaterial = (Materials)dgUnifiedData.SelectedItem;
            if (selectedMaterial != null)
            {
                OneWindow oneWindow = new OneWindow(selectedMaterial);
                oneWindow.ShowDialog(); 
                dgUnifiedData.ItemsSource = db.Materials.ToList();
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string filterColumn = (cbFilterColumn.SelectedItem as ComboBoxItem)?.Content.ToString();
            string filterValue = txtFilter.Text.ToLower();

            var filteredData = from orderDetail in db.OrderDetails
                               join material in db.Materials on orderDetail.MaterialID equals material.MaterialID
                               join order in db.Orders on orderDetail.OrderID equals order.OrderID
                               join employee in db.Employee on order.EmployeeID equals employee.EmployeeID into employeeOrders
                               from employeeOrder in employeeOrders.DefaultIfEmpty()
                               join client in db.Client on order.ClientID equals client.ClientID into clientOrders
                               from clientOrder in clientOrders.DefaultIfEmpty()
                               select new
                               {
                                   MaterialID = material.MaterialID,
                                   MaterialName = material.Name,
                                   Unit = material.Unit,
                                   UnitPrice = material.UnitPrice,

                                   EmployeeID = employeeOrder != null ? employeeOrder.EmployeeID : 0,
                                   EmployeeFullName = employeeOrder != null ? employeeOrder.FullName : "N/A",
                                   EmployeePhone = employeeOrder != null ? employeeOrder.Phone : "N/A",
                                   Position = employeeOrder != null ? employeeOrder.Position : "N/A",

                                   ClientID = clientOrder != null ? clientOrder.ClientID : 0,
                                   ClientFullName = clientOrder != null ? clientOrder.FullName : "N/A",
                                   ClientPhone = clientOrder != null ? clientOrder.Phone : "N/A",
                                   Email = clientOrder != null ? clientOrder.Email : "N/A"
                               };

            // Применение фильтрации
            if (!string.IsNullOrEmpty(filterValue))
            {
                switch (filterColumn)
                {
                    case "Employee Name":
                        filteredData = filteredData.Where(x => x.EmployeeFullName.ToLower().Contains(filterValue));
                        break;
                    case "Client Name":
                        filteredData = filteredData.Where(x => x.ClientFullName.ToLower().Contains(filterValue));
                        break;
                    case "Material Name":
                        filteredData = filteredData.Where(x => x.MaterialName.ToLower().Contains(filterValue));
                        break;
                }
            }

            dgUnifiedData.ItemsSource = filteredData.ToList();
        }

        private void LoadData()
        {
            var combinedData = (from orderDetail in db.OrderDetails
                                join material in db.Materials on orderDetail.MaterialID equals material.MaterialID
                                join order in db.Orders on orderDetail.OrderID equals order.OrderID
                                join employee in db.Employee on order.EmployeeID equals employee.EmployeeID into employeeOrders
                                from employeeOrder in employeeOrders.DefaultIfEmpty() // LEFT JOIN для связи с сотрудниками
                                join client in db.Client on order.ClientID equals client.ClientID into clientOrders
                                from clientOrder in clientOrders.DefaultIfEmpty() // LEFT JOIN для связи с клиентами
                                select new
                                {
                                    MaterialID = material.MaterialID,
                                    MaterialName = material.Name,
                                    Unit = material.Unit,
                                    UnitPrice = material.UnitPrice,

                                    EmployeeID = employeeOrder != null ? employeeOrder.EmployeeID : 0,
                                    EmployeeFullName = employeeOrder != null ? employeeOrder.FullName : "N/A",
                                    EmployeePhone = employeeOrder != null ? employeeOrder.Phone : "N/A",
                                    Position = employeeOrder != null ? employeeOrder.Position : "N/A",

                                    ClientID = clientOrder != null ? clientOrder.ClientID : 0,
                                    ClientFullName = clientOrder != null ? clientOrder.FullName : "N/A",
                                    ClientPhone = clientOrder != null ? clientOrder.Phone : "N/A",
                                    Email = clientOrder != null ? clientOrder.Email : "N/A"
                                }).ToList();

            dgUnifiedData.ItemsSource = combinedData;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // Сброс фильтров и возврат всех данных
            LoadData();
            txtFilter.Text = string.Empty;
            cbFilterColumn.SelectedIndex = -1;
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog pD = new PrintDialog();
                if (pD.ShowDialog()==true)
                {
                    pD.PrintVisual(dgUnifiedData, "Проект");
                }
            }
            finally
            {
                this.IsEnabled=true;
            }
        }
    }
}
