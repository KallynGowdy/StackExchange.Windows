using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Windows.Html
{
    /// <summary>
    /// Defines a class that is designed to assist with building a complete webpage (with styling) from a post.
    /// Note that this class assumes that none of the given content contains any malicious code.
    /// </summary>
    public class HtmlHelper
    {
        public string WrapPostBody(string body)
        {
            return Html(
                Head(
                    ContentSecurityPolicy("script-src 'self' ms-appx-web:; style-src 'self' ms-appx-web: https://cdn.rawgit.com/google/code-prettify/master/loader/prettify.css"),
                    Stylesheet("ms-appx-web:///Html/post.css"),
                    Stylesheet("ms-appx-web:///Html/lib/themes/atelier-heath-light.css")
                ),
                Body(
                    body,
                    Script("ms-appx-web:///Html/lib/prettify.js"),
                    Script("ms-appx-web:///Html/mark_prettyprint.js")
                )
            );
        }

        public string Html(params string[] content)
        {
            return Tag("html", content);
        }

        public string Body(params string[] content)
        {
            return Tag("body", content);
        }

        public string Tag(string tag, params string[] content)
        {
            if (content.Length > 0)
            {
                return $@"
<{tag}>
    {string.Join(Environment.NewLine, content)}
</{tag}>".Trim();
            }
            else
            {
                return $"<{tag} />";
            }
        }

        public string Head(params string[] content)
        {
            return Tag("head", content);
        }

        public string Link(string rel, string href, string type)
        {
            return $@"
<link rel=""{rel}""
      href=""{href}""
      type=""{type}"" />".Trim();
        }

        public string Stylesheet(string href)
        {
            return Link("stylesheet", href, "text/css");
        }

        public string ContentSecurityPolicy(string content)
        {
            return Tag($"meta http-equiv=\"Content-Security-Policy\" content=\"{content}\"");
        }

        public string Script(string src)
        {
            return $@"<script type=""text/javascript"" src=""{src}""></script>";
        }
    }
}
