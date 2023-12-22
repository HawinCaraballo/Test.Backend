
namespace Test.Backend.Application.Behaviours.Response
{
    public class ValidationErrorResponse
    {
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
        public void Add(string propertyName, List<string> errorMessages)
        {
            if (!Errors.ContainsKey(propertyName))
            {
                Errors[propertyName] = new List<string>();
            }

            Errors[propertyName].AddRange(errorMessages);
        }
    }
}
