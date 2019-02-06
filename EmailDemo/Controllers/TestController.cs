using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmailDemo.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace EmailDemo.Controllers
{
   
    public class TestController : Controller
    {   
        private readonly IEmailSender _emailSender;
        List<string> _emails = new List<string>();

        public  TestController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [Route("Test")]
        public async void Index()
        {
            FileInfo existingFile = new FileInfo(@"E:\EmailDemo\EmailDemo\wwwroot\EmailData\Emails.xlsx");
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        _emails.Add( worksheet.Cells[row, col].Value.ToString().Trim());
                    }
                }
            }

            await _emailSender.SendEmailAsync(_emails, "SMTP Email sending demo",
                             $"shani bulk email sending testing");
            
           

           
        }

       
        }

        
    }