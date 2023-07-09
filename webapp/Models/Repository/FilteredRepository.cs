namespace TekoEmployeesMvc.Models;

public class FilteredRepository<TEntity> where TEntity : class
{
    internal Dictionary<string, List<TEntity>> filteredDbSet; 
    internal Dictionary<string, System.DateTime> datetimeDbSet; 
    private static System.Timers.Timer aTimer;

    public FilteredRepository()
    {
        this.filteredDbSet = new Dictionary<string, List<TEntity>>(); 
        this.datetimeDbSet = new Dictionary<string, System.DateTime>(); 
        SetTimer(); 
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(2000);
        
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }
    private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
    {
        foreach (var item in filteredDbSet)
        {
            // Delete unnecessary elements from dataset and datetime set 

            // e.SignalTime
            // datetimeDbSet[item.Key]
        }
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
            datetimeDbSet.Remove(uid); 
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
        datetimeDbSet.Add(uid, System.DateTime.Now); 
        filteredDbSet.Add(uid, entities.ToList());
        return uid; 
    }
}
