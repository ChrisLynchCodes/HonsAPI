namespace HonsBackendAPI.Database
{
    public class DatabaseSettings : IDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CustomersCollectionName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
        public string OrdersCollectionName { get; set; } = null!;
        public string OrderLinesCollectionName { get; set; } = null!;
        public string CategoriesCollectionName { get; set; } = null!;
        public string AdminsCollectionName { get; set; } = null!;
        public string ReviewsCollectionName { get; set; } = null!;
        public string AddressesCollectionName { get; set; } = null!;
        public string BasketsCollectionName { get; set; } = null!;
    }
}
