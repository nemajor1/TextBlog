public class PostModel
{
    public int postId { get; set; }
    public string postTitle { get; set; }
    public string postContent { get; set; }
    public int authorId { get; set; }
    public DateTime postDate { get; set; }

    public UserModel Authors { get; set; }
    public List<CommentModel> Comments { get; set; }
}
