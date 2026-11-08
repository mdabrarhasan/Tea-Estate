namespace TEALINK.GARDEN.Model.Common
{
    public class EnamProcess
    {
       
        public enum EnumProcessName { KPI, EVO }
        public enum EnumSubMenuCode { DB0001, UM0001, CM0001, GM0001, DM0001,PLK0001, KMM0001, PRUNE0001,WB0001, WBTCH0001, KMJM0001, SM0001,
            WM0001, H0001, SECM0001, PTM0001, PGM0001, CON0001, WC0001, RM0001, DP0001, NPA0001, LC0001, CW0001, LM0001, PTA0001, GW0001,SHIFT0001 ,
            SLGEN0001
        }

        public enum EnumDocumentStatus { INIT, PEND, APPR, REJC, COMP }
        public enum EnumMessage { SAVE, EDIT, DEL, APPR, REJC }
        public enum EnumNotification { SUBMIT, APPROVE, REJECT }

    }
}
