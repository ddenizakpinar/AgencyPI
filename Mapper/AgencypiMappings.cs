using AgencyPI.Models;
using AgencyPI.Models.Dto;
using AutoMapper;

namespace AgencyPI.Mapper
{
    public class AgencypiMappings : Profile
    {
        public AgencypiMappings()
        {
            CreateMap<Agent, AgentDto>().ReverseMap();
            CreateMap<Agent, AgentCreateDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}