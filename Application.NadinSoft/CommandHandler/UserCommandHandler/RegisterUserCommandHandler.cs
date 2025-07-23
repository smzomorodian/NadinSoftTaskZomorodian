using Application.NadinSoft.Command.User;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.NadinSoft.CommandHandler.UserCommandHandler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        
        public RegisterUserCommandHandler(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                // میتونی خطاها رو برگردونی یا Exception بندازی
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return user.Id.ToString();
        }
    }
}
