using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nxk161830Asg4
{
    /**
     * This Class stores the Search Result
     * By Line Number and Line Text
     **/
    class SearchResult
    {
        private int lineNo;
        private string lineText;

        public int LineNo
        {
            get
            {
                return lineNo;
            }

            set
            {
                lineNo = value;
            }
        }

        public string LineText
        {
            get
            {
                return lineText;
            }

            set
            {
                lineText = value;
            }
        }
    }
}
