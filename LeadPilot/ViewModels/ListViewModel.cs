namespace LeadPilot.ViewModels
{
    public class ListViewModel<T> where T:class
    {
        public T data {  get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}
