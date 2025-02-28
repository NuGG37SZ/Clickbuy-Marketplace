using ProductService.Entity;
using ProductService.HttpClient;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IUserClient _userClient;

        public ProductServiceImpl(IProductRepository productRepository, IUserClient userClient)
        {
            _productRepository = productRepository;
            _userClient = userClient;
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
