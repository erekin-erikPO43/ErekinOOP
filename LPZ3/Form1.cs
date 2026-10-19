using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace LPZ3
{
    public partial class Form1 : Form
    {
        private List<Employee> _allEmployees = new List<Employee>();
        private ComboBox cmbDepartment;
        private TextBox txtSearchName;
        private NumericUpDown numMinSalary;
        private NumericUpDown numMaxSalary;
        private ComboBox cmbSortCriterion;
        private RadioButton rdbAsc;
        private Button btnApply;
        private DataGridView dgvEmployees;
        private Label lblStatus;

        public Form1()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            this.Width = 800;
            this.Height = 600;

            cmbDepartment = new ComboBox() { Top = 20, Left = 20, Width = 150 };
            cmbDepartment.Items.AddRange(new object[] { "Все отделы", "ИТ", "Маркетинг", "Бухгалтерия", "Продажи" });
            cmbDepartment.SelectedIndex = 0;
            this.Controls.Add(cmbDepartment);

            txtSearchName = new TextBox() { Top = 20, Left = 180, Width = 150, Text = "" };
            this.Controls.Add(txtSearchName);

            numMinSalary = new NumericUpDown() { Top = 20, Left = 340, Width = 80, Maximum = 1000000 };
            this.Controls.Add(numMinSalary);

            // Сначала Maximum, потом Value, чтобы не было ошибки исключения
            numMaxSalary = new NumericUpDown() { Top = 20, Left = 430, Width = 80, Maximum = 1000000, Value = 100000 };
            this.Controls.Add(numMaxSalary);

            cmbSortCriterion = new ComboBox() { Top = 60, Left = 20, Width = 200 };
            cmbSortCriterion.Items.AddRange(new object[] { "ФИО", "Оклад", "Отдел + Оклад (по убыванию)", "Должность + Дата приема" });
            cmbSortCriterion.SelectedIndex = 0;
            this.Controls.Add(cmbSortCriterion);

            rdbAsc = new RadioButton() { Top = 60, Left = 240, Width = 120, Text = "По возрастанию", Checked = true };
            this.Controls.Add(rdbAsc);

            btnApply = new Button() { Top = 60, Left = 380, Width = 120, Text = "Применить" };
            btnApply.Click += btnApply_Click;
            this.Controls.Add(btnApply);

            dgvEmployees = new DataGridView() { Top = 100, Left = 20, Width = 740, Height = 400 };
            this.Controls.Add(dgvEmployees);

            lblStatus = new Label() { Top = 510, Left = 20, Width = 740, Text = "Статус: Готово" };
            this.Controls.Add(lblStatus);

            _allEmployees = new List<Employee>
        {
            new Employee(1, "Иванов Иван Иванович", "ИТ", "Разработчик", 150000, new DateTime(2022, 5, 10)),
            new Employee(2, "Петров Пётр Петрович", "Маркетинг", "Менеджер", 90000, new DateTime(2023, 1, 15)),
            new Employee(3, "Сидорова Анна Сергеевна", "ИТ", "Тестировщик", 120000, new DateTime(2021, 11, 20))
        };
        }

        private void btnApply_Click(object? sender, EventArgs e)
        {
            string selectedDept = cmbDepartment.SelectedItem?.ToString() ?? "Все отделы";
            string searchText = txtSearchName.Text.Trim();
            decimal minSalary = numMinSalary.Value;
            decimal maxSalary = numMaxSalary.Value;
            string sortCriterion = cmbSortCriterion.SelectedItem?.ToString() ?? "ФИО";
            bool isAscending = rdbAsc.Checked;

            var resultList = EmployeeQueryExtensions.FilterAndSort(
                _allEmployees, selectedDept, minSalary, maxSalary, searchText, sortCriterion, isAscending
            ).ToList();

            dgvEmployees.DataSource = resultList;

            int count = resultList.Count;
            decimal totalSalary = resultList.Sum(emp => emp.Salary);
            lblStatus.Text = $"Найдено сотрудников: {count} | Общий фонд з/п: {totalSalary:N2} руб.";
        }
    }
}