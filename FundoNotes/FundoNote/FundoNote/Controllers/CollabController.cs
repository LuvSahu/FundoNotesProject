using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private readonly FundoContext fundoContext;

        private readonly ILogger<CollabController> _logger;


        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, FundoContext fundoContext, IDistributedCache distributedCache, ILogger<CollabController> _logger)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.fundoContext = fundoContext;
            this.distributedCache = distributedCache;
            this._logger = _logger;

        }
        [Authorize]
        [HttpPost("CreateCollab")]
        public IActionResult AddCollab(long noteid, string email)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "UserId").Value);
                var result = collabBL.AddCollab(noteid, userid, email);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Collaborator Added Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to add"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult Remove(long noteid, string email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = collabBL.Remove(noteid, email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborater Email deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collaborater Email not deleted" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        [Authorize]
        [HttpGet("ReadCollab")]
        public IActionResult ReadCollab(long colabId)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = collabBL.ReadCollab(colabId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "COLLABRATION EMAIL RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "COLLABRATION RECIEVED FAILED" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw;
            }
        }

        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = await fundoContext.CollabTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }
}
