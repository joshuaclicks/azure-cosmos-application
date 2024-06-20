namespace Project.Application.Constants
{
    public class ErrorMessages
    {
        public const string DEFAULT_VALIDATION_MESSAGE = "Sorry, you have supplied one or more wrong inputs. Kindly check your input and try again.";

        public const string SERVER_ERROR = "Sorry, we are unable to fulfill your request at the moment, kindly try again later.";
        public const string CONFLICT_ERROR = "Sorry, there seems to be a request conflict, kindly check your input and try again.";
        public const string DATABASE_CONFLICT_ERROR = "One or more unique fields already exist, kindly try again later.";
        public const string NOT_FOUND_ERROR = "Sorry, the resource you have requested for is not available at the moment.";
        public const string PROGRAM_TYPE_ALREADY_EXIST = "Program with the title already exist.";
        public const string QUESTION_TYPE_ALREADY_EXIST = "Question type already exist, kindly check your input and try again";
        public const string PROGRAM_NOT_FOUND = "Sorry, the program you have selected is invalid. Kindly check and try again later";
        public const string QUESTION_TYPES_INVALID = "Sorry, one or more question types is invalid. Kindly check and try again later.";
        public const string QUESTION_TYPES_NOT_FOUND = "Sorry, there are not question types configured yet. Kindly contact the administrator for further assistance.";
        public const string QUESTIONS_INVALID = "One or more question is invalid.";
        public const string RESPONSE_QUESTIONS_INVALID = "One or more question you attempted is invalid.";

        public const string RESIDENTIAL_COUNTRY_INVALID = "Your residential country is invalid.";
        public const string NATIONALITY_INVALID = "Your nationality is invalid.";
    }
}
