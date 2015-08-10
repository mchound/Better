using Better.Data.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace Better.Web.Models.ModelBinders
{
    public class SeasonKeyModelBinder : IModelBinder
    {
        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.Model = JsonConvert.DeserializeObject<IEnumerable<SeasonKey>>(val.AttemptedValue);
            return true;
        }
    }
}