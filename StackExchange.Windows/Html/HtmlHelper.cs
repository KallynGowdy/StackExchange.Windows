using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.UI.Text;
using Windows.UI.Xaml.Documents;

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

        public static string Tag(string tag, params string[] content)
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

        public static Block ConvertHtmlToBlocks(string html)
        {
            var xml = XDocument.Parse(Tag("p", html));
            return ConvertToBlock(xml.Root);
        }

        private static bool FilterNodes(XNode element)
        {
            return element.NodeType == XmlNodeType.Element || element.NodeType == XmlNodeType.CDATA || element.NodeType == XmlNodeType.Text;
        }

        private static Block ConvertToBlock(XElement element)
        {
            if (element.Name.LocalName == "p")
            {
                return P(element);
            }
            else
            {
                var inline = ConvertToInline(element);
                if (inline != null)
                {
                    return new Paragraph()
                    {
                        TextIndent = 0,
                        Inlines = { inline }
                    };
                }
            }
            return null;
        }

        private static Inline ConvertToInline(XNode node)
        {
            if (node is XText text)
            {
                return new Run()
                {
                    Text = text.Value
                };
            }
            else if (node is XElement element)
            {
                if (element.Name.LocalName == "strong")
                {
                    return Bold(element);
                }
                else if (element.Name.LocalName == "a")
                {
                    return Link(element);
                }
                else if (element.Name.LocalName == "code")
                {
                    return Code(element);
                }
                else
                {
                    return new Run()
                    {
                        Text = element.Value
                    };
                }
            }
            return null;
        }

        private static Block P(XElement element)
        {
            var p = new Paragraph()
            {
                TextIndent = 0
            };
            foreach (var inline in ConvertToInlines(element))
            {
                if (inline != null)
                {
                    p.Inlines.Add(inline);
                }
            }
            return p;
        }

        private static IEnumerable<Inline> ConvertToInlines(XElement element)
        {
            return element.Nodes().Where(FilterNodes).Select(ConvertToInline);
        }

        private static Inline Bold(XElement element)
        {
            var bold = new Bold();
            AddInlines(element, bold);
            return bold;
        }

        private static Inline Link(XElement element)
        {
            var link = new Hyperlink();
            var uri = element.Attribute("href");
            link.NavigateUri = new Uri(uri.Value);
            AddInlines(element, link);
            return link;
        }

        private static Inline Code(XElement element)
        {
            var code = new Span { FontStyle = FontStyle.Italic };
            AddInlines(element, code);
            return code;
        }

        private static void AddInlines(XElement element, Span output)
        {
            foreach (var inline in ConvertToInlines(element))
            {
                if (inline != null)
                {
                    output.Inlines.Add(inline);
                }
            }
        }
    }
}
