using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
public class ReportService
{
    private const string ConnectionString = "Data Source=enterprise.db"; public List<Category> Categories { get; private set; } = new List<Category>(); public List<Product> Products { get; private set; } = new List<Product>(); public List<Sale> Sales { get; private set; } = new List<Sale>();
    public void LoadDataFromDatabase()
    {
        Categories.Clear();
        Products.Clear();
        Sales.Clear();

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand("SELECT Id, Name, DiscountRate FROM Categories", connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    Categories.Add(new Category { Id = reader.GetInt32(0), Name = reader.GetString(1), DiscountRate = reader.GetDouble(2) });
            }

            using (var cmd = new SQLiteCommand("SELECT Id, Name, CategoryId, Price, Stock FROM Products", connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    Products.Add(new Product { Id = reader.GetInt32(0), Name = reader.GetString(1), CategoryId = reader.GetInt32(2), Price = reader.GetDouble(3), Stock = reader.GetInt32(4) });
            }

            using (var cmd = new SQLiteCommand("SELECT Id, ProductId, CustomerName, Quantity, SaleDate, Channel FROM Sales", connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    Sales.Add(new Sale { Id = reader.GetInt32(0), ProductId = reader.GetInt32(1), CustomerName = reader.GetString(2), Quantity = reader.GetInt32(3), SaleDate = DateTime.Parse(reader.GetString(4)), Channel = reader.GetString(5) });
            }
        }
    }

    public List<SaleReportDto> GetSaleReports()
    {
        return Sales
            .Join(Products, s => s.ProductId, p => p.Id, (s, p) => new { s, p })
            .Join(Categories, sp => sp.p.CategoryId, c => c.Id, (sp, c) => new SaleReportDto
            {
                CustomerName = sp.s.CustomerName,
                ProductName = sp.p.Name,
                CategoryName = c.Name,
                PriceWithDiscount = sp.p.Price * (1 - c.DiscountRate),
                TotalAmount = (sp.p.Price * (1 - c.DiscountRate)) * sp.s.Quantity
            }).ToList();
    }

    public List<SaleReportDto> GetFilteredSales(string categoryFilter, int pageNumber, int pageSize, out int totalRecords)
    {
        var query = GetSaleReports().AsEnumerable();
        if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "Все")
            query = query.Where(r => r.CategoryName == categoryFilter);

        totalRecords = query.Count();
        return query.OrderBy(r => r.CategoryName).ThenByDescending(r => r.TotalAmount).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }

    public List<CategorySalesSummaryDto> GetCategorySalesSummaries()
    {
        return GetSaleReports()
            .GroupBy(r => r.CategoryName).Select(g => new CategorySalesSummaryDto
            {
                CategoryName = g.Key,
                TotalDeals = g.Count(),
                TotalRevenue = g.Sum(r => r.TotalAmount),
                AverageCheck = g.Average(r => r.TotalAmount),
                MinPurchase = g.Min(r => r.TotalAmount),
                MaxPurchase = g.Max(r => r.TotalAmount)
            }).ToList();
    }

    public string GetGlobalAuditSummary()
    {
        var summaries = GetCategorySalesSummaries();
        if (!summaries.Any()) return "Нет данных.";
        return summaries.Aggregate("АУДИТ:\n", (c, n) => c + n.CategoryName + ": " + n.TotalRevenue.ToString("N2") + "\n", f => f + "ИТОГО: " + summaries.Sum(s => s.TotalRevenue).ToString("N2"));
    }

    public List<Product> GetOnlineOnlyProducts()
    {
        var online = Sales.Where(s => s.Channel == "Online").Select(s => s.ProductId).Distinct();
        var retail = Sales.Where(s => s.Channel == "Retail").Select(s => s.ProductId).Distinct();
        return Products.Where(p => online.Except(retail).Contains(p.Id)).ToList();
    }

    public List<Product> GetPopularBothChannelsProducts()
    {
        var online = Sales.Where(s => s.Channel == "Online").Select(s => s.ProductId).Distinct();
        var retail = Sales.Where(s => s.Channel == "Retail").Select(s => s.ProductId).Distinct();
        return Products.Where(p => online.Intersect(retail).Contains(p.Id)).ToList();
    }

    public List<Product> GetAllSoldProductsUnique()
    {
        return Products.Where(p => Sales.Select(s => s.ProductId).Contains(p.Id)).ToList();
    }
}