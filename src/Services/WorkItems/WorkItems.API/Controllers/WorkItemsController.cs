namespace WorkItems.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WorkItems.API.Entities;
    using WorkItems.API.Repositories;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkItemsController : ControllerBase
    {
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemsController(IWorkItemRepository workItemRepository)
        {
            this._workItemRepository = workItemRepository;
        }

        [HttpGet("{assetName}", Name = "GetWorkItem")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(string assetName)
        {
            var workItem = await _workItemRepository.GetWorkItem(assetName);
            return Ok(workItem);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WorkItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkItem>> CreateWorkItem([FromBody] WorkItem workItem)
        {
            await _workItemRepository.CreateWorkItem(workItem);
            return CreatedAtRoute("GetWorkItem", new { assetName = workItem.AssetName }, workItem);
        }

        [HttpPut]
        [ProducesResponseType(typeof(WorkItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkItem>> UpdateWorkItem([FromBody] WorkItem workItem)
        {
            return Ok(await _workItemRepository.UpdateWorkItem(workItem));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(WorkItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteWorkItem(string assetName)
        {
            return Ok(await _workItemRepository.DeleteWorkItem(assetName));
        }
    }
}
