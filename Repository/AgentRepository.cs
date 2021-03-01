using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPI.Data;
using AgencyPI.Models;
using AgencyPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AgencyPI.Repository
{
    public class AgentRepository : IAgentRepository
    {

        private readonly ApplicationDbContext _context;

        public AgentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AgentExists(int agentId)
        {
            bool exists = _context.Agents.Any(x => x.Id == agentId);
            return exists;
        }

        public bool CreateAgent(Agent agent)
        {
            _context.Agents.Add(agent);
            return Save();
        }

        public bool DeleteAgent(Agent agent)
        {
            _context.Agents.Remove(agent);
            return Save();
        }

        public Agent GetAgent(int? agentId)
        {
            Agent agent = _context.Agents.Include(x => x.Orders).Include(x => x.Customers).FirstOrDefault(x => x.Id == agentId);
            return agent;
        }

        public List<Agent> GetAgentsByOrder(int orderId)
        {
            List<Agent> agents = _context.Agents.Where(a => a.Orders.Any(o => o.Id == orderId)).Include(x => x.Orders).ToList();

            return agents;

        }

        public List<Agent> GetAgents()
        {
            List<Agent> agents = _context.Agents.Include(x => x.Orders).Include(x => x.Customers).OrderBy(x => x.Name).ToList();
            return agents;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAgent(Agent agent)
        {
            _context.Agents.Update(agent);
            return Save();
        }
    }
}