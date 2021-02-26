using System.Collections.Generic;
using AgencyPI.Models;

namespace AgencyPI.Repository.IRepository
{
    public interface IAgentRepository
    {
        Agent GetAgent(int agentId);
        List<Agent> GetAgents();
        bool CreateAgent(Agent agent);
        bool UpdateAgent(Agent agent);
        bool DeleteAgent(Agent agent);
        List<Agent> GetAgentInOrder(int orderId);
        bool Save();
    }
}