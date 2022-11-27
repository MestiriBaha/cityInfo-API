namespace FirstApiCreated.Services
{
    public class PaginationMetadata
    {
        public int TotalItemcount { get; set; }
        public int TotalPagecount { get; set; }
        public int Pagesize { get; set; }
        public int Currentpage { get; set; }
        public PaginationMetadata(int TotalItemcount, int Currentpage , int Pagesize)  
        {
            this.TotalItemcount = TotalItemcount;   
            this.Currentpage=Currentpage;
            this.Pagesize = Pagesize;
            TotalPagecount = (int)Math.Ceiling(TotalItemcount / (double)Pagesize);
            }

    }
}
