namespace N_LayerBestPratice.Repository.Abstract;

public interface IAuditEntity
{
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    
    
    // ilerde authentication eklendiginde user ıd lerini de tutabiliriz
    // public string CreatedBy { get; set; }
}