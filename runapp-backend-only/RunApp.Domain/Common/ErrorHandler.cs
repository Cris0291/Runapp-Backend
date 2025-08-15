using ErrorOr;

namespace RunApp.Domain.Common
{
    public abstract class ErrorHandler
    {
        private static List<Error> _errors = new();
        protected static List<Error> Errors
        {
            get
            {
                var temp = _errors.ToList();
                _errors.Clear();
                return temp;
            }
        }

        public static void AddError(Error error)
        {
            _errors.Add(error);
        }
        public static void AddError(IEnumerable<Error> errors)
        {
            _errors.AddRange(errors);
        }
        public static bool HasError()
        {
            return _errors.Any();
        }
    }
}
