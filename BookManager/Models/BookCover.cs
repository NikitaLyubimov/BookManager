//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookCover
    {
        public long Id { get; set; }
        public int CoverFile { get; set; }
        public string Book_Key { get; set; }
    
        public virtual Book Book { get; set; }
    }
}