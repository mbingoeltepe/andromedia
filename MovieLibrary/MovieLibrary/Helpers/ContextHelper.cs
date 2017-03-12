using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace MovieLibrary.Helpers
{
    public class ContextHelper<T> where T : ObjectContext, new()
    {
        private const string ObjectContextKey = "ObjectContext";

        public static T GetCurrentContext()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                string contextTypeKey = ObjectContextKey + typeof(T).Name;

                if (httpContext.Items[contextTypeKey] == null)
                {
                    httpContext.Items.Add(contextTypeKey, new T());
                }
                return httpContext.Items[contextTypeKey] as T;
            }

            throw new ApplicationException("There is no Http Context available");
        }
    }
}