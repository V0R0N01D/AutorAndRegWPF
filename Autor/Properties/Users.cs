//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Autor.Properties
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public int IdUser { get; set; }
        public string Logins { get; set; }
        public string Passwords { get; set; }
        public int TypeUserss { get; set; }
    
        public virtual TypeUsers TypeUsers { get; set; }
    }
}
