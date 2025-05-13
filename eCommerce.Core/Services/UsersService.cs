using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContract;
using eCommerce.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersService(IUserRepository userRepository, IMapper mapper) 
        { 
           _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
        {
            ApplicationUser user= await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<AuthenticationResponse?>(user) with { Success=true,Token="token"};
            //return new AuthenticationResponse(user.UserId, user.Email, user.PersonName, user.Gender, "token", Success: true);
        }

        public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
        {
            ApplicationUser user=_mapper.Map<ApplicationUser?>(registerRequest);
            //ApplicationUser user = new ApplicationUser()
            //{
            //    PersonName = registerRequest.PersonName,
            //    Email = registerRequest.Email,
            //    Password = registerRequest.Password,
            //    Gender = registerRequest.Gender.ToString(),
            //};
            ApplicationUser? registeredUser=await _userRepository.AddUser(user);
            if (registeredUser == null)
            {
                return null;
            }
            return _mapper.Map<AuthenticationResponse?>(user) with { Success = true, Token = "token" };
            //return new AuthenticationResponse(registeredUser.UserId,registeredUser.Email,registeredUser.PersonName,registeredUser.Gender,"token", true);
        }
    }
}
