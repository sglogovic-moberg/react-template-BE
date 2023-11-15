using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReactAppBackend.Database;
using ReactAppBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactAppBackend.Handler.Login
{
    public class LoginCommand : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public LoginCommand(DatabaseContext context, IConfiguration iConfig)
        {
            _context = context;
            _configuration = iConfig;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Where(x => x.Email == request.Username)
                .Where(x => x.Password == request.Password)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentException("Username or password is wrong");
            }

            var tokenModel = GenerateToken(user.Id, user.Email, user.UserRole);

            return new LoginCommandResponse
            {
                Token = tokenModel.AccessToken!,
                UserRole = user.UserRole,
                UserId = user.Id
            };
        }

        public TokenModel GenerateToken(int userId, string username, UserRoleEnum role)
        {
            var claims = new List<Claim>
            {
                new("userId", userId.ToString()),
                // we are adding ClaimValueTypes.Integer so the frontend can know to which type to cast claim
                new("username", username),
                new("userRole", ((int)role).ToString())
            };

            var jwtOptions = _configuration.GetSection("JwtOptions");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.GetSection("SigningKey")!.Value!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var validTo = DateTime.UtcNow.AddMinutes(int.Parse(jwtOptions.GetSection("ExpirationSeconds")!.Value!));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtOptions.GetSection("Issuer")!.Value!,
                audience: jwtOptions.GetSection("Audience")!.Value!,
                claims: claims,
                expires: validTo,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new TokenModel
            {
                AccessToken = token,
            };
        }
    }
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public required string Username { get; set; }

        public required string Password { get; set; }

    }

    public class LoginCommandResponse
    {
        public required string Token { get; set; }

        public required UserRoleEnum UserRole { get; set; }

        public required int UserId { get; set; }
    }
}
