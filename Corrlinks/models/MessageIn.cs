using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corrlinks.models
{
    class MessageIn
    {
        private string from;
        private DateTime date;
        private string subject;
        private string message;
        private int inmate_id;
        private string status;
        private DateTime timestamp;

        public string FROM
        {
            get { return from; }
            set { from = value; }
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
