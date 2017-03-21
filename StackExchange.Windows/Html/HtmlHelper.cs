using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Data.Html;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using HtmlAgilityPack;

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

        public static string TagNoIndent(string tag, params string[] content)
        {
            if (content.Length > 0)
            {
                return $@"<{tag}>{string.Join(Environment.NewLine, content)}</{tag}>".Trim();
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
            var doc = new HtmlDocument();
            doc.LoadHtml(TagNoIndent("p", html));
            return ConvertToBlock(doc.DocumentNode.FirstChild);
        }

        private static bool FilterNodes(HtmlNode element)
        {
            return element.NodeType == HtmlNodeType.Element || element.NodeType == HtmlNodeType.Text;
        }

        private static Block ConvertToBlock(HtmlNode element)
        {
            if (element.Name == "p")
            {
                return P(element);
            }
            else
            {
                var inline = ConvertToInline(element);
                if (inline != null)
                {
                    var para = Paragraph();
                    para.Inlines.Add(inline);
                    return para;
                }
            }
            return null;
        }

        private static Inline ConvertToInline(HtmlNode node)
        {
            if (node is HtmlTextNode text)
            {
                return new Run()
                {
                    Text = WebUtility.HtmlDecode(text.Text)
                };
            }
            else if (node is HtmlNode element)
            {
                switch (element.Name)
                {
                    case "strong":
                        return Bold(element);
                    case "a":
                        return Link(element);
                    case "code":
                        return Code(element);
                    default:
                        return new Run()
                        {
                            Text = WebUtility.HtmlDecode(element.InnerText)
                        };
                }
            }
            return null;
        }

        private static Block P(HtmlNode element)
        {
            var p = Paragraph();
            foreach (var inline in ConvertToInlines(element))
            {
                if (inline != null)
                {
                    p.Inlines.Add(inline);
                }
            }
            return p;
        }

        private static Paragraph Paragraph()
        {
            return new Paragraph()
            {
                Margin = new Thickness(0)
            };
        }

        private static IEnumerable<Inline> ConvertToInlines(HtmlNode element)
        {
            return element.ChildNodes.Where(FilterNodes).Select(ConvertToInline);
        }

        private static Inline Bold(HtmlNode element)
        {
            var bold = new Bold();
            AddInlines(element, bold);
            return bold;
        }

        private static Inline Link(HtmlNode element)
        {
            var link = new Hyperlink();
            var uri = element.GetAttributeValue("href", "");
            link.NavigateUri = new Uri(uri);
            AddInlines(element, link);
            return link;
        }

        private static Inline Code(HtmlNode element)
        {
            return new InlineUIContainer
            {
                Child = new StackPanel()
                {
                    Children =
                    {
                        new TextBlock()
                        {
                            Text = WebUtility.HtmlDecode(element.InnerText),
                            Style = (Style) App.Current.Resources["InlineCodeComment"]
                        }
                    },
                    Style = (Style)App.Current.Resources["InlineCodeCommentContainer"]
                }
            };
        }

        private static void AddInlines(HtmlNode element, Span output)
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
