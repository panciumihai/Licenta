using AutoMapper;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Models;

namespace CampusManagement.Business
{
    public class PersonService : DetailsService<Entities.Person, PersonDetailsModel>, IPersonService
    {
        private IGenericRepository GenericRepository { get; }
        private IMapper Mapper { get; }

        public PersonService(IGenericRepository genericRepository, IMapper mapper)
            :base(genericRepository, mapper)
        {
            GenericRepository = genericRepository;
            Mapper = mapper;
        }

    }
}
