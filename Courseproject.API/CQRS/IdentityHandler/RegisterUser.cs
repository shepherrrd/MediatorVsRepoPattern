using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courseproject.API.CQRS.IdentityHandler
{
    public class RegisterUser : IRequest<SuccessDto>
    {
        public User user {get; set;}
        
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUser, SuccessDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public RegisterUserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<SuccessDto> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var details = new IdentityUser
            {
                UserName = request.user.username,
                Email = request.user.email,
            };
            
            var user = await _userManager.CreateAsync(details,request.user.password);
            if (!user.Succeeded)
            {
                return new SuccessDto {
                    status = false,
                    message = "An Error Occured",
                    data = user.Errors
                    
                };
            }
            var dto = new SuccessDto
            {
                data = user
            };
            return dto;
        }
    }
}