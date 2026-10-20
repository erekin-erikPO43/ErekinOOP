using System;

namespace WinFormsLinqApp
{
    public record OrderItem(
        int Id,
        string CustomerName,
        string ProductName,
        string Category,
        decimal Price,
        int Quantity
    )
    {
     
        public virtual bool Equals(OrderItem? other)
        {
            if (other is null) return false;
            return string.Equals(ProductName, other.ProductName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(ProductName ?? string.Empty);
        }
    }
}