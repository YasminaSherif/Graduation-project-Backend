using Graduation_project.Models;

namespace Graduation_project.Repository.Auth
{
    public interface IAuthentication
    {
        Task<AuthenticationResponseDTO> RegisterAsync(RegisterRequestDTO requestDTO);
        Task<AuthenticationResponseDTO> LoginAsync(LoginRequestDto requestDto);
      ///  Task<AuthenticationResponseDTO> ForgetPasswordAsync(ForgetPasswordModel model);
    }
}
