using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using MiniECommece.APIUtilities;
using ProductService.Domain.UseCases.Queries.Find;
using MiniECommerce.Library;
using System.Security.Claims;

namespace ProductService.API.Controllers
{
    public class ProductViewController : ProductServiceController
    {
        private readonly IMediator _mediator;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public ProductViewController(
            IMediator mediator, 
            RabbitMQPublisher rabbitMQPublisher)
        {
            _mediator = mediator;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductView>> ListViews()
        {
            var res = await _mediator.Send(
                new ListProductViewsQuery());

            _rabbitMQPublisher.PublishMessage(
                "myExchange", "product-request", "Item added to basket");

            return res;
        }

        [HttpGet("id/{productId}")]
        public async Task<ProductView?> ListViews([FromRoute] Guid productId)
            => await _mediator.Send(
                new FindProductViewByIdQuery(productId));

    }
}
