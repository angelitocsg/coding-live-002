using System.Threading.Tasks;

namespace MyApi.Interfaces
{
    public interface IClickCounterHubRequest
    {
        Task LikeClick(string who);
        Task HeartClick(string who);
        Task HahaClick(string who);
        Task TongueClick(string who);
    }
}