namespace SendGridBlazorDemo.Blazor.Server.Controllers
{
    using DevExpress.ExpressApp;
    using SendGridBlazorDemo.Module.BusinessObjects;

    // ...
    public class EmailModelActivatorController : ObjectViewController<DetailView, EmailModel>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            if ((ObjectSpace is NonPersistentObjectSpace) && (View.CurrentObject == null))
            {
                View.CurrentObject = ObjectSpace.CreateObject(View.ObjectTypeInfo.Type);
                View.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            }
        }
    }
}
