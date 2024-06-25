using Microsoft.AspNetCore.Components.Web;

public static class AllInOnerMapper
{
    public static Comments ParseComment(this CommentDto commentDto)
    {
        return new Comments
        {
            Comment = commentDto.Comment,
            FilmId = commentDto.FilmId,
            Point = commentDto.Point
        };
    }
}
