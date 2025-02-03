using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using Microsoft.AspNetCore.Components;

namespace SendGridBlazorDemo.Blazor.Server.Editors.EmailContentEditor
{
    [PropertyEditor(typeof(string), false)]
    public class EmailContentEditor : BlazorPropertyEditorBase
    {
        public EmailContentEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        public override EmailContentModel ComponentModel => (EmailContentModel)base.ComponentModel;
        protected override IComponentModel CreateComponentModel()
        {
            var model = new EmailContentModel();
            //model.ValueExpression = () => model.Value;
            model.ValueChanged = EventCallback.Factory.Create<string>(this, value => {
                model.Content = value;
                OnControlValueChanged();
                WriteValue();
            });
            return model;
        }
        protected override void ReadValueCore()
        {
            base.ReadValueCore();
            ComponentModel.Content = (string)PropertyValue;
        }
        protected override object GetControlValueCore() => ComponentModel.Content;
        protected override void ApplyReadOnly()
        {
            base.ApplyReadOnly();
            ComponentModel?.SetAttribute("readonly", !AllowEdit);
        }
    }
}
