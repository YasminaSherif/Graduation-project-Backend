namespace Graduation_project.Models
{
    public class Customer:User
    {
        public virtual ICollection<CustomerRequest> CustomerRequest { get; set; } = new List<CustomerRequest>();
    }
}
