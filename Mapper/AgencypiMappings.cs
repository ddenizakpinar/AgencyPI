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
            CreateMap<Agent, AgentCreateDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerCreateDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();

            CreateMap<JsonPatchDocument<Agent>, JsonPatchDocument<AgentDto>>();
            CreateMap<Operation<Agent>, Operation<AgentDto>>();
        }
    }
}