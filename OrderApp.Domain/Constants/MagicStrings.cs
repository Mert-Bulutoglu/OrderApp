using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Constants
{
    public static class MagicStrings
    {
        #region ErrorMessages
        public static string NotFoundMessage<T>()
        {
            return $"{typeof(T).Name} not found";
        }
        #endregion
    }
}
