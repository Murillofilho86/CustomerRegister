namespace CustomerMicroService.Application.Queries.SQL
{
    public static class CustomerSql
    {
        public static string GetById
        {
            get
            {
                return @"SELECT c.CustomerId  
                                ,c.FirstName
                                ,c.LastName
                                ,c.Email
                                ,c.Phone
                        FROM Customer c
                        WHERE c.CustomerId = @customerId;";
            }
        }

        public static string GetByFilter
        {
            get
            {
                return @"SELECT c.CustomerId  
                                ,c.FirstName
                                ,c.LastName
                                ,c.Email
                                ,c.Phone
                        FROM Customer c
                        ORDER BY CustomerId
                        OFFSET @offset ROWS
                        FETCH NEXT @pageSize ROWS ONLY;";
            }
        }
    }
}
