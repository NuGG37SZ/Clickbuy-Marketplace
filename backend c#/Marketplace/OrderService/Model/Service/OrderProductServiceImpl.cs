using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.Model.Mapper;
using OrderService.Model.Repository;

namespace OrderService.Model.Service
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

            if (orderProduct != null)
                return OrderProductMapper.MapOrderProductToOrderProductDTO(orderProduct);

            return null;
        }

        public async Task<List<OrderProductDTO>> GetByOrderId(int orderId)
        {
            List<OrderProduct> orderProducts = await _orderProductRepository.GetByOrderId(orderId);

            return orderProducts
                    .Select(OrderProductMapper.MapOrderProductToOrderProductDTO)
                    .ToList();
        }

        public async Task<List<OrderProductDTO>> GetByUserId(int userId)
        {
            List<OrderProduct> orderProducts = await _orderProductRepository.GetByUserId(userId);

            return orderProducts
                    .Select(OrderProductMapper.MapOrderProductToOrderProductDTO)
                    .ToList();
        }

        public async Task Update(int id, OrderProductDTO orderProductDTO)
        {
            OrderProductDTO? currentProductDTO = await GetById(id);

            if (currentProductDTO != null)
            {
                await _orderProductRepository.Update(id,
                    OrderProductMapper.MapOrderProductDTOToOrderProduct(orderProductDTO)
                );
            }
        }
    }
}
