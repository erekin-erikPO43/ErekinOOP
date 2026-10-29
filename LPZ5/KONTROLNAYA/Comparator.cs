using System;
using System.Collections.Generic;
public class IgnoreCaseComparer : IEqualityComparer<string>
{
    public bool Equals(string x, string y) { if (x == null && y == null) return true; if (x == null || y == null) return false; return string.Equals(x, y, StringComparison.OrdinalIgnoreCase); }
    public int GetHashCode(string obj)
    {
        if (obj == null) return 0;
        return obj.ToLower().GetHashCode();
    }
}