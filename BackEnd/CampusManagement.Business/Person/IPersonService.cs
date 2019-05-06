using System.Threading.Tasks;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Person
{
    public interface IPersonService : IDetailsService<PersonDetailsModel>
    {
        Task<Entities.Person> FindPersonByEmailAsync(string email);
    }
}
