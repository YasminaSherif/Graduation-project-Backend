
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Graduation_project.Repository.Auth
{
    public class AuthenticationRepo:IAuthentication
    {
        private readonly IConfiguration _jwtOptions;
        private readonly ApplicationContext _db;

        public AuthenticationRepo(IConfiguration jwtOptions, ApplicationContext db)
        {
            _jwtOptions = jwtOptions;
            _db = db;
        }


        public async Task<AuthenticationResponseDTO> LoginAsync(LoginRequestDto requestDto)
        {
            var user =await _db.Users.SingleOrDefaultAsync(u => u.Email == requestDto.Email && u.Password == requestDto.Password);
            if (user == null)
               return new AuthenticationResponseDTO { Message = "Email or Password is not correct" };

            var token = AuthenticateUser(new AuthenticationRequest { UserName = user.UserName, Password = user.Password });
            return new AuthenticationResponseDTO()
            {
                Id=user.Id,
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Role = user.Role.ToString(),
                UserName = user.UserName,
                Token=token,
                Bios= user.Bios,
                City= user.City,
                Location = user.Location,
                ProfilePicture= user.ProfilePicture,
                Message="Logged in"
                
            };

        }

        public async Task<AuthenticationResponseDTO> RegisterAsync(RegisterRequestDTO requestDTO)
        {

            if (await _db.Users.SingleOrDefaultAsync(u => u.Email == requestDTO.Email) is not null)
                return new AuthenticationResponseDTO { Message = "Email is already Registerd" };

            if (await _db.Users.SingleOrDefaultAsync(u => u.UserName == requestDTO.UserName) is not null)
                return new AuthenticationResponseDTO { Message = "Username is taken" };

            byte[] profilePictureBytes = null;
            if (requestDTO.ProfilePicture != null)
            {
                using var stream = new MemoryStream();
                await requestDTO.ProfilePicture.CopyToAsync(stream);
                profilePictureBytes = stream.ToArray();
            }

            User user;
            if (requestDTO.Role==0)
            {
                user = new Customer()
                {

                    Bios = requestDTO.Bios,
                    Role = Roles.Customer,
                    UserName = requestDTO.UserName,
                    FirstName = requestDTO.FirstName,
                    LastName = requestDTO.LastName,
                    City = requestDTO.City,
                    Email=requestDTO.Email,
                    Location=requestDTO.Location,
                    Password= requestDTO.Password,
                    PhoneNumber= requestDTO.PhoneNumber,
                    ProfilePicture=profilePictureBytes,
                    };
            }
            else
            {
                user = new Worker()
                {
                    Bios = requestDTO.Bios,
                    Role = Roles.Worker,
                    UserName = requestDTO.UserName,
                    FirstName = requestDTO.FirstName,
                    LastName = requestDTO.LastName,
                    City = requestDTO.City,
                    Email = requestDTO.Email,
                    Location = requestDTO.Location,
                    Password = requestDTO.Password,
                    PhoneNumber = requestDTO.PhoneNumber,
                    ProfilePicture = profilePictureBytes,
                    CategoryId=requestDTO.CategoryId.Value,
                    
                };
            }

            var result = await _db.Users.AddAsync(user);
            _db.SaveChanges();

            var token = AuthenticateUser(new AuthenticationRequest { UserName = user.UserName, Password = user.Password });

            AuthenticationResponseDTO final= new AuthenticationResponseDTO()
            {
                Id=user.Id,
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Role = user.Role.ToString(),
                UserName = user.UserName,
                Token = token,
                Bios = user.Bios,
                City = user.City,
                Location = user.Location,
                ProfilePicture = user.ProfilePicture,
                Message = "Registerd"

            };


            return final;
        }

        public string AuthenticateUser(AuthenticationRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //object makes me create token 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions["Jwt:Issuer"],
                Audience = _jwtOptions["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions["Jwt:SigningKey"])), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new(ClaimTypes.NameIdentifier,request.UserName),
                        new(ClaimTypes.Email,"a@b.com")
                    }
                    )
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);//this is an token object
            var accessToken = tokenHandler.WriteToken(securityToken);//this transform the object into string token
            return accessToken;
        }

       
    }
}
