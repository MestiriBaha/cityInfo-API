﻿namespace FirstApiCreated.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}