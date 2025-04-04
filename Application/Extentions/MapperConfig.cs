using Application.DTOs;
using Application.DTOs.AuthDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extentions
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {

            #region Product

            // Mapping for ProductDetailsDTO
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants))
                .ReverseMap();


            CreateMap<ProductVariant, ProductVariantDto>()
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ProductColor.Name))            
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages));

            CreateMap<ProductVariantSize, SizeDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Size.Name));

            CreateMap<ProductImage, ProductImageDto>();

            // Mapping for ProductCardDTO
            CreateMap<Product, ProductCardDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VariantId, opt => opt.MapFrom(src => src.ProductVariants.FirstOrDefault().Id))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ProductVariants.SelectMany(pv => pv.ProductImages).FirstOrDefault().URL)); // Get the first image URL
            #endregion

            #region Wishlist

            CreateMap<Wishlist, WishlistDto>();

            CreateMap<WishlistItem, WishlistItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.VariantId, opt => opt.MapFrom(src => src.Product.ProductVariants.FirstOrDefault().Id))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ProductVariants.SelectMany(pv => pv.ProductImages).FirstOrDefault().URL));
            //.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ProductVariants..FirstOrDefault().URL));

            #endregion

            #region Cart

            CreateMap<CartItem, CartItemDto>()
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductVariant.Product.Id))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
               .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ProductVariant.ProductColor.Name))
              .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size != null ? src.Size.Name : "Unknown")) // Handle null case
               .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductVariant.Product.Price))
               .ForMember(dest => dest.AvailableStock, opt => opt.MapFrom(src => src.ProductVariant.Quantity))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductVariant.ProductImages.FirstOrDefault().URL)) // Get the first image URL
                .ReverseMap();

            #endregion

            #region Order

            CreateMap<CreateOrderDto, Order>()
                 .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.CustomerAddress))
                 .ForMember(dest => dest.OrderItems, opt => opt.Ignore()) // We'll handle this separately
                 .ForMember(dest => dest.ShippingCost, opt => opt.Ignore()); // This comes from repository
        
            CreateMap<CustomerAddressDto, CustomerAddress>()
                .ReverseMap();

            CreateMap<OrderItemDto, OrderProductVariant>()
               .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType.ToString()))
                .ForMember(dest => dest.ShippingMethod, opt => opt.MapFrom(src => src.ShippingCost.ShippingMethod.Name))
                .ForMember(dest => dest.ShippingCost, opt => opt.MapFrom(src => src.ShippingCost.Price));

            CreateMap<Order, OrderSummaryDto>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));

            CreateMap<OrderProductVariant, OrderItemDto>();
            #endregion

            #region Users

            CreateMap<ICustomer, CustomerDto>() .ReverseMap();
        CreateMap<IAppUser, CustomerDto>() .ReverseMap();

            #endregion

            #region Shipping
            CreateMap<ShippingCost, ShippingOptionDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShippingMethod.Name))
           .ForMember(dest => dest.EstimatedDelivery, opt => opt.MapFrom(src => src.ShippingMethod.EstimatedDelivery));
            #endregion

        }


    }
}
