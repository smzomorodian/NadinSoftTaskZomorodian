using Application.NadinSoft.Command.User;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.CommandHandler.UserCommandHandler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IGeTUserWhitByRepository _geTUserWhitByRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public LoginUserCommandHandler(IGeTUserWhitByRepository geTUserWhitByRepository, IMapper mapper, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _geTUserWhitByRepository = geTUserWhitByRepository;
            _mapper = mapper;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _geTUserWhitByRepository.GetByNationalCode(request.NationalCode);
            //var user = _mapper.Map<ApplicationUser>(request);
            if(user.Nationalcode != request.NationalCode)
            {
                return "کاربر یافت نشد";
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("NationalCode", user.Nationalcode),
                new Claim(ClaimTypes.Role, "User") // یا از UserManager نقش رو بخون
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

