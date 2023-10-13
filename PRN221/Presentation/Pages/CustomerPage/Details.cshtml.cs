﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAOs.Models;

namespace Presentation.Pages.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly IRentingTransactionRepository _rentingTransactionRepository;

        public DetailsModel(IRentingTransactionRepository rentingTransactionRepository)
        {
            _rentingTransactionRepository = rentingTransactionRepository;
        }

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!; 

        public IActionResult OnGet(int? id)
        {
            var account = HttpContext.Session.GetString("account");
            if (account != null)
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var rentingtransaction = _rentingTransactionRepository.GetByIdIncludes(id.Value);
            if (rentingtransaction == null)
            {
                return NotFound();
            }
            else 
            {
                RentingTransaction = rentingtransaction;
            }
            return Page();
        }
    }
}
