using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Ganz.Application.Utilities;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies.Identity;

namespace Ganz.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionUtility _encryptionUtility;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork,IEncryptionUtility encryptionUtility)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _encryptionUtility = encryptionUtility;
        }

        public async Task<LoginResponseDto> LoginAsync(string UserName, string Password)
        {
            var user = await _userRepository.SingleOrDefaultAsync(UserName);
            if (user == null) throw new Exception();

            var hashPassowrd = _encryptionUtility.GetSHA256(Password, user.PasswordSalt);
            if (user.Password != hashPassowrd) throw new Exception();

            var token = _encryptionUtility.GetNewToken(user.Id);
            var refreshToken = _encryptionUtility.GetNewRefreshToken();

            var response = new LoginResponseDto
            {
                UserName = user.UserName,
                Token = token,
                RefreshToken = refreshToken,
            };

            return response;
        }

        public async Task<string> RegisterAsync(string username, string password)
        {
            var salt = _encryptionUtility.GetNewSalt();
            var hashPassowrd = _encryptionUtility.GetSHA256(password, salt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Password = hashPassowrd,
                PasswordSalt = salt,
                Registerdate = DateTime.Now,
                UserName = username
            };

            await _userRepository.InsertUserAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return user.UserName;
        }

        public async Task<RefreshTokenResponse> GenerateNewToken(string Token, string RefreshToken)
        {
            var userRefreshToken = await _userRepository
            .SingleOrDefaultUserRefreshTokenAsync(RefreshToken);

            if (userRefreshToken == null) throw new Exception("RefreshToken Dosen't Exist!!!");

            var token = _encryptionUtility.GetNewToken(userRefreshToken.UserId);
            var refreshToken = _encryptionUtility.GetNewRefreshToken();


            var response = new RefreshTokenResponse
            {
                RefreshToken = refreshToken,
                Token = token
            };

            return response;
        }
    }
}
