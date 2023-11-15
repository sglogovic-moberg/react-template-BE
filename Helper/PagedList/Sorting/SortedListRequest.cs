namespace ReactAppBackend.Helpers;

public class SortedListRequest : BaseListRequest
{
    public List<SortCriterion> SortCriteria { get; set; } = new();
}