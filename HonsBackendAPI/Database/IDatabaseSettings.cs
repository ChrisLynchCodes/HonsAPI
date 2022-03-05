namespace HonsBackendAPI.Database
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; } 
        public string CustomersCollectionName { get; set; } 
        public string ProductsCollectionName { get; set; } 
        public string OrdersCollectionName { get; set; }
        public string OrderLinesCollectionName { get; set; }
        public string CategoriesCollectionName { get; set; }
        public string AdminsCollectionName { get; set; } 
        public string ReviewsCollectionName { get; set; } 
        public string AddressesCollectionName { get; set; }
        public string BasketsCollectionName { get; set; }
    }
}