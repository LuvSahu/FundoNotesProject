using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class LabelController : ControllerBase
    {
       private readonly ILabelBL labelBL;

       private readonly IMemoryCache memoryCache;
       private readonly IDistributedCache distributedCache;

       private readonly FundoContext fundoContext;

       private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, FundoContext fundoContext, IDistributedCache distributedCache, ILogger<LabelController> _logger)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.fundoContext = fundoContext;
            this.distributedCache = distributedCache;
            this._logger = _logger;

        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabel(long noteid, string labelname)
        {
            try
            {
                long userid = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.AddLabel(userid, noteid, labelname);
                if (result != null)
                {
                    return Ok(new
                    {
                        Success = true,
                        message = "Label Added Sucessfully",
                        data = result
                    });
                }
                else
                {
                    return BadRequest(new
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
        [HttpGet("Read")]
        public IActionResult ReadLabel(long labelId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.ReadLabel(labelId, UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LABEL RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABEL RECIEVED FAILED" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateLabel(long labelId, string labelname)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.UpdateLabel(labelId, labelname);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LABEL UPDATE SUCCESSFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABEL UPDATE FAILED" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public ActionResult DeleteLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.DeleteLabel(labelId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label not deleted" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            


        }

        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = await fundoContext.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }

    }
}
