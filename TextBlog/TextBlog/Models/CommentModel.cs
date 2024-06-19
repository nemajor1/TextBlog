public class CommentModel
{
    public int commentId { get; set; }
    public int postId { get; set; }
    public int authorId { get; set; }
    public string commentContent { get; set; }
    public DateTime commentDate { get; set; }

    public UserModel Author { get; set; }
    public PostModel Post { get; set; }
}
