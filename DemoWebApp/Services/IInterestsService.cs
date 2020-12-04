using DemoWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebApp.Services
{
    public interface IInterestsService
    {
        Task<string> GetContentForInterest(string title);
        Task<IEnumerable<Interest>> GetAllInterestsForUser(string userName);
        void Subscribe(string userName, Interest interest);
        void UnSubscribe(string userName, Interest interest);
    }
}
