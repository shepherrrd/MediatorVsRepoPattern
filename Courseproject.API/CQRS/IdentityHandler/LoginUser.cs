
using AutoMapper.Configuration.Annotations;
using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Courseproject.API.CQRS.IdentityHandler;

public class LoginUser : IRequest<SuccessDto>
{
    public string Email { get; set; } 
    public string Password { get; set; }

}

public class LoginUserHandler : IRequestHandler<LoginUser, SuccessDto>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly GenerateToken _token;
    public LoginUserHandler(UserManager<IdentityUser> userManager, IConfiguration config, GenerateToken token)
    {
        _userManager = userManager;
        _token = token;
    }
    public async Task<SuccessDto> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        var Identityuser = await _userManager.FindByEmailAsync(request.Email);
        if (Identityuser == null)
        {
            return new SuccessDto { 
            
            status = false,
            message = "Email Or PassWord Is Incorrect"
            };
        }
        
        var pass = await _userManager.CheckPasswordAsync(Identityuser,request.Password);

        if (!pass)
        {
            return new SuccessDto
            {

                status = false,
                message = "Email Or PassWord Is Incorrect"
            };
        }
        var user = new User { 
        email = request.Email
        };
        var token = _token.generateTokens(user);


        return new SuccessDto { 
        
        data = new
        {
            token= token,
        }
        };
    }
}
