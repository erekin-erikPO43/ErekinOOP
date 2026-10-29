using System.Collections.Generic;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T item);
    void Delete(int id);
}

public interface IStorable
{
    string FilePath { get; }
    void Save();
}

public interface IExportable
{
    string FilePath { get; }
    string GetExportInfo();
}