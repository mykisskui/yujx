//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mykisskui
{
    using System;
    using System.Collections.Generic;
    
    public partial class article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string author { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string key { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<int> read { get; set; }
        public Nullable<int> top { get; set; }
        public Nullable<int> tread { get; set; }
        public Nullable<bool> Enable { get; set; }

      
    }
    public enum type
    {
        文章 = 1,
        游戏 = 2
    }
}
