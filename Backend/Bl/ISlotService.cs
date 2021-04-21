using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ViewModels;

namespace Bl
{
    public interface ISlotService
    {
        Task<List<TimeSlotModel>> GetSlots();
    }
}
