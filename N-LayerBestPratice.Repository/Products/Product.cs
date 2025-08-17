    using N_LayerBestPratice.Repository.Stores;

    namespace N_LayerBestPratice.Repository.Products;

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Store Store { get; set; } 
        public int StoreId{ get; set; } // Foreign key for Category
    }