namespace N_LayerBestPratice.Repository.UnitOfWork;

public interface IUnitOfWork
{
    //int ile etkilenen satır sayısını döndürür
    Task<int> SaveChangesAsync();
}