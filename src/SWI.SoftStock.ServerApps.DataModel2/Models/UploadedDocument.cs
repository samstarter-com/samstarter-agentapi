namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;   
    using System.ComponentModel.DataAnnotations;

    public partial class UploadedDocument : IAuditable
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    }
}
