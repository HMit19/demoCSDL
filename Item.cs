using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCSDL
{
    internal class Item
    {
        private string question;
        private string sql;

        public Item() { }

        public Item(string question, string sql)
        {
            this.question = question;
            this.sql = sql;
        }

        public string Question { get => question; set => question = value; }
        public string Sql { get => sql; set => sql = value; }
    }
}
