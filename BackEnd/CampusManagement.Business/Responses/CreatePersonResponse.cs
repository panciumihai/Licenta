namespace CampusManagement.Business.Responses
{
    public class CreatePersonResponse: BaseResponse
    {
        public Entities.Person Person { get; private set; }

        public CreatePersonResponse(bool success, string message, Entities.Person person) 
            : base(success, message)
        {
            Person = person;
        }
    }
}
