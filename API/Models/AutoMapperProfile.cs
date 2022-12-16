using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities;
using AutoMapper;

namespace API.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Game, GameModel>()
                .ForMember(gm => gm.Categories, opt => opt.MapFrom(g => g.Categories.Select(c => c.Category.Name)))
                .ForMember(gm => gm.CategoriesId, opt => opt.MapFrom(g => g.Categories.Select(c => c.CategoryId)))
                .ForMember(gm => gm.Publisher, opt => opt.MapFrom(g => g.Publisher.Name))
                .ForMember(gm => gm.PublisherId, opt => opt.MapFrom(g => g.Publisher.Id))
                .ReverseMap()
                //.ForMember(g => g.Categories, opt => opt.MapFrom(gm => gm.CategoriesId.Select(x =>
                //    new GameCategory { GameId = gm.Id, CategoryId = x })))
                .ForMember(g => g.Categories, opt => opt.Ignore())
                .ForMember(g => g.Publisher, opt => opt.Ignore());

            CreateMap<RegisterUserModel, AppUser>();

            CreateMap<AppUser, AppUserModel>()
                .ForMember(model => model.Roles, opt => opt.MapFrom(entity =>
                entity.UserRoles.Select(x => x.Role.Name)));

            CreateMap<Order, OrderModel>()
                .ForMember(om => om.OrderItems, o => o.MapFrom(o => o.OrderItems.Select(x =>
                    new OrderItemModel
                    {
                        OrderId = o.Id,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        GameId = x.GameId,
                        Game = x.Game.Name
                    })))
                .ReverseMap()
                .ForMember(o => o.AppUser, opt => opt.Ignore())
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.OrderItems, opt => opt.MapFrom(om => om.OrderItems.Select(x =>
                    new OrderItem
                    {
                        OrderId = (int)x.OrderId,
                        GameId = (int)x.GameId,
                        Quantity = x.Quantity,
                        Price = (decimal)x.Price
                    })));

            CreateMap<Discussion, DiscussionModel>()
                .ForMember(dm => dm.Username, opt => opt.MapFrom(d => d.AppUser.UserName))
                .ForMember(dm => dm.PhotoUrl, opt => opt.MapFrom(d => d.AppUser.PhotoUrl))
                .ReverseMap()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.AppUser, opt => opt.Ignore())
                .ForMember(d => d.Game, opt => opt.Ignore());

            CreateMap<Comment, CommentModel>()
                .ForMember(dm => dm.Username, opt => opt.MapFrom(d => d.AppUser.UserName))
                .ForMember(dm => dm.PhotoUrl, opt => opt.MapFrom(d => d.AppUser.PhotoUrl))
                .ForMember(dm => dm.Replies, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.AppUser, opt => opt.Ignore())
                .ForMember(d => d.Discussion, opt => opt.Ignore());

            CreateMap<Review, ReviewModel>()
                .ForMember(rm => rm.Username, opt => opt.MapFrom(r => r.AppUser.UserName))
                .ForMember(rm => rm.Game, opt => opt.MapFrom(r => r.Game.Name))
                .ReverseMap()
                .ForMember(r => r.AppUser, opt => opt.Ignore())
                .ForMember(r => r.Game, opt => opt.Ignore());

            CreateMap<Discussion, DiscussionDetailModel>()
                .ForMember(dm => dm.Username, opt => opt.MapFrom(d => d.AppUser.UserName))
                .ForMember(dm => dm.PhotoUrl, opt => opt.MapFrom(d => d.AppUser.PhotoUrl))
                .ForMember(ddm => ddm.Comments, opt => opt.
                MapFrom((d, x, y, ctx) => ctx.Mapper.Map<IEnumerable<CommentModel>>(d.Comments)));

            CreateMap<Publisher, PublisherModel>();

            CreateMap<Category, CategoryModel>();

            CreateMap<AppUser, AdditionalInfoModel>();
        }
    }
}