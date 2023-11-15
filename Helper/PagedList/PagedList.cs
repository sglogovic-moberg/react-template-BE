namespace ReactAppBackend.Helpers;

public class PagedList<T>
{
    public PagedList(List<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public int TotalCount { get; set; }

    public List<T> Items { get; set; }
}