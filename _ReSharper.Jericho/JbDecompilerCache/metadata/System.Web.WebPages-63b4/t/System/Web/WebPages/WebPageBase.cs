// Type: System.Web.WebPages.WebPageBase
// Assembly: System.Web.WebPages, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Development\Jericho\packages\AspNetWebPages.Core.2.0.20126.16343\lib\net40\System.Web.WebPages.dll

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace System.Web.WebPages
{
    /// <summary>
    /// Serves as the base class for classes that represent an ASP.NET Razor page.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is temporary (elipton)")]
    public abstract class WebPageBase : WebPageRenderingBase
    {
        /// <summary>
        /// When overridden in a derived class, configures the current web page based on the configuration of the parent web page.
        /// </summary>
        /// <param name="parentPage">The parent page from which to read configuration information.</param>
        protected virtual void ConfigurePage(WebPageBase parentPage);

        /// <summary>
        /// Creates a new instance of the <see cref="T:System.Web.WebPages.WebPageBase"/> class by using the specified virtual path.
        /// </summary>
        /// 
        /// <returns>
        /// The new <see cref="T:System.Web.WebPages.WebPageBase"/> object.
        /// </returns>
        /// <param name="virtualPath">The virtual path to use to create the instance.</param>
        public static WebPageBase CreateInstanceFromVirtualPath(string virtualPath);

        /// <summary>
        /// Called by content pages to create named content sections.
        /// </summary>
        /// <param name="name">The name of the section to create.</param><param name="action">The type of action to take with the new section.</param>
        public void DefineSection(string name, SectionWriter action);

        /// <summary>
        /// Executes the code in a set of dependent web pages by using the specified parameters.
        /// </summary>
        /// <param name="pageContext">The context data for the page.</param><param name="writer">The writer to use to write the executed HTML.</param>
        public void ExecutePageHierarchy(WebPageContext pageContext, TextWriter writer);

        /// <summary>
        /// Executes the code in a set of dependent web pages by using the specified context, writer, and start page.
        /// </summary>
        /// <param name="pageContext">The context data for the page.</param><param name="writer">The writer to use to write the executed HTML.</param><param name="startPage">The page to start execution in the page hierarchy.</param>
        public void ExecutePageHierarchy(WebPageContext pageContext, TextWriter writer, WebPageRenderingBase startPage);

        /// <summary>
        /// Executes the code in a set of dependent web pages.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We really don\'t care if SourceHeader fails, and we don\'t want it to fail any real requests ever")]
        public override void ExecutePageHierarchy();

        /// <summary>
        /// Initializes the current page.
        /// </summary>
        protected virtual void InitializePage();

        /// <summary>
        /// Returns a value that indicates whether the specified section is defined in the page.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified section is defined in the page; otherwise, false.
        /// </returns>
        /// <param name="name">The name of the section to search for.</param>
        public bool IsSectionDefined(string name);

        /// <summary>
        /// Returns and removes the context from the top of the <see cref="P:System.Web.WebPages.WebPageBase.OutputStack"/> instance.
        /// </summary>
        public void PopContext();

        /// <summary>
        /// Inserts the specified context at the top of the <see cref="P:System.Web.WebPages.WebPageBase.OutputStack"/> instance.
        /// </summary>
        /// <param name="pageContext">The page context to push onto the <see cref="P:System.Web.WebPages.WebPageBase.OutputStack"/> instance.</param><param name="writer">The writer for the page context.</param>
        public void PushContext(WebPageContext pageContext, TextWriter writer);

        /// <summary>
        /// In layout pages, renders the portion of a content page that is not within a named section.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML content to render.
        /// </returns>
        public HelperResult RenderBody();

        /// <summary>
        /// Renders the content of one page within another page.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML content to render.
        /// </returns>
        /// <param name="path">The path of the page to render.</param><param name="data">(Optional) An array of data to pass to the page being rendered. In the rendered page, these parameters can be accessed by using the <see cref="P:System.Web.WebPages.WebPageBase.PageData"/> property.</param>
        public override HelperResult RenderPage(string path, params object[] data);

        /// <summary>
        /// In layout pages, renders the content of a named section.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML content to render.
        /// </returns>
        /// <param name="name">The section to render.</param><exception cref="T:System.Web.HttpException">The <paramref name="name"/> section was already rendered.-or-The <paramref name="name"/> section was marked as required but was not found.</exception>
        public HelperResult RenderSection(string name);

        /// <summary>
        /// In layout pages, renders the content of a named section and specifies whether the section is required.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML content to render.
        /// </returns>
        /// <param name="name">The section to render.</param><param name="required">true to specify that the section is required; otherwise, false.</param>
        public HelperResult RenderSection(string name, bool required);

        /// <summary>
        /// Writes the specified <see cref="T:System.Web.WebPages.HelperResult"/> object as an HTML-encoded string.
        /// </summary>
        /// <param name="result">The helper result to encode and write.</param>
        public override void Write(HelperResult result);

        /// <summary>
        /// Writes the specified object as an HTML-encoded string.
        /// </summary>
        /// <param name="value">The object to encode and write.</param>
        public override void Write(object value);

        /// <summary>
        /// Writes the specified object without HTML-encoding it first.
        /// </summary>
        /// <param name="value">The object to write.</param>
        public override void WriteLiteral(object value);

        protected internal override TextWriter GetOutputWriter();

        /// <summary>
        /// Gets or sets the path of a layout page.
        /// </summary>
        /// 
        /// <returns>
        /// The path of the layout page.
        /// </returns>
        public override string Layout { get; set; }

        /// <summary>
        /// Gets the current <see cref="T:System.IO.TextWriter"/> object for the page.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.IO.TextWriter"/> object.
        /// </returns>
        public TextWriter Output { get; }

        /// <summary>
        /// Gets the stack of <see cref="T:System.IO.TextWriter"/> objects for the current page context.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.IO.TextWriter"/> objects.
        /// </returns>
        public Stack<TextWriter> OutputStack { get; }

        /// <summary>
        /// Provides array-like access to page data that is shared between pages, layout pages, and partial pages.
        /// </summary>
        /// 
        /// <returns>
        /// A dictionary that contains page data.
        /// </returns>
        public override IDictionary<object, dynamic> PageData { get; }

        /// <summary>
        /// Provides property-like access to page data that is shared between pages, layout pages, and partial pages.
        /// </summary>
        /// 
        /// <returns>
        /// An object that contains page data.
        /// </returns>
        public override dynamic Page { get; }
    }
}
