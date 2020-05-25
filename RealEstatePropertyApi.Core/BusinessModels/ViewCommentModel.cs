using System;

namespace CoreApiProject.Core.BusinessModels
{
    public class ViewCommentModel
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid TaskId { get; set; }
        public string Name { get; set; }
    }
}
