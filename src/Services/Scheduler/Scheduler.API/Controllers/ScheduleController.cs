namespace Scheduler.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Scheduler.API.Entities;
    using Scheduler.API.Repositories;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _repository;

        public ScheduleController(IScheduleRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet("{assetName}", Name = "GetSchedule")]
        [ProducesResponseType(typeof(Schedule), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Schedule>> GetSchedule(string assetName)
        {
            var schedule = await _repository.GetSchedule(assetName);

            return Ok(schedule ?? new Schedule(assetName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Schedule), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Schedule>> UpdateSchedule([FromBody] Schedule schedule)
        {
            return Ok(await _repository.UpdateSchedule(schedule));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSchedule(string assetName)
        {
            await _repository.DeleteSchedule(assetName);
            return Ok();
        }
    }
}
