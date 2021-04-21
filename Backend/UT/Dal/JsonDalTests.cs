using System.Collections.Generic;
using System.Threading.Tasks;
using Dal;
using Domain.Models;
using NUnit.Framework;

namespace UT.Dal
{
    [TestFixture]
    public class JsonDalTests
    {
        private readonly IJsonDal _jsonDal;

        public JsonDalTests()
        {
            _jsonDal = new JsonDal();
        }

        [TestCase]
        public async Task ShouldReadJsonSuccessfully()
        {
            var result = await _jsonDal.LoadJson<List<BookedSlot>>("BookedSlots.json");
            Assert.NotNull(result);
            Assert.AreEqual(4, result.Count);
        }
    }
}