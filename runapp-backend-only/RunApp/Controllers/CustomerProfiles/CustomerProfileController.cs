using Contracts.Common;
using Contracts.CustomerProfile.Request;
using Contracts.Products.Responses;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunApp.Api.Mappers.CustomerProfiles;
using RunApp.Api.Mappers.Orders;
using RunApp.Api.Mappers.Products;
using RunApp.Api.Mappers.Reviews;
using RunApp.Api.Routes;
using RunApp.Api.Services;
using RunApp.Domain.UserAggregate;
using RunnApp.Application.CustomerProfiles.Commands.AddAddress;
using RunnApp.Application.CustomerProfiles.Commands.AddPaymentMethod;
using RunnApp.Application.CustomerProfiles.Commands.UpdateAccountInfo;
using RunnApp.Application.CustomerProfiles.Commands.UpdateAddress;
using RunnApp.Application.CustomerProfiles.Commands.UpdatePaymentMethod;
using RunnApp.Application.CustomerProfiles.Queries.GetSimpleBoughtProducts;
using RunnApp.Application.CustomerProfiles.Queries.GetUserAccountInfo;
using RunnApp.Application.CustomerProfiles.Queries.GetUserBoughtProducts;
using RunnApp.Application.CustomerProfiles.Queries.GetUserCreatedProducts;
using RunnApp.Application.CustomerProfiles.Queries.GetUserLikes;
using RunnApp.Application.CustomerProfiles.Queries.GetUserReviews;

namespace RunApp.Api.Controllers.CustomerProfiles
{
    public class CustomerProfileController(ISender mediator, UserManager<AppUser> userManager) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly UserManager<AppUser> _userManager = userManager;

        [Authorize]
        [HttpGet(ApiEndpoints.CustomerProfiles.GetUserReviews)]
        public async Task<IActionResult> GetReviews()
        {
            Guid userId = HttpContext.GetUserId();
            var userReviews = await _mediator.Send(new GetUserReviewsQuery(userId));

            return userReviews.MatchFirst(value => Ok(value.ReviewsToUserReviewsResponse()), Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.CustomerProfiles.GetUserLikes)]
        public async Task<IActionResult> GetLikes()
        {
            Guid userId = HttpContext.GetUserId();

            var userLikes = await _mediator.Send(new GetUserLikesQuery(userId));

            return Ok(userLikes.LikesDtoToLikesResponse());
        }

        [Authorize]
        [HttpGet(ApiEndpoints.CustomerProfiles.GetUserBoughtProducts)]
        public async Task<IActionResult> GetBoughtProducts()
        {
            Guid userId = HttpContext.GetUserId();
            var productWithImage = await _mediator.Send(new GetUserBoughtProductsQuery(userId));

            return productWithImage.MatchFirst(value => Ok(value.ProductsWithImageToProductsResponse()), Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.CustomerProfiles.GetBoughtProducts)]
        public async Task<IActionResult> GetSimpleBoughtProducts()
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _mediator.Send(new GetSimpleBoughtProductsQuery(userId));

            return result.MatchFirst(value => Ok(new SimpleBoughtProductsResponse(value.BoughtProducts, value.BoughtproductsWithReviews)), Problem);
        }

        [Authorize]
        [HttpPost(ApiEndpoints.CustomerProfiles.AddAddress)]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest addressRequest)
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new AddAddressCommand(userId, addressRequest.ZipCode, addressRequest.Address, addressRequest.City, addressRequest.Country, addressRequest.State));

            return result.Match(value => Ok(value.FromAddressToAddressResponse()), Problem);
        }

        [Authorize]
        [HttpPost(ApiEndpoints.CustomerProfiles.AddPaymenthMethod)]
        public async Task<IActionResult> AddPaymentMethod([FromBody] CardRequest cardRequest)
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new AddPaymentMethodCommand(userId, cardRequest.CardName, cardRequest.CardNumber, cardRequest.CVV, cardRequest.ExpiryDate));

            return result.MatchFirst(value => Ok(value.FromCardToCardDto()), Problem);
        }

        [Authorize]
        [HttpPut(ApiEndpoints.CustomerProfiles.UpdateAddress)]
        public async Task<IActionResult> UpdateAddres([FromBody] AddressRequest addressRequest)
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new UpdateAddressCommand(userId, addressRequest.ZipCode, addressRequest.Address, addressRequest.City, addressRequest.Country, addressRequest.State));

            return result.MatchFirst(value => Ok(value.FromAddressToAddressResponse()), Problem);
        }

        [Authorize]
        [HttpPut(ApiEndpoints.CustomerProfiles.UpdatePaymentMethod)]
        public async Task<IActionResult> UpdatePaymentMethod([FromBody] CardRequest cardRequest)
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new UpdatePaymentMethodCommand(userId, cardRequest.CardName, cardRequest.CardNumber, cardRequest.CVV, cardRequest.ExpiryDate));

            return result.MatchFirst(value => Ok(value.FromCardToCardDto()), Problem);
        }

        [Authorize]
        [HttpPut(ApiEndpoints.CustomerProfiles.UpdateAccountInfo)]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountInfoRequest accountInfo)
        {
            var user = await _userManager.FindByEmailAsync(accountInfo.OldEmail);
            if (user == null) return BadRequest("User was not found");

            var isUserNickNameTaken = await _userManager.Users.AnyAsync(x => x.NickName == accountInfo.NickName);
            if (isUserNickNameTaken) return BadRequest("User nickname was already registerd");

            var isUserEmailTaken = await _userManager.Users.AnyAsync(x => x.Email == accountInfo.NewEmail);
            if (isUserEmailTaken) return BadRequest("New user email was already registerd");

            user.Email = accountInfo.NewEmail;
            user.UserName = accountInfo.Name;
            user.NickName = accountInfo.NickName;
            var resultUpdate = await _userManager.UpdateAsync(user);
            if (!resultUpdate.Succeeded)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = 500,
                    Title = "An unexpected error happened",
                    Detail = string.Join(", ", resultUpdate.Errors.Select(x => x.Description))
                });
            }
            Guid userId = HttpContext.GetUserId();
            var result = await _mediator.Send(new UpdateAccountInfoCommand(userId, accountInfo.Name, accountInfo.NewEmail, accountInfo.NickName));
            return result.MatchFirst(value => Ok(value.FromCustomerToAccountResponse()), Problem);
        }

        [Authorize]
        [HttpGet(ApiEndpoints.CustomerProfiles.GetAccountInfo)]
        public async Task<IActionResult> GetAccountInformation()
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new GetUserAccountInfoQuery(userId));

            return result.MatchFirst(value => Ok(value.FromCustomerToUserInfo()), Problem);
        }

        [Authorize]
        [HttpPost(ApiEndpoints.CustomerProfiles.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordDtoRequest passwordDtoRequest)
        {
            var user = await _userManager.FindByEmailAsync(passwordDtoRequest.Email);
            if(user == null) return BadRequest("User was not found");

            var result = await _userManager.ChangePasswordAsync(user, passwordDtoRequest.OldPassword, passwordDtoRequest.NewPassword);

            if (result.Succeeded) return Ok();

            var errorDescriptions = result.Errors.Select(x => x.Description);

            return BadRequest(new ProblemDetails
            {
                Status = 500,
                Title = "An unexpected error happened",
                Detail = string.Join(", ", errorDescriptions)
            });
        }

        [Authorize]
        [HttpGet(ApiEndpoints.CustomerProfiles.GetCreatedProducts)]
        public async Task<IActionResult> GetCreatedProducts()
        {
            Guid userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new GetUserCreatedProductsQuery(userId));

            return result.Match(value => Ok(value.ProductsWithImageToCreatedProductsResponse()), Problem);
        }
    }
}
