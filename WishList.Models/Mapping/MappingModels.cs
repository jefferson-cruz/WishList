using AutoMapper;
using WishList.Domain.Entities;
using WishList.Models.Product;
using WishList.Services.Models.User;

namespace WishList.Mapping.Models
{
    public class MappingModels
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserModel>()
                .ForMember(x => x.Name, x => x.MapFrom(src => src.Name))
                .ForMember(x => x.Email, x => x.MapFrom(src => src.Email));


            cfg.CreateMap<Product, ProductModel>()
                .ForMember(x => x.Name, x => x.MapFrom(src => src.Name));
        }
    }
}
