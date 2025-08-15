using Contracts.StoreOwnerProfiles.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunApp.Api.Mappers.StoreOwnerProfiles;
using RunApp.Api.Routes;
using RunApp.Api.Services;
using RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile;

namespace RunApp.Api.Controllers.StoreOwnerProfiles
{
   
    public class StoreOwnerProfileController(ISender sender, IJwtServiceGenerator jwtServiceGenerator) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IJwtServiceGenerator _jwtServiceGenerator = jwtServiceGenerator;

        [Authorize]
        [HttpPost(ApiEndpoints.StoreOwnerProfiles.CreateStoreOwnerProfile)]
        public async Task<IActionResult> CreateStoreOwnerProfile(StoreOwnerProfileRequest storeOwnerProfileRequest)
        {
            var userId = HttpContext.GetUserId();
            var storeOwnerCommand = new CreateStoreOwnerProfileCommand(userId, storeOwnerProfileRequest.StoreName, 
                storeOwnerProfileRequest.AddressRequest.FromAdressRequestToAdressCommand(), storeOwnerProfileRequest.CardRequest.FromCardRequestToCardCommand(), 
                storeOwnerProfileRequest.InitialInvestment);
            var profileResult = await _sender.Send(storeOwnerCommand);


            return profileResult.Match(value =>
            {
               var token =  _jwtServiceGenerator.GenerateJwtToken(storeProfile: value, customClaims: storeOwnerProfileRequest.Claims);
               var response =  value.FromProfileRequestToProfileResponse(token);
                return Ok(response);
               
            } , Problem);
        }
    }
}
