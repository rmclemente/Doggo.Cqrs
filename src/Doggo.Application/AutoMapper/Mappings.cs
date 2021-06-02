using AutoMapper;
using Doggo.Application.Commands.Breeds;
using Doggo.Application.Dtos;
using Doggo.Domain.Entities;

namespace Doggo.Application.AutoMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Breed, BreedResponse>().ReverseMap();
            CreateMap<Breed, UpdateBreedCommand>()
                .ForMember(member => member.MessageType, opt => opt.Ignore())
                .ForMember(member => member.Timestamp, opt => opt.Ignore())
                .ForMember(member => member.AggregateId, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Breed, CreateBreedCommand>()
                .ForMember(member => member.MessageType, opt => opt.Ignore())
                .ForMember(member => member.Timestamp, opt => opt.Ignore())
                .ForMember(member => member.AggregateId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
