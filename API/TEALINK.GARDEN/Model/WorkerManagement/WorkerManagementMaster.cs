namespace TEALINK.GARDEN.Model.WorkerManagement
{
    public class WorkerManagementView
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string EMPNO { get; set; }
        public string PFNUMBER { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACTNUMBER { get; set; }

        public int DIVISIONID { get; set; }
        public string DIVISIONNAME { get; set; }


        public int GENDERID { get; set; }
        public string GENDERNAME { get; set; }

        public int WORKERTYPEID { get; set; }
        public string WORKERTYPE { get; set; }

        public int WORKERSUBTYPEID { get; set; }
        public string WORKERSUBTYPE { get; set; }

        public int BOOKID { get; set; }
        public string BOOKNAME { get; set; }
        public bool DISABLEDDISINCENTIVEID { get; set; }
        public bool MONTHLYRATED { get; set; }
        public bool ESSENTIALWORKER { get; set; }
        public bool RETIREDID { get; set; }
        public int PLUCKINGBATCHID { get; set; }
        public string PLUCKINGBATCHNAME { get; set; }

        public int BATCHID { get; set; }
        public string BATCHNAME { get; set; }
        public int KAMJARIID { get; set; }
        public string KAMJARI { get; set; }
    }


    public class WorkerManagementMaster
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string PFNUMBER { get; set; }
        public int DIVISIONID { get; set; }
        public string EMPNO { get; set; }
        public int GENDERID { get; set; }
        public int WORKERTYPEID { get; set; }
        public int WORKGROUP { get; set; }
        public int JOBSTATION { get; set; }
        public int WORKERSUBTYPEID { get; set; }
        public int BOOKID { get; set; }
        public bool DISABLEDDISINCENTIVEID { get; set; }
        public bool MONTHLYRATED { get; set; }
        public bool ESSENTIALWORKER { get; set; }
        public bool RETIREDID { get; set; }
        public int PLUCKINGBATCHID { get; set; }
        public int BATCHID { get; set; }
        public int KAMJARIID { get; set; }


        public string DATEOFBIRTH { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACTNUMBER { get; set; }

        public int UNITID { get; set; }
        public int ENROLL { get; set; }
    }

    public class WorkerExcelData
    {
        
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string PFNUMBER { get; set; }
        public string DIVISION { get; set; }
        public string GENDER { get; set; }
        public string WORKERTYPE { get; set; }
        public string BOOK { get; set; }
        public bool RETIREDID { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACTNUMBER { get; set; }
        public bool ISACTIVE {  get; set; }
    }
}
