using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Digital_Signature_test
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignatureController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public SignatureController(IMemoryCache cache)
        {
            _cache = cache;
        }
        [HttpPost]
        public IActionResult SaveSignature([FromBody] SignatureData data)
        {
            try
            {
                _cache.Set("UserSignature", data.SignatureImage,
                    TimeSpan.FromHours(24)); // Cache for 24 hours

                return Ok(new { success = true, message = "Signature saved successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetSignature()
        {
            var signature = _cache.Get<string>("UserSignature");
            if (string.IsNullOrEmpty(signature))
            {
                return NotFound(new { success = false, message = "No signature found" });
            }
            return Ok(new { success = true, signature });
        }
    }
}
