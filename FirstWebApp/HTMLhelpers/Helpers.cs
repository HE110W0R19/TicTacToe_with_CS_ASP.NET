using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstWebApp.HTMLhelpers
{
    public static class Helpers
    {
        public static HtmlString ShowNames(this IHtmlHelper html, string firstName, string lastName)
        {
            string outputBox = $"<h1 class=\"display-4\">Hello, {firstName} and {lastName}</h1>";
            return new HtmlString(outputBox);
        }

        public static HtmlString ShowWinner(this IHtmlHelper html, string winnerName)
        {
            string outputBox = $"<b>!!! {winnerName} is winner !!!</b>";
            return new HtmlString(outputBox);
        }
    }
}
