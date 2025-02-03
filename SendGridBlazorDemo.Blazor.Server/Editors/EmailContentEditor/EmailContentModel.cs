using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;

namespace SendGridBlazorDemo.Blazor.Server.Editors.EmailContentEditor
{
    public class EmailContentModel: ComponentModelBase
    {
        public string Content
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public EventCallback<string> ValueChanged
        {
            get => GetPropertyValue<EventCallback<string>>();
            set => SetPropertyValue(value);
        }

        public override Type ComponentType => typeof(EmailContentComponent);
    }
}
