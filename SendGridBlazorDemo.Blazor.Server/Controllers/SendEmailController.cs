using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SendGrid.Helpers.Mail;
using SendGridBlazorDemo.Blazor.Server.Services;
using SendGridBlazorDemo.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGridBlazorDemo.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
    public partial class SendEmailController : ObjectViewController<DetailView, EmailModel>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public SendGridClientManager SendGridM { get; set; }
        //[ActivatorUtilitiesConstructor]
        public SendEmailController()
        {
            InitializeComponent();
           
            var sendEmailAction = new SimpleAction(this, "Send", "MyCategory");
            sendEmailAction.ImageName = "Action_Change_State";
            sendEmailAction.Execute += SendEmailAction_Execute;
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        private async void SendEmailAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            SendGridM = Application.ServiceProvider.GetRequiredService<SendGridClientManager>();
            bool response = await SendGridM.SendEmail(ViewCurrentObject.To, ViewCurrentObject.CC, ViewCurrentObject.Bcc, ViewCurrentObject.Content);

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active["Delete"] = false;
            Frame.GetController<ModificationsController>().Active["Modification"] = false;
            Frame.GetController<RefreshController>().Active["Refresh"] = false;
            Frame.GetController<BlazorRecordsNavigationController>().Active["Nav"] = false;
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
