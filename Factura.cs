using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdate
{
    class Factura
    {
        public string recepientRFC;
        public string senderRFC;
        public string uuid;
        public string total;
        public string folio;

        public Factura(String RecepientRFC, String SenderRFC, String Uuid, String Total, String Folio)
        {
            recepientRFC = RecepientRFC;
            senderRFC = SenderRFC;
            uuid = Uuid;
            total = Total;
            folio = Folio;
        }
        public Factura()
        {
        }
        private String getChain()
        {
            return "?re=" + senderRFC + "&rr=" + recepientRFC + "&tt=" + total + "&id=" + uuid;
        }
        public int statusOnSAT()
        {
            int state;
            ValidacionSAT comprobacion = new ValidacionSAT(getChain());
            switch (comprobacion.status.ToLower())
            {
                case "s - com"://Found on SAT database
                    if (comprobacion.code.Equals("Cancelado"))
                    {
                        state = 3;
                    }
                    else
                    {
                        state = 1;
                    }
                    break;
                case "n - 601": //Incorrect information
                    state = 4;
                    break;
                case "error": //Error on connection, reschedule for checking
                    state = 0;
                    break;
                default: //Is not yet found on SAT database
                    state = 2;
                    break;
            }
            return state;
        }

    }
}
