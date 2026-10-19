using System;
using System.Collections.Generic;
using System.Linq;
public static class EmployeeQueryExtensions
{
    public static IEnumerable<Employee> FilterAndSort(IEnumerable<Employee> employees, string selectedDept, decimal minSalary, decimal maxSalary, string searchText, string sortCriterion, bool isAscending)
    {
        string searchLower = searchText?.ToLower() ?? string.Empty;
        var query = employees.Where(e =>
            (string.IsNullOrEmpty(selectedDept) || selectedDept == "Все отделы" || e.Department == selectedDept) &&
            (e.Salary >= minSalary && e.Salary <= maxSalary) &&
            (string.IsNullOrEmpty(searchLower) || e.FullName.ToLower().Contains(searchLower))
        );

        query = sortCriterion switch
        {
            "ФИО" => isAscending
                ? query.OrderBy(e => e.FullName)
                : query.OrderByDescending(e => e.FullName),

            "Оклад" => isAscending
                ? query.OrderBy(e => e.Salary)
                : query.OrderByDescending(e => e.Salary),

            "Отдел + Оклад (по убыванию)" => isAscending
                ? query.OrderBy(e => e.Department).ThenByDescending(e => e.Salary)
                : query.OrderByDescending(e => e.Department).ThenByDescending(e => e.Salary),

            "Должность + Дата приема" => isAscending
                ? query.OrderBy(e => e.Position).ThenBy(e => e.HireDate)
                : query.OrderByDescending(e => e.Position).ThenByDescending(e => e.HireDate),

            _ => query.OrderBy(e => e.Id)
        };

        return query;
    }
}