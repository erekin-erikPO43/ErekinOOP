using System;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double DiscountRate { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int CategoryId { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string CustomerName { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
    public string Channel { get; set; } = "";
}

public class SaleReportDto
{
    public string CustomerName { get; set; } = "";
    public string ProductName { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public double PriceWithDiscount { get; set; }
    public double TotalAmount { get; set; }
}

public class CategorySalesSummaryDto
{
    public string CategoryName { get; set; } = "";
    public int TotalDeals { get; set; }
    public double TotalRevenue { get; set; }
    public double AverageCheck { get; set; }
    public double MinPurchase { get; set; }
    public double MaxPurchase { get; set; }
}