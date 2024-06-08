

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BreadcrumbAttribute : ActionFilterAttribute {

    private readonly string _path;

    public BreadcrumbAttribute(string path) {
        _path = path;
    }


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        if(context.Controller is Controller controller) {
            controller.ViewData["breadcrumb"] = new BreadcrumbModel(_path);
        }

    }

}