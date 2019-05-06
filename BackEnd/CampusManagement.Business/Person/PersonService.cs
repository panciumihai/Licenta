using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Person
{
    public class PersonService : DetailsService<Entities.Person, PersonDetailsModel>, IPersonService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public PersonService(IGenericRepository genericRepository, IMapper mapper)
            :base(genericRepository, mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Entities.Person> FindPersonByEmailAsync(string email)
        {
            var persons = await _genericRepository.FindAsync<Entities.Person>(x => x.Email == email, 
                "PersonRoles.Role");
             // ai ramas sa faci eager pe find
            // return persons.FirstOrDefault(p => p.Email == email);
            return persons.FirstOrDefault();
        }
    }
}
