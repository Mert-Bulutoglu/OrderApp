using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Constants
{
    public static class MagicStrings
    {

        #region ExceptionValues

        public const string BadRequestExceptionValue = "BadRequestException";
        public const string NotFoundExceptionValue = "NotFoundException";
        public const string ArgumentNullExceptionValue = "ArgumentNullException";
        public const string NotImplementedExceptionValue = "NotImplementedException";
        public const string KeyNotFoundExceptionValue = "KeyNotFoundException";
        public const string ConflictExceptionValue = "ConflictException";
        public const string ForbiddenExceptionValue = "ForbiddenException";
        public const string UnauthorizedAccessExceptionValue = "UnauthorizedAccessException";
        #endregion
        #region ErrorMessages
        public static string NotFoundMessage<T>()
        {
            return $"{typeof(T).Name} not found";
        }
        #endregion
    }
}
