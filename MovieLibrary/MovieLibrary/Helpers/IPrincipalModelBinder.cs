using System.Web.Mvc;
using System;
using System.Security.Principal;

public class IPrincipalModelBinder : IModelBinder
{
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        if (controllerContext == null)
        {
            throw new ArgumentNullException("controllerContext");
        }
        if (bindingContext == null)
        {
            throw new ArgumentNullException("bindingContext");
        }

        IPrincipal p = controllerContext.HttpContext.User;
        return p;
    }
}