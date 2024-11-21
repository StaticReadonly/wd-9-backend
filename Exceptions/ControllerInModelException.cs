using FluentValidation.Results;

namespace WebApplication1.Exceptions
{
    public class ControllerInModelException : Exception
    {
        public Dictionary<string, string> Errors { get; set; }

        public ControllerInModelException(string field, string errorMessage) : base() 
        {
            Errors = new Dictionary<string, string>()
            {
                { field, errorMessage }
            };
        }

        public ControllerInModelException(ValidationResult result) : base()
        {
            Errors = new Dictionary<string, string>();

            foreach(var err in result.Errors)
            {
                Errors.Add(err.PropertyName, err.ErrorMessage);
            }
        }
    }
}
