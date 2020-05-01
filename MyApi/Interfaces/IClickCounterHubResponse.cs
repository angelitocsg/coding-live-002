using System.Threading.Tasks;
using MyApi.Entities;

namespace MyApi.Interfaces
{
    public interface IClickCounterHubResponse
    {
        Task SendClickCounters(Counter counter);
    }
}
