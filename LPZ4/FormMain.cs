using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsLinqApp
{
    public partial class FormMain : Form
    {
        private readonly string connectionString = "Data Source=store.db;Version=3;";

        public FormMain()
        {
            InitializeComponent();

            
            btnCalcAggregates.Click += btnCalcAggregates_Click;
            btnExceptOnlineOnly.Click += btnExceptOnlineOnly_Click;
            btnIntersectBoth.Click += btnIntersectBoth_Click;
            btnUnionAllUnique.Click += btnUnionAllUnique_Click;

            EnsureDatabaseCreated();
        }

        private List<OrderItem> LoadOrdersFromDb(string tableName)
        {
            var orders = new List<OrderItem>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = $"SELECT Id, CustomerName, ProductName, Category, Price, Quantity FROM {tableName}";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new OrderItem(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            Convert.ToDecimal(reader.GetDouble(4)),
                            reader.GetInt32(5)
                        ));
                    }
                }
            }

            return orders;
        }

        private void btnCalcAggregates_Click(object sender, EventArgs e)
        {
            List<OrderItem> onlineOrders = LoadOrdersFromDb("OnlineOrders");
            dgvResults.DataSource = onlineOrders;

            if (!onlineOrders.Any()) return;

            int totalCount = onlineOrders.Count();
            int expensiveCount = onlineOrders.Count(x => (x.Price * x.Quantity) > 1000m);
            decimal totalRevenue = onlineOrders.Sum(x => x.Price * x.Quantity);
            decimal avgPrice = onlineOrders.Average(x => x.Price * x.Quantity);
            decimal minPrice = onlineOrders.Min(x => x.Price);
            decimal maxPrice = onlineOrders.Max(x => x.Price);

            string categoriesSummary = onlineOrders
                .Select(x => x.Category)
                .Distinct()
                .Aggregate("Категории: ", (current, next) => current + " | " + next);

            lblCount.Text = $"Count(): Всего заказов = {totalCount}, с чеком > 1000 руб. = {expensiveCount}";
            lblSum.Text = $"Sum(): Общая выручка = {totalRevenue:N2} руб.";
            lblAvg.Text = $"Average(): Средняя стоимость позиций = {avgPrice:N2} руб.";
            lblMinMax.Text = $"Min() / Max(): Мин. цена = {minPrice:N2} руб. | Макс. цена = {maxPrice:N2} руб.";
            lblAggregate.Text = $"Aggregate(): {categoriesSummary}";
        }

        private void btnExceptOnlineOnly_Click(object sender, EventArgs e)
        {
            var onlineOrders = LoadOrdersFromDb("OnlineOrders");
            var storeOrders = LoadOrdersFromDb("StoreOrders");

            var result = onlineOrders.Except(storeOrders).ToList();
            dgvResults.DataSource = result;
        }

        private void btnIntersectBoth_Click(object sender, EventArgs e)
        {
            var onlineOrders = LoadOrdersFromDb("OnlineOrders");
            var storeOrders = LoadOrdersFromDb("StoreOrders");

            var result = onlineOrders.Intersect(storeOrders).ToList();
            dgvResults.DataSource = result;
        }

        private void btnUnionAllUnique_Click(object sender, EventArgs e)
        {
            var onlineOrders = LoadOrdersFromDb("OnlineOrders");
            var storeOrders = LoadOrdersFromDb("StoreOrders");

            var result = onlineOrders.Union(storeOrders).ToList();
            dgvResults.DataSource = result;
        }

        private void EnsureDatabaseCreated()
        {
            if (!System.IO.File.Exists("store.db"))
            {
                SQLiteConnection.CreateFile("store.db");
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string createTablesSql = @"
                        CREATE TABLE OnlineOrders (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            CustomerName TEXT,
                            ProductName TEXT,
                            Category TEXT,
                            Price REAL,
                            Quantity INTEGER
                        );
                        CREATE TABLE StoreOrders (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            CustomerName TEXT,
                            ProductName TEXT,
                            Category TEXT,
                            Price REAL,
                            Quantity INTEGER
                        );

                        INSERT INTO OnlineOrders (CustomerName, ProductName, Category, Price, Quantity) VALUES
                        ('Иван Иванов', 'Ноутбук', 'Электроника', 45000, 1),
                        ('Алексей Смирнов', 'Мышь', 'Аксессуары', 1200, 2),
                        ('Мария Новикова', 'Клавиатура', 'Аксессуары', 3500, 1),
                        ('Елена Морозова', 'Монитор', 'Электроника', 15000, 1);

                        INSERT INTO StoreOrders (CustomerName, ProductName, Category, Price, Quantity) VALUES
                        ('Олег Петров', 'Мышь', 'Аксессуары', 1200, 1),
                        ('Дмитрий Сидоров', 'Наушники', 'Аудио', 2500, 1),
                        ('Ольга Васильева', 'Монитор', 'Электроника', 15000, 2);
                    ";

                    using (var cmd = new SQLiteCommand(createTablesSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}