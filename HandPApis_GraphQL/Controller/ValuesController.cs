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
            var owners = await _consumer.GetAppointments();
            return Ok(owners);
        }
    }
}
