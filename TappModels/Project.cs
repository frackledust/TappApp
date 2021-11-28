using System;
using System.Collections.Generic;
using System.Text;

namespace TappModels
{

    /*
    [project_id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]               VARCHAR (20)  NOT NULL,
    [original_language]  VARCHAR (10)  NOT NULL,
    [translate_language] VARCHAR (10)  NOT NULL,
    [original_file]      VARCHAR (500) NOT NULL,
    [translate_file]     VARCHAR (500) NULL,
    [requester_id]    
     */

    public class Project : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Original_language { get; set; }
        public string Translate_language { get; set; }
        
        public string Original_text { get; set; }

        public string Translated_text { get; set; }

        public bool IsTranslated
        {
            get
            {
                return Translated_text != null && Translated_text.Length > 0;
            }
        }
    }
}
