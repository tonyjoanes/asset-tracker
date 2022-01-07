namespace Assets.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Assets.API.Entities;
    using Assets.API.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _repository;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IAssetRepository repository, ILogger<AssetsController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Asset>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            var assets = await _repository.GetAssets();
            return Ok(assets);
        }

        [HttpGet("{id:length(24)}", Name = "GetAsset")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> GetAssetById(string id)
        {
            var asset = await _repository.GetAsset(id);

            if (asset == null)
            {
                _logger.LogError($"Asset with id: {id}, not found.");
                return NotFound();
            }

            return Ok(asset);
        }

        [Route("[action]/{type}", Name = "GetAssetByType")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Asset>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetByType(string type)
        {
            var assets = await _repository.GetAssetByType(type);
            return Ok(assets);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> CreateAsset([FromBody] Asset asset)
        {
            await _repository.CreateAsset(asset);

            return CreatedAtRoute("GetAsset", new { id = asset.Id }, asset);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsset([FromBody] Asset asset)
        {
            return Ok(await _repository.UpdateAsset(asset));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteAsset")]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAssetById(string id)
        {
            return Ok(await _repository.DeleteAsset(id));
        }
    }
}
