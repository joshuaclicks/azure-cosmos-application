using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Constants
{
    public class ErrorMessages
    {
        public const string DEFAULT_VALIDATION_MESSAGE = "Sorry, you have supplied one or more wrong inputs. Kindly check your input and try again.";

        public const string SERVER_ERROR = "Sorry, we are unable to fulfill your request at the moment, kindly try again later.";
        public const string CONFLICT_ERROR = "Sorry, there seems to be a request conflict, kindly check your input and try again.";
        public const string DATABASE_CONFLICT_ERROR = "One or more unique fields already exist, kindly try again later.";
        public const string NOT_FOUND_ERROR = "Sorry, the resource you have requested for is not available at the moment.";
    }
}
