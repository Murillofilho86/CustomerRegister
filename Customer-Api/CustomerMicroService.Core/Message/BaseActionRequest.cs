using System.Runtime.Serialization;

namespace CustomerMicroService.Framework.Message
{
    public class BaseActionRequest : BaseRequest
    {

        [DataMember(Name = "page")]
        public int? Page { get; set; } = 1;


        [DataMember(Name = "pageSize")]
        public int? PageSize { get; set; } = 10;


        [DataMember(Name = "columnOrder")]
        public int? ColumnOrder { get; set; } = 1;


        [DataMember(Name = "orderDirection")]
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Asc;


    }
}


public enum OrderDirection
{
    Asc = 1,
    Desc
}