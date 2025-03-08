using OrderService.DTO;
using OrderService.Entity;
using OrderService.Mapper;
using OrderService.Repository;

namespace OrderService.Service
{
    public class OrderProductServiceImpl : IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;

        public OrderProductServiceImpl(IOrderProductRepository orderProductRepository) => 
            _orderProductRepository = orderProductRepository;

        public async Task Create(OrderProductDTO orderProductDTO)
        {
            await _orderProductRepository.Create(OrderProductMapper
                .MapOrderProductDTOToOrderProduct(orderProductDTO)
            );
        }

        public async Task DeleteById(int id)
        {
            await _orderProductRepository.DeleteById(id);
        }

        public async Task<List<OrderProductDTO>> GetAll()
        {
            List<OrderProduct> orderProducts = await _orderProductRepository.GetAll();

            return orderProducts
                        .Select(OrderProductMapper.MapOrderProductToOrderProductDTO)
                        .ToList();
        }

        public async Task<OrderProductDTO?> GetById(int id)
        {
            OrderProduct? orderProduct = await _orderProductRepository.GetById(id);

            if(orderProduct != null) 
                return OrderProductMapper.MapOrderProductToOrderProductDTO(orderProduct);

            return null;
        }

        public async Task Update(int id, OrderProductDTO orderProductDTO)
        {
            await _orderProductRepository.Update(id, 
                OrderProductMapper.MapOrderProductDTOToOrderProduct(orderProductDTO)
            );
        }
    }
}
