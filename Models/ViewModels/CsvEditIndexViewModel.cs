using CSVEDITOR.Models.File;
using System.Collections.Generic;

namespace CSVEDITOR.Models.ViewModels
{
    public class CsvEditIndexViewModel
    {
        //Sets for showing creat window
        public bool CreatingStatus { get; set; }
        //Sets for showing edit window
        public bool EditingStatus { get; set; }

        public TransactionModel TransactionForEdit { get; set; }
        public List<TransactionModel> Transactions { get; set; }

    }
}
