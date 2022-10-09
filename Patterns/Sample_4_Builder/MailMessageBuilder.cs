using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_4_Builder
{
    internal class MailMessageBuilder
    {
        private readonly MailMessage _mailMessage = new MailMessage();

        public MailMessage Build()
        {
            if (String.IsNullOrEmpty(_mailMessage.To))
            {
                throw new InvalidOperationException("Can't create message without To value!");
            }
            return _mailMessage;
        }

        public MailMessageBuilder From(string address)
        {
            _mailMessage.From = address;
            return this;
        }

        public MailMessageBuilder To(string address)
        {
            _mailMessage.To = address;
            return this;
        }

        public MailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public MailMessageBuilder Body(string body)
        {
            _mailMessage.Body = body;
            return this;
        }
    }
}
