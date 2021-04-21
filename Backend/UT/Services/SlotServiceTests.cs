using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bl;
using Dal;
using Domain.ConfigModels;
using Domain.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace UT.Services
{
    public class SlotServiceTests
    {
        private readonly ISlotService _slotService;
        private readonly int _startHour = 11;
        private readonly int _startMinute = 30;

        public SlotServiceTests()
        {
            var mockJsonDal = new Mock<IJsonDal>();
            mockJsonDal.Setup(mj => mj.LoadJson<List<BookedSlot>>(It.IsAny<string>())).Returns(Task.FromResult(new List<BookedSlot>
            {
                new()
                {
                    StartTimeHour = _startHour,
                    StartTimeMinute = _startMinute
                }
            }));

            var mockJsonDataSource = Options.Create(new JsonDataSource
            {
                BookedSlots = string.Empty
            });
            var mockSlotConfig = Options.Create(new SlotConfig
            {
                SlotDurationInMinutes = 30,
                StartSlotHour = 9,
                StartSlotMinute = 0,
                EndSlotHour = 17,
                EndSlotMinute = 0
            });
            _slotService = new SlotService(mockSlotConfig,  mockJsonDataSource, mockJsonDal.Object);
        }

        [TestCase]
        public async Task ShouldGetData()
        {
            var result = await _slotService.GetSlots();
            Assert.NotNull(result);
            Assert.AreEqual(16, result.Count);
            Assert.False(result.First(r => r.StartTime.Hour == _startHour && r.StartTime.Minute == _startMinute).IsAvailable);
        }
    }
}
