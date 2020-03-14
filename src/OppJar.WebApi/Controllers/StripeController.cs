using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OppJar.Dto;
using Stripe;
using System.Threading.Tasks;

namespace OppJar.WebApi.Controllers
{
    public class StripeController : BaseApi
    {
        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            var customer = new CustomerCreateOptions
            {
                Email = dto.Email,
                Name = dto.Name
            };

            var custService = new CustomerService();

            return Ok(await custService.CreateAsync(customer));
        }

        [HttpPost("card/save-card")]
        public async Task<IActionResult> SaveCard([FromBody] SaveCardDto dto)
        {
            var options = new PaymentMethodAttachOptions
            {
                Customer = dto.CustomerId,
            };

            var service = new PaymentMethodService();

            return Ok(await service.AttachAsync(dto.PaymentMethodIds, options));
        }

        [HttpPost("payment/pay")]
        [AllowAnonymous]
        public async Task<IActionResult> Pay([FromBody] PayDto dto)
        {
            var paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = null;

            try
            {
                if (dto.ConfirmPaymentRequest.PaymentMethodId != null)
                {
                    var createOptions = new PaymentIntentCreateOptions
                    {
                        PaymentMethod = dto.ConfirmPaymentRequest.PaymentMethodId,
                        Amount = (int)(dto.Amount * 100),
                        Currency = "usd",
                        ConfirmationMethod = "manual",
                        Confirm = true
                    };

                    if (!string.IsNullOrEmpty(dto.ConfirmPaymentRequest.CustomerId))
                    {
                        createOptions.Customer = dto.ConfirmPaymentRequest.CustomerId;
                    }

                    paymentIntent = await paymentIntentService.CreateAsync(createOptions);
                }
                if (dto.ConfirmPaymentRequest.PaymentIntentId != null)
                {
                    var confirmOptions = new PaymentIntentConfirmOptions { };

                    paymentIntent = await paymentIntentService.ConfirmAsync(
                        dto.ConfirmPaymentRequest.PaymentIntentId,
                        confirmOptions
                    );
                }
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.StripeError.Message });
            }

            return GeneratePaymentResponse(paymentIntent);
        }

        private IActionResult GeneratePaymentResponse(PaymentIntent intent)
        {
            // Note that if your API version is before 2019-02-11, 'requires_action'
            // appears as 'requires_source_action'.
            if (intent.Status == "requires_action" &&
                intent.NextAction.Type == "use_stripe_sdk")
            {
                // Tell the client to handle the action
                return Ok(new
                {
                    requires_action = true,
                    payment_intent_client_secret = intent.ClientSecret
                });
            }
            else if (intent.Status == "succeeded")
            {
                // The payment didn’t need any additional actions and completed!
                // Handle post-payment fulfillment
                return Ok(new { success = true });
            }
            else
            {
                // Invalid status
                return StatusCode(500, new { error = "Invalid PaymentIntent status" });
            }
        }
    }
}
