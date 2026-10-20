using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Forms;

namespace VehicleFleetApp
{
    public partial class FormMain : Form
    {
     
        private List<Vehicle> fleet = new List<Vehicle>();

      
        private ComboBox cmbVehicleType;
        private TextBox txtModel;
        private NumericUpDown numPower;
        private CheckBox chklsElectric; 
        private NumericUpDown numCapacity; 
        private Button btnAdd;
        private ListBox lstFleet;
        private Button btnCalculateTax;
        private Button btnSaveStorable;
        private Label lblTotalTax;

        public FormMain()
        {
            InitializeComponentCustom();
        }

      
        private void InitializeComponentCustom()
        {
            this.Text = "Система учета автопарка (Vehicle Fleet Management)";
            this.Width = 650;
            this.Height = 450;

           
            cmbVehicleType = new ComboBox() { Left = 20, Top = 20, Width = 150 };
            cmbVehicleType.Items.AddRange(new string[] { "Легковой", "Грузовой" });
            cmbVehicleType.SelectedIndex = 0;
            cmbVehicleType.SelectedIndexChanged += CmbVehicleType_SelectedIndexChanged;
            this.Controls.Add(cmbVehicleType);

            
            txtModel = new TextBox() { Left = 180, Top = 20, Width = 120, Text = "Модель" };
            this.Controls.Add(txtModel);

            numPower = new NumericUpDown() { Left = 310, Top = 20, Width = 80, Minimum = 1, Maximum = 2000, Value = 150 };
            this.Controls.Add(numPower);

           
            chklsElectric = new CheckBox() { Left = 400, Top = 22, Width = 100, Text = "Электро" };
            this.Controls.Add(chklsElectric);

            numCapacity = new NumericUpDown() { Left = 400, Top = 22, Width = 100, Minimum = 0.5m, Maximum = 100m, Value = 5m, DecimalPlaces = 1 };
            numCapacity.Visible = false; 
            this.Controls.Add(numCapacity);

            
            btnAdd = new Button() { Left = 510, Top = 18, Width = 100, Text = "Добавить" };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

         
            lstFleet = new ListBox() { Left = 20, Top = 60, Width = 590, Height = 220 };
            this.Controls.Add(lstFleet);

           
            btnCalculateTax = new Button() { Left = 20, Top = 295, Width = 180, Text = "Рассчитать налог" };
            btnCalculateTax.Click += BtnCalculateTax_Click;
            this.Controls.Add(btnCalculateTax);

            lblTotalTax = new Label() { Left = 210, Top = 300, Width = 400, Text = "Суммарный налог: 0 руб." };
            this.Controls.Add(lblTotalTax);

           
            btnSaveStorable = new Button() { Left = 20, Top = 335, Width = 250, Text = "Сохранить Storable объекты" };
            btnSaveStorable.Click += BtnSaveStorable_Click;
            this.Controls.Add(btnSaveStorable);
        }

        
        private void CmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCar = cmbVehicleType.SelectedIndex == 0;
            chklsElectric.Visible = isCar;
            numCapacity.Visible = !isCar;
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string model = txtModel.Text;
                int power = (int)numPower.Value;

                Vehicle vehicle;
                if (cmbVehicleType.SelectedIndex == 0)
                {
                    bool isElectric = chklsElectric.Checked;
                    vehicle = new Car(model, power, isElectric);
                }
                else
                {
                    double capacity = (double)numCapacity.Value;
                    vehicle = new Truck(model, power, capacity);
                }

                fleet.Add(vehicle);
                lstFleet.Items.Add(vehicle.GetInfo());
                MessageBox.Show("Транспортное средство успешно добавлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания объекта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void BtnCalculateTax_Click(object sender, EventArgs e)
        {
            double totalTax = 0;
            foreach (var v in fleet)
            {
                
                totalTax += v.CalculateTax();
            }
            lblTotalTax.Text = $"Суммарный налог по автопарку: {totalTax} руб.";
        }

        
        private void BtnSaveStorable_Click(object sender, EventArgs e)
        {
            int savedCount = 0;
            foreach (var v in fleet)
            {
              
                if (v is IStorable storable)
                {
                    storable.Save(storable.FilePath);
                    savedCount++;
                }
            }
            MessageBox.Show($"Успешно сохранено объектов в файл: {savedCount}", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
