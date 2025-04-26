using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        [HttpPost("Create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent()
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 5000, // 50.00 TL
                Currency = "try",
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }
}
