using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Extensions
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src)
        {
            string img = string.Format("<img src='{0}' />", src);
            return MvcHtmlString.Create(img);
        }
        public static MvcHtmlString Image(this HtmlHelper helper, string src, object htmlAttributes)
        {

            TagBuilder imgBuilder = new TagBuilder("img");
            //"~/............."
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);

            imgBuilder.Attributes.Add("src", url.Content(src));

            if (htmlAttributes != null)
            {
                IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    imgBuilder.Attributes.Add(item.Key, item.Value.ToString());
                }
            }
            return MvcHtmlString.Create(imgBuilder.ToString());
        }
    }
}