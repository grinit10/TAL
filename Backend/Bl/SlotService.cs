using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal;
using Domain.ConfigModels;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.Extensions.Options;

namespace Bl
{
    public class SlotService: ISlotService
    {
        private readonly IJsonDal _jsonDal;
        private readonly SlotConfig _slotConfig;
        private readonly JsonDataSource _jsonDataSource;

        public SlotService(IOptions<SlotConfig> slotConfig, IOptions<JsonDataSource> jsonDataSource, IJsonDal jsonDal)
        {
            _jsonDal = jsonDal;
            _slotConfig = slotConfig.Value;
            _jsonDataSource = jsonDataSource.Value;
        }

        public async Task<List<TimeSlotModel>> GetSlots()
        {
            var slots = GenerateSlots();
            var bookedSlots = await _jsonDal.LoadJson<List<BookedSlot>>(_jsonDataSource.BookedSlots);
            slots.ForEach(slot =>
            {
                if(bookedSlots.Exists(b => b.StartTimeHour == slot.StartTime.Hour && b.StartTimeMinute == slot.StartTime.Minute))
                {
                    slot.IsAvailable = false;
                }
            });
            return slots;
        }

        private List<TimeSlotModel> GenerateSlots()
        {
            var slots = new List<TimeSlotModel>();
            var startTimeSpan = new TimeSpan(_slotConfig.StartSlotHour, _slotConfig.StartSlotMinute, 00);
            var endTimeSpan = new TimeSpan(_slotConfig.EndSlotHour, _slotConfig.EndSlotMinute, 00);
            var startTime = DateTime.Today.Add(startTimeSpan);
            var endTime = DateTime.Today.Add(endTimeSpan);

            while (startTime <= endTime.Add(new TimeSpan(0, -_slotConfig.SlotDurationInMinutes, 00)))
            {
                slots.Add(new TimeSlotModel
                {
                    IsAvailable = true,
                    StartTime = startTime
                });
                startTime = startTime.Add(new TimeSpan(0, _slotConfig.SlotDurationInMinutes, 00));
            }

            return slots;
        }
    }
}
