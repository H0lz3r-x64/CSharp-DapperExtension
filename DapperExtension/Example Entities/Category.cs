namespace DapperExtension.Entities
{
    public class Category
    {
        public int? Id { get; set; }
        public int? Parent_Id { get; set; }
        public string Text { get; set; }

        public Category()
        {
        }

        public Category(int? id, int? parentId, string text)
        {
            Id = id;
            Parent_Id = parentId;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }
        public override string ToString() =>
            "(" + Id + ", " + (Parent_Id == null ? "NULL" : Parent_Id) + ", " + Text + ")";
    }
}
