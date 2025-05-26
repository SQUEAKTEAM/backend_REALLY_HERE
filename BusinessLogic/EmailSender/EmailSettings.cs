﻿namespace BusinessLogic.EmailSender;

public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public bool EnableSsl { get; set; }
    public int Timeout { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
