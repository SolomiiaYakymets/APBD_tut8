namespace tutorial8.Models;

public class PageResult<T> where T : class
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<T> Data { get; set; } = [];
}