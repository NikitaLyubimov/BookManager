//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [KnownType(typeof(Book))]
    [DataContract]
    public partial class BookCover
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int CoverFile { get; set; }
        [DataMember]
        public string Book_Key { get; set; }
        

        [DataMember]
        public virtual Book Book { get; set; }
    }
}