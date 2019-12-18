using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandPApis_GraphQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly OwnerConsumer _consumer;

        public ValuesController(OwnerConsumer consumer)
        {
            _consumer = consumer;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime currentWeekSunday = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
            DateTime firstDate = currentWeekMonday.AddDays(-28);
            DateTime endDate = currentWeekSunday.AddDays(14);

            var owners = await _consumer.GetAppointments(firstDate, endDate);
            return Ok(owners);
        }

       

    }
}


static class DateTimeExtensions
{    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7; return dt.AddDays(-1 * diff).Date;
    }
}