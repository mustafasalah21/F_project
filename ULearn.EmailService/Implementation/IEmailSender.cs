// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEmail.cs" company="JustProtect">
//   Copyright (C) 2017. All rights reserved.
// </copyright>
// <summary>
//   The email interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace ULearn.EmailService.Implementation
{
    public interface IEmailSender  
    {
        void SendEmail(Message message);
    }
}