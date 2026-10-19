using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LPZ2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            using (var db = new WarehouseContext())
            {
                db.Database.CreateIfNotExists();
                if (!db.Categories.Any())
                {
                    var cat1 = new Category { Name = "Электроника" };
                    var cat2 = new Category { Name = "Бытовая техника" };
                    var cat3 = new Category { Name = "Аксессуары" };

                    db.Categories.AddRange(new[] { cat1, cat2, cat3 });
                    db.SaveChanges();

                    db.Products.AddRange(new[]
                    {
                        new Product { Name = "Смартфон", Price = 120000m, Quantity = 15, CategoryId = cat1.Id },
                        new Product { Name = "Ноутбук", Price = 350000m, Quantity = 5, CategoryId = cat1.Id },
                        new Product { Name = "Планшет", Price = 90000m, Quantity = 8, CategoryId = cat1.Id },
                        new Product { Name = "Холодильник", Price = 250000m, Quantity = 3, CategoryId = cat2.Id },
                        new Product { Name = "Микроволновка", Price = 45000m, Quantity = 10, CategoryId = cat2.Id },
                        new Product { Name = "Наушники", Price = 15000m, Quantity = 25, CategoryId = cat3.Id }
                    });
                    db.SaveChanges();
                }
            }

            LoadCategories();
            UpdateReport();
        }

        private void LoadCategories()
        {
            using (var db = new WarehouseContext())
            {
                var categories = db.Categories.ToList();
                categories.Insert(0, new Category { Id = 0, Name = "Все категории" });

                cmbCategories.DataSource = categories;
                cmbCategories.DisplayMember = "Name";
                cmbCategories.ValueMember = "Id";
            }
        }

        private List<ProductReportItem> GetProductReport(int selectedCategoryId, string searchText)
        {
            using (var dbContext = new WarehouseContext())
            {
                var reportQuery = from p in dbContext.Products
                                  join c in dbContext.Categories on p.CategoryId equals c.Id
                                  where (selectedCategoryId == 0 || c.Id == selectedCategoryId)
                                  where (string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText))
                                  select new ProductReportItem
                                  {
                                      ProductName = p.Name,
                                      CategoryName = c.Name,
                                      Price = p.Price,
                                      Quantity = p.Quantity,
                                      TotalValue = p.Price * p.Quantity
                                  };

                return reportQuery.ToList();
            }
        }

        private void UpdateReport()
        {
            int categoryId = Convert.ToInt32(cmbCategories.SelectedValue ?? 0);
            string searchText = txtSearch.Text.Trim();

            List<ProductReportItem> reportData = GetProductReport(categoryId, searchText);

            dgvReport.DataSource = reportData;

            decimal totalSum = reportData.Sum(x => x.TotalValue);
            lblTotalSum.Text = $"Общая стоимость склада: {totalSum:C}";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }
    }
}