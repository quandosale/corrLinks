using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corrlinks.models
{
    class MessageOut
    {
        private int inbox_id;
        private string mailto;
        private DateTime date;
        private string subject;
        private string message;
        private int inmate_id;
        private string status;
        private DateTime timestamp;

        public int INBOX_ID
        {
            get { return inbox_id; }
            set { inbox_id = value; }
        }

        public string MAILTO
        {
            get { return mailto; }
            set { mailto = value; }
        }
        public DateTime DATE
        {
            get { return date; }
            set { date = value; }
        }
        public string SUBJECT
        {
            get { return subject; }
            set { subject = value; }
        }
        public string MESSAGE
        {
            get { return message; }
            set { message = value; }
        }
        public int INMATE_ID
        {
            get { return inmate_id; }
            set { inmate_id = value; }
        }
        public string STATUS
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime TIMESTAMP
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
    }
}
