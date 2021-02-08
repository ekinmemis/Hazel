/*
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the author(s) be held liable for any damages arising from
 * the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 *   1. The origin of this software must not be misrepresented; you must not
 *      claim that you wrote the original software. If you use this software
 *      in a product, an acknowledgment in the product documentation would be
 *      appreciated but is not required.
 * 
 *   2. Altered source versions must be plainly marked as such, and must not
 *      be misrepresented as being the original software.
 * 
 *   3. This notice may not be removed or altered from any source distribution.
 */

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Hazel.Core.Html.CodeFormatter
{
    /// <summary>
    /// Provides a base implementation for all code formatters.
    /// </summary>
    public abstract partial class SourceFormat
    {
        /// <summary/>
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceFormat"/> class.
        /// </summary>
        protected SourceFormat()
        {
            TabSpaces = 4;
            LineNumbers = false;
            Alternate = false;
            EmbedStyleSheet = false;
        }

        /// <summary>
        /// Gets the CSS stylesheet as a stream.
        /// </summary>
        /// <returns>A text <see cref="Stream"/> of the CSS definitions.</returns>
        public static Stream GetCssStream()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Manoli.Utils.CSharpFormat.csharp.css");
        }

        /// <summary>
        /// Gets the CSS stylesheet as a string.
        /// </summary>
        /// <returns>A string containing the CSS definitions.</returns>
        public static string GetCssString()
        {
            using var reader = new StreamReader(GetCssStream());
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Gets or sets the tabs width..
        /// </summary>
        public byte TabSpaces { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether LineNumbers
        /// Enables or disables line numbers in output..
        /// </summary>
        public bool LineNumbers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Alternate
        /// Enables or disables alternating line background..
        /// </summary>
        public bool Alternate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EmbedStyleSheet
        /// Enables or disables the embedded CSS style sheet..
        /// </summary>
        public bool EmbedStyleSheet { get; set; }

        /// <summary>
        /// Transforms a source code stream to HTML 4.01.
        /// </summary>
        /// <param name="source">Source code stream.</param>
        /// <returns>A string containing the HTML formatted code.</returns>
        public string FormatCode(Stream source)
        {
            using var reader = new StreamReader(source);
            var s = reader.ReadToEnd();
            return FormatCode(s, LineNumbers, Alternate, EmbedStyleSheet, false);
        }

        /// <summary>
        /// Transforms a source code string to HTML 4.01.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <returns>A string containing the HTML formatted code.</returns>
        public string FormatCode(string source)
        {
            return FormatCode(source, LineNumbers, Alternate, EmbedStyleSheet, false);
        }

        /// <summary>
        /// Allows formatting a part of the code in a different language,
        /// for example a JavaScript block inside an HTML file.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string FormatSubCode(string source)
        {
            return FormatCode(source, false, false, false, true);
        }

        /// <summary>
        /// Gets or sets the CodeRegex
        /// The regular expression used to capture language tokens..
        /// </summary>
        protected Regex CodeRegex { get; set; }

        /// <summary>
        /// Called to evaluate the HTML fragment corresponding to each 
        /// matching token in the code.
        /// </summary>
        /// <param name="match">The match<see cref="Match"/>.</param>
        /// <returns>A string containing the HTML code fragment.</returns>
        protected abstract string MatchEval(Match match);

        //does the formatting job
        /// <summary>
        /// The FormatCode.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <param name="lineNumbers">The lineNumbers<see cref="bool"/>.</param>
        /// <param name="alternate">The alternate<see cref="bool"/>.</param>
        /// <param name="embedStyleSheet">The embedStyleSheet<see cref="bool"/>.</param>
        /// <param name="subCode">The subCode<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string FormatCode(string source, bool lineNumbers,
            bool alternate, bool embedStyleSheet, bool subCode)
        {
            //replace special characters
            var sb = new StringBuilder(source);

            if (!subCode)
            {
                sb.Replace("&", "&amp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\t", string.Empty.PadRight(TabSpaces));
            }

            //color the code
            source = CodeRegex.Replace(sb.ToString(), MatchEval);

            sb = new StringBuilder();

            if (embedStyleSheet)
            {
                sb.AppendFormat("<style type=\"{0}\">\n", MimeTypes.TextCss);
                sb.Append(GetCssString());
                sb.Append("</style>\n");
            }

            if (lineNumbers || alternate) //we have to process the code line by line
            {
                if (!subCode)
                    sb.Append("<div class=\"csharpcode\">\n");

                var reader = new StringReader(source);
                var i = 0;
                const string spaces = "    ";
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    i++;
                    if (alternate && i % 2 == 1)
                    {
                        sb.Append("<pre class=\"alt\">");
                    }
                    else
                    {
                        sb.Append("<pre>");
                    }

                    if (lineNumbers)
                    {
                        var order = (int)Math.Log10(i);
                        sb.Append("<span class=\"lnum\">"
                            + spaces.Substring(0, 3 - order) + i
                            + ":  </span>");
                    }

                    sb.Append(line.Length == 0 ? "&nbsp;" : line);
                    sb.Append("</pre>\n");
                }

                reader.Close();

                if (!subCode)
                    sb.Append("</div>");
            }
            else
            {
                //have to use a <pre> because IE below ver 6 does not understand 
                //the "white-space: pre" CSS value
                if (!subCode)
                    sb.Append("<pre class=\"csharpcode\">\n");

                sb.Append(source);
                if (!subCode)
                    sb.Append("</pre>");
            }

            return sb.ToString();
        }
    }
}
