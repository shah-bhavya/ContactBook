using AutoMapper;
using Contact.API.DTOs.Contact;
using Contact.API.DTOs.User;
using Contact.Core.Entities.Contacts;
using Contact.Core.Entities.Users;

namespace Contact.API.Helper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ContactEntity, ContactResponseDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ContactId)); // means you want to map from ContactEntity to ContactResponseDTO
            CreateMap<ContactPhoneEntity, ContactResponseDTO>()
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Number))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ContactId));
            CreateMap<ContactRequestDTO, ContactEntity>().ReverseMap();
            CreateMap<PhoneNumberDTO, PhoneNumbersEntity>()
                //.ForMember(d => d.PhoneNumberId, o => o.MapFrom(s => s.PhoneId))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.phone))
                .ForMember(d => d.ContactType, o => o.MapFrom(s => s.phoneType));
            CreateMap<RegisterDTO, UserEntity>();
        }
    }
}
