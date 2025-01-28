using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC.Filters;

public class WhiteSpaceRemoverAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var response = context.HttpContext.Response;
        response.Body = new HtmlMinimizer(response.Body);
    }
}


class HtmlMinimizer : Stream
{
    private readonly Stream _outputStream;
    public HtmlMinimizer(Stream filterStream) => _outputStream = filterStream;

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        var html = Encoding.UTF8.GetString(buffer, offset, count);
        var regex = new Regex(@"(?<=\s)\s+(?![^<>]*</pre>)");
        html = regex.Replace(html, string.Empty);
        buffer = Encoding.UTF8.GetBytes(html);
        await _outputStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }
    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }
    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }
    public override void Flush() => _outputStream.Flush();
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }
}

