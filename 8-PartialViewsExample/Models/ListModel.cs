namespace PartialViewsExample.Models;

public class ListModel
{
    public string Title { get; set; }
    public IEnumerable<string> List { get; set; }
}