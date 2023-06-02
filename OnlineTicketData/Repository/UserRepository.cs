using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineTicketData.Db;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;
using OnlineTicketData.Repository.IRepostiory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineTicketData.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
      
        private string secretKey;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration,
           IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
         
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
         
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers
                .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());


          

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.Password);


            if (user == null || isValidPassword == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            //if user was found generate JWT Token
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role) }
                ),


                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
               // Role = roles.FirstOrDefault()

            };
            return loginResponseDTO;
        }



        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Password =BCrypt.Net.BCrypt.HashPassword(registerationRequestDTO.Password),
                Role = registerationRequestDTO.Role,
                Name = registerationRequestDTO.Name
            };

              _db.LocalUsers.Add(user);
                _db.SaveChanges();
            user.Password = "";
            return user;
        }




    }
}