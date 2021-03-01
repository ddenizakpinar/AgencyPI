using AgencyPI.Models;
using AgencyPI.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace AgencyPI.Mapper
{
    public class AgencypiMappings : Profile
    {
        public AgencypiMappings()
        {
            CreateMap<Agent, AgentDto>().ReverseMap();
            CreateMap<AgentDto, Agent>().ReverseMap();
            CreateMap<Agent, AgentCreateUpdateDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerCreateUpdateDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateUpdateDto>().ReverseMap();
        }
    }
}