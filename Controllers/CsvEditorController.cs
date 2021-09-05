using ClosedXML.Excel;
using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.Context;
using CSVEDITOR.Models.File;
using CSVEDITOR.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEDITOR.Controllers
{
    //User must authorize for use this part of the API
    //Authorize details in AccountController
    [Authorize]
    public class CsvEditorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CsvEditorContext _context;

        public CsvEditorController(IMediator mediator, CsvEditorContext context)
        {
            _mediator = mediator;
            _context = context;
        }


        public async Task<ActionResult> Index()
        {
            //Getting all transactions from DB
            var command = new GetTransactionCommand();
            //Details in GetTransactionHandler
            var result = await _mediator.Send(command);
            //Details in CsvEditIndexViewModel in Models folder
            CsvEditIndexViewModel viewModel = new CsvEditIndexViewModel
            {
                CreatingStatus = false,
                EditingStatus = false,
                TransactionForEdit = null,
                Transactions = result
            };
            return View(viewModel);
        }


        public async Task<ActionResult> Edit(int id)
        {
            //Getting all transactions from DB
            var command = new GetTransactionCommand();
            //Details in GetTransactionHandler
            var result = await _mediator.Send(command);
            //Details in CsvEditIndexViewModel in Models folder
            CsvEditIndexViewModel viewModel = new CsvEditIndexViewModel
            {
                CreatingStatus = false,
                //Setting Editing status true for showing edit window in CsvEditor/Index
                EditingStatus = true,
                //Separeting edit transaction from other
                TransactionForEdit = result.Where(x => x.TransactionId == id).ToArray()[0],
                //Another transactions
                Transactions = result.Where(x => x.TransactionId != id).ToList()
            };
            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] int TransactionId, string Status, string Type, string ClientName, string Amount)
        {
            //Getting updated transaction data from form in CsvEditor/Edit
            var command = new EditTransactionCommand(new TransactionModel
            {
                TransactionId = TransactionId
                ,
                Status = Status
                ,
                Type = Type
                ,
                ClientName = ClientName
                ,
                Amount = Amount
            });
            //Details in EditTransactionHandler
            //Handler return boolean for notify app for succes or error
            var result = await _mediator.Send(command);
            if (result) return RedirectToAction("Index", "CsvEditor");
            //in case of error it redirect user to error page
            else return RedirectToAction("Error", "Home");

        }

        public async Task<ActionResult> Create()
        {
            //getting all transactions for insert them to viewmodel
            var command = new GetTransactionCommand();
            //Details in GetTransactionHandler
            var result = await _mediator.Send(command);

            CsvEditIndexViewModel viewModel = new CsvEditIndexViewModel
            {
                //Setting CreatingStatus true for show creat window at CsvEditor/Create
                CreatingStatus = true,
                EditingStatus = false,
                TransactionForEdit = null,
                Transactions = result
            };
            return View("Index", viewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] int TransactionId, string Status, string Type, string ClientName, string Amount)
        {
            //Getting data from form at CsvEditor/Create
            //Insert Data to command
            var command = new AddTransactionCommand(new TransactionModel { TransactionId = TransactionId, Status = Status, Type = Type, ClientName = ClientName, Amount = Amount });
            //Details in AddTransactionHandler
            var result = await _mediator.Send(command);
            //in case of error redirecting user to error page
            if (result)
                return RedirectToAction("Index", "CsvEditor");
            else
                return RedirectToAction("Error", "Home");
        }


        [HttpPost]

        public async Task<ActionResult> Import(IFormFile File)
        {
            //Import .csv file from form at CsvEditor/Index
            //Putting File Data to command
            var command = new ImportCsvFileCommand(File);
            //Details in ImportCsvFileHandler
            var result = await _mediator.Send(command);
            //In Result we get updated transaction list 
            //Putting Reuslt into viewmodel
            return View("Index", new CsvEditIndexViewModel { CreatingStatus = false, EditingStatus = false, TransactionForEdit = null, Transactions = result });
        }

        public async Task<ActionResult> Delete(int id)
        {
            //Getting transaction id from <a>Delete</a> in transaction row at CsvEditor/Index
            var command = new DeleteTransactionCommand(id);
            var result = await _mediator.Send(command);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Search([FromForm] string ClientName)
        {
            //Getting Client Name from form at CsvEditor/Index
            //Putting Client Name into command
            var command = new SearchTransactionByClientNameCommand(ClientName);
            //Details in SearchTransactionByClientNameHandler
            //Getting transaction list where client name coincided
            var result = await _mediator.Send(command);
            //Putting transaction list into viewModel
            return View("Index", new CsvEditIndexViewModel { CreatingStatus = false, EditingStatus = false, TransactionForEdit = null, Transactions = result });
        }
        [HttpPost]
        public async Task<ActionResult> Filter([FromForm] string Status, string Type)
        {
            //Filtering transactions by status and type
            //Getting data from form at CsvEditor/Index
            //Putting data into command
            var command = new FilterTransactionsByStatusAndTypeCommand(Status, Type);
            //Detail in FilterTransactionsByStatusAndTypeHandler
            var result = await _mediator.Send(command);
            //Getting new transaction list 
            //Putting new transaction list  into viewmodel
            return View("Index", new CsvEditIndexViewModel { CreatingStatus = false, EditingStatus = false, TransactionForEdit = null, Transactions = result });
        }
        [HttpPost]
        public async Task<ActionResult> Export([FromForm] string IdBool, string StatusBool, string TypeBool, string ClientBool, string AmountBool, string Status, string Type)
        {
            //Exporting Excel file 
            //creating metadata
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "transactions.xlsx";
            //using try catch for catching erors
            try
            {
                //creating class instanse
                //Using XLWorkbook service for creating tables
                using (var workbook = new XLWorkbook())
                {
                    //creating command for getting filtered informations
                    var command = new FilterTransactionsByStatusAndTypeCommand(Status, Type);
                    //Details ing FilterTransactionsByStatusAndTypeHandler
                    var result = await _mediator.Send(command);

                    var transactions = result;
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Transactions");
                    //Getting checkbox info from form at CsvEdit/Index
                    //Data from checkbox coming in string or null
                    //Checked checkbox is "on"
                    //Checking wich columns will be exported 

                    worksheet.Cell(1, 1).Value = IdBool == "on" ? "TransactionId" : "";
                    worksheet.Cell(1, 2).Value = StatusBool == "on" ? "Status" : "";
                    worksheet.Cell(1, 3).Value = TypeBool == "on" ? "Type" : "";
                    worksheet.Cell(1, 4).Value = ClientBool == "on" ? "Client" : "";
                    worksheet.Cell(1, 5).Value = AmountBool == "on" ? "Amount" : "";
                    //Putting filtered data into cells
                    for (int index = 1; index <= transactions.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value =
                            //Checking which data will be puted into cells
                            IdBool == "on" ?
                        transactions[index - 1].TransactionId : "";
                        worksheet.Cell(index + 1, 2).Value =
                            StatusBool == "on" ?
                        transactions[index - 1].Status : "";
                        worksheet.Cell(index + 1, 3).Value =
                            TypeBool == "on" ?
                        transactions[index - 1].Type : "";
                        worksheet.Cell(index + 1, 4).Value =
                            ClientBool == "on" ?
                        transactions[index - 1].ClientName : "";
                        worksheet.Cell(index + 1, 5).Value =
                            AmountBool == "on" ?
                        transactions[index - 1].Amount : "";
                    }
                    //Creating memory stream for saving file data 
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        //Returning File to user
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                //in case of error returning 404 error
                return NotFound(ex);
            }
        }
    }
}
