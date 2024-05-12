namespace PrintManager.Application.Parameters;

public  class PaginationParameters
{
    private PaginationParameters(int? page, int? pageSize, int defaultPageNumber, int defaultPageSize) 
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : defaultPageNumber;

        Size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : defaultPageSize;

        Skip = (pageNumber - 1) * Size;
    }

    public int Skip { get;}

    public int Size { get;}

    public static PaginationParameters Create(int? page, int? pageSize, int defaultPageNumber, int defaultPageSize)
    {
        return new PaginationParameters(page, pageSize, defaultPageNumber, defaultPageSize);
    }
}
