namespace TekoEmployeesMvc.Models;

public class FilteredRecord<TEntity> where TEntity : class
{
    public IEnumerable<TEntity> Value { get; set; }
    public System.DateTime DateTimeCreated { get; set; }
    public bool IsReadyForDeleting { get; set; }
}