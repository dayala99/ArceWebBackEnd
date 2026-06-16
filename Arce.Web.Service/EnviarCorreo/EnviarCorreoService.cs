using System.Net;
using System.Net.Mail;
using Arce.Web.Entity;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Service;

public class EnviarCorreoService: IEnviarCorreoService
{
    private readonly IConfiguration _configuration;

    public EnviarCorreoService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarCorreoAsync(CorreoEntity correo)
    {
        if (correo == null) throw new ArgumentNullException(nameof(correo));
        if (string.IsNullOrWhiteSpace(correo.Para)) throw new Exception("Correo destino obligatorio.");
        if (string.IsNullOrWhiteSpace(correo.Asunto)) throw new Exception("Asunto obligatorio.");

        var host = _configuration["Smtp:Host"];
        var port = int.Parse(_configuration["Smtp:Port"] ?? "587");
        var enableSsl = bool.Parse(_configuration["Smtp:EnableSsl"] ?? "true");
        var user = _configuration["Smtp:User"];
        var password = _configuration["Smtp:Password"];
        var from = _configuration["Smtp:From"] ?? user;
        var fromName = _configuration["Smtp:FromName"] ?? "Sistema ARCE";

        if (string.IsNullOrWhiteSpace(host)) throw new Exception("Configuracion SMTP incompleta: falta Smtp:Host.");
        if (string.IsNullOrWhiteSpace(user)) throw new Exception("Configuracion SMTP incompleta: falta Smtp:User.");
        if (string.IsNullOrWhiteSpace(password)) throw new Exception("Configuracion SMTP incompleta: falta Smtp:Password.");
        if (string.IsNullOrWhiteSpace(from)) throw new Exception("Configuracion SMTP incompleta: falta Smtp:From.");

        using var message = new MailMessage
        {
            From = new MailAddress(from, fromName),
            Subject = correo.Asunto.Trim(),
            Body = !string.IsNullOrWhiteSpace(correo.CuerpoHtml)
                ? correo.CuerpoHtml
                : correo.CuerpoTexto ?? string.Empty,
            IsBodyHtml = !string.IsNullOrWhiteSpace(correo.CuerpoHtml),
            Priority = MailPriority.High
        };

        message.To.Add(correo.Para.Trim());

        foreach (var copia in correo.Copias ?? new List<string>())
            if (!string.IsNullOrWhiteSpace(copia)) message.CC.Add(copia.Trim());

        foreach (var copiaOculta in correo.CopiasOcultas ?? new List<string>())
            if (!string.IsNullOrWhiteSpace(copiaOculta)) message.Bcc.Add(copiaOculta.Trim());

        using var smtp = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(user, password),
            EnableSsl = enableSsl
        };

        await smtp.SendMailAsync(message);
    }
}
