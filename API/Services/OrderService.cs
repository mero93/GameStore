using API.Data;
using API.Data.Entities;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly GameStoreDb _context;
        private readonly IMapper _autoMapper;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(GameStoreDb context, IMapper autoMapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _autoMapper = autoMapper;
            _userManager = userManager;
        }
        public async Task CreateOrderAsync(OrderModel newOrder)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == newOrder.AppUserId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(newOrder));
            }

            var order = _autoMapper.Map<Order>(newOrder);

            order.OrderDate = DateTime.Now;

            //var orderItems = new List<OrderItem>();

            //foreach (var item in newOrder.OrderItems)
            //{
            //    orderItems.Add(
            //        new OrderItem
            //        {
            //            OrderId = order.Id,
            //            Quantity = 2,
            //            GameId = 1,
            //        });
            //}

            //order.OrderItems = orderItems;

            _context.Add(order);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync(int appUserId)
        {
            return await _autoMapper.ProjectTo<OrderModel>(_context.Orders
                .Where(x => x.AppUserId == appUserId)).ToListAsync(); 
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            return await _autoMapper.ProjectTo<OrderModel>(_context.Orders)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
