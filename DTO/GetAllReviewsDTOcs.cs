namespace Graduation_project.DTO
{
    public class GetAllReviewsDTOcs:ResponseDto
    {
       public List<ReviewResponseDTO> reviews { get; set; } = new List<ReviewResponseDTO>();
    }
}
