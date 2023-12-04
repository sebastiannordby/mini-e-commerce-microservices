using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Library.Events.PurchaseService;
using PurchaseService.Library;
using System.ComponentModel.DataAnnotations;

namespace PurchaseService.API.Controllers
{
    public class PurchaseController : PurchaseServiceController
    {
        private readonly IBus _bus;

        public PurchaseController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(
            [FromBody] PaymentCommandDto command)
        {
            if (command is null)
                return BadRequest();

            if(command.Method == PaymentCommandDto.PaymentMethod.Card)
            {
                if (command.Card is null)
                    return BadRequest();

                await _bus.Publish(new OrderPurchasedEvent(command.OrderId));
                return Ok(string.Format(
                    "Successfully payed with card {0}. Thank you {1}",
                    command.Card.CardNumber,
                    command.Card.CardHolderFullName));
            }
            else if(command.Method == PaymentCommandDto.PaymentMethod.Vipps)
            {
                if (command.Vipps is null)
                    return BadRequest();


                await _bus.Publish(new OrderPurchasedEvent(command.OrderId));
                return Ok(string.Format(
                    "Successfully payed with Vipps {0}",
                    command.Vipps.PhoneNumber));
            }

            return BadRequest("Unknown payment method");
        }
    }
}
