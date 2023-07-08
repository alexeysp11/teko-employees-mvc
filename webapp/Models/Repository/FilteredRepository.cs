namespace TekoEmployeesMvc.Models;

public class FilteredRepository<TEntity> where TEntity : class
{
    internal Dictionary<string, List<TEntity>> filteredDbSet; 

    public FilteredRepository()
    {
        this.filteredDbSet = new Dictionary<string, List<TEntity>>(); 
    }

    public virtual IEnumerable<TEntity> GetFiltered(string uid)
    {
        if (string.IsNullOrEmpty(uid)) throw new System.Exception("UID could not be null or empty"); 

        IEnumerable<TEntity> list = new List<TEntity>(); 
        if (filteredDbSet.ContainsKey(uid))
        {
            if (filteredDbSet[uid] != null)
                list = filteredDbSet[uid]; 
            filteredDbSet.Remove(uid); 
        }
        return list;
    }

    public virtual string InsertFiltered(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new System.Exception("List of entities could not be null"); 

        string uid = string.Empty; 
        do 
        {
            uid = System.Guid.NewGuid().ToString(); 
        } while (filteredDbSet.ContainsKey(uid)); 
        filteredDbSet.Add(uid, entities.ToList());
        return uid; 
    }
}
