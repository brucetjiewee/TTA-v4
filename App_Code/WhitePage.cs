using System.Text.RegularExpressions;
using System.Web.UI;
public class WhitePage: MasterPage
{
    private static readonly Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
    private static readonly Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);
    public static string RemoveWhitespaceFromHtml(string html)
    {
        html = RegexBetweenTags.Replace(html, ">");
        html = RegexLineBreaks.Replace(html, "<");

        return html.Trim();
    }

    protected override void Render(HtmlTextWriter writer)
    {
        using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
        {
            base.Render(htmlwriter);
            string html = htmlwriter.InnerWriter.ToString();

            // Trim the whitespace from the 'html' variable
            html = RemoveWhitespaceFromHtml(html);
            writer.Write(html);
        }
    }  
}