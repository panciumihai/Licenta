namespace CampusManagement.Business.Responses
{
    public class CreatePersonResponse: BaseResponse
    {
        public Domain.Entities.Person Person { get; private set; }

        public CreatePersonResponse(bool success, string message, Domain.Entities.Person person) 
            : base(success, message)
        {
            Person = person;
        }
    }
}
