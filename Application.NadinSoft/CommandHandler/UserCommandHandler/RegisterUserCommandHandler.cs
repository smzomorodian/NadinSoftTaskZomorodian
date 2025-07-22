using Application.NadinSoft.Command.User;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.CommandHandler.UserCommandHandler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly ICrudRepository<ApplicationUser> _crudRepository;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(ICrudRepository<ApplicationUser> crudRepository, IMapper mapper)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);

            await _crudRepository.Add(user);
            await _crudRepository.SaveChange();

            return user.Id.ToString();
        }
    }
}
