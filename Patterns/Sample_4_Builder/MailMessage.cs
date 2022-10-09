using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_4_Builder
{
    internal class MailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return $"From: {From}; To: {To}; Subject: {Subject}; Body: {Body}";
        }
    }
}
