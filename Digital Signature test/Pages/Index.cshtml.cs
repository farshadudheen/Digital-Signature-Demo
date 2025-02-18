using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace Digital_Signature_test.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMemoryCache _cache;
        public IndexModel(ILogger<IndexModel> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        public bool HasSignature { get; set; }
        public void OnGet()
        {
            var signature = _cache.Get<string>("UserSignature");
            HasSignature = !string.IsNullOrEmpty(signature);
        }
    }
}
