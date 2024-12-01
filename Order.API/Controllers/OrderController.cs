using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Data;
using Order.API.Dtos;
using Order.API.Models;
using Rebus.Bus;
using Shared.Events;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public OrderController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }


        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderModelDto orderModelDto, IBus rebus)
        {
            OrderModel order = new OrderModel()
            {
                ConsumerId = orderModelDto.ConsumerId,
                OrderItems = orderModelDto.OrderItems.Select(item => new OrderItemModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
                OrderStatus = Models.Enums.OrderStatus.InProgress,
                CreatedDate = DateTime.Now,
                TotalPrice  = orderModelDto.OrderItems.Sum(item => item.Price * item.Quantity),
            };

            await _appDbContext.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
          

            OrderStartedEvent orderStartedEvent = new(Guid.NewGuid())
            {
                OrderId = order.Id,
                ConsumerId = order.ConsumerId,
                TotalPrice = order.TotalPrice,
                OrderItems = orderModelDto.OrderItems.Select(item => new Shared.Models.OrderItemModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()     
            }; 

            await rebus.Send(orderStartedEvent);

            return Ok(order);
        }
    }
}
