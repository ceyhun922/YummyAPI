using AutoMapper;
using YummyAPI.DTOs.AboutDTO;
using YummyAPI.DTOs.CategoryDTO;
using YummyAPI.DTOs.ChefDTO;
using YummyAPI.DTOs.ContactDTO;
using YummyAPI.DTOs.FeatureDTO;
using YummyAPI.DTOs.FooterDTO;
using YummyAPI.DTOs.GalleryDTO;
using YummyAPI.DTOs.NotificationDTO;
using YummyAPI.DTOs.OrganizationDTO;
using YummyAPI.DTOs.ProductDTO;
using YummyAPI.DTOs.RezervationDTO;
using YummyAPI.DTOs.ServiceDTO;
using YummyAPI.DTOs.TestimonialDTO;
using YummyAPI.Entities;

namespace YummyAPI.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //category
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDTOs>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTOs>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            //product
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetByIdProductDto>().ReverseMap();
            CreateMap<Product,ResultGetAllProductWithCategoryDto>().ForMember(x=>x.CategoryName, y=>y.MapFrom(z=>z.Category.CategoryName)).ReverseMap();

            //chef
            CreateMap<Chef, CreateChefDto>().ReverseMap();
            CreateMap<Chef, ResultChefDto>().ReverseMap();
            CreateMap<Chef, UpdateChefDto>().ReverseMap();
            CreateMap<Chef, GetByIdChefDto>().ReverseMap();

            //service
            CreateMap<Service, ResultServiceDto>().ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();
            CreateMap<Service, UpdateService>().ReverseMap();
            CreateMap<Service, GetByIdServiceDto>().ReverseMap();

            //gallery
            CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
            CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
            CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();
            CreateMap<Gallery, GetByIdGalleryDto>().ReverseMap();

            //testimonial
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetByIdTestimonialDto>().ReverseMap();


            //orgabization
            CreateMap<Organization, ResultOrganizationDto>().ReverseMap();
            CreateMap<Organization, CreateOrganizationDto>().ReverseMap();
            CreateMap<Organization, UpdateOrganizationDto>().ReverseMap();
            CreateMap<Organization, GetByIdOrganizationDto>().ReverseMap();


            //footer
            CreateMap<Footer, ResultFooterDto>().ReverseMap();
            CreateMap<Footer, CreateFooterDto>().ReverseMap();
            CreateMap<Footer, UpdateFooterDto>().ReverseMap();

            //contact
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();

            //feature
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

            //Rezarvation
            CreateMap<Rezervation, ResultRezervationDto>().ReverseMap();
            CreateMap<Rezervation, CreateRezervationDto>().ReverseMap();
            CreateMap<Rezervation, UpdateRezervationDto>().ReverseMap();

            //About
            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetByIdAboutDto>().ReverseMap();

            //Notification

            CreateMap<Notification, ResultNotificationDto>().ReverseMap();
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
            CreateMap<Notification, ResultNotificationReadFalseDto>().ReverseMap();


        }
    }
}

