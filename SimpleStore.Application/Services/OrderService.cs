using AutoMapper;
using SimpleStore.Application.Mapper;
using SimpleStore.Application.Models;
using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Application.Services
{
    public interface IOrderService
    {
        OrderModel GetOrderById(int orderId);
        List<OrderModel> GetOrderList();
        OrderModel CreateOrder(OrderModel orderModel);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IItemRepository itemRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IItemRepository itemRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.itemRepository = itemRepository;
        }

        private readonly IMapper mapper = ObjectMapper.Mapper;

        public OrderModel CreateOrder(OrderModel orderModel)
        {
            var order = mapper.Map<Order>(orderModel);

            foreach (var detail in order.Details)
            {
                detail.Item = itemRepository.Entities
                    .Single(e => e.ItemId == detail.Item.ItemId);
            }

            order.Timestamp = DateTime.UtcNow;
            order = orderRepository.Add(order);
            return mapper.Map<OrderModel>(order);
        }

        public List<OrderModel> GetOrderList()
        {
            var orders = orderRepository.Entities.ToList();
            return mapper.Map<List<OrderModel>>(orders);
        }

        public OrderModel GetOrderById(int orderId)
        {
            var order = orderRepository.Entities
                .Single(e => e.OrderId == orderId);
            return mapper.Map<OrderModel>(order);
        }
    }
}
