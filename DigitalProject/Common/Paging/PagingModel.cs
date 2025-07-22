using DigitalProject.Entitys;

namespace DigitalProject.Common.Paging
{
    public class PagingModel<T>
    {
       public  List<T> Data {  get; set; }
       public int  TotalRecords { get; set; }
       public int  PageNumber {  get; set; }
       public int PageSize {  get; set; }
    }
}
