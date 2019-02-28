using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateAddedSort { get; set; }
        public string CurrentBookFilter { get; set; }
        public string CurrentKeywordFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<JournalEntry> JournalEntry { get;set; }


        public async Task OnGetAsync(string sortOrder, string currentBookFilter, string currentKeywordFilter, 
            string searchBook, string searchKeyword, int? pageIndex)
        {
            CurrentSort = sortOrder;
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateAddedSort = sortOrder == "dateAdded" ? "dateAdded_desc" : "dateAdded";

            if (searchBook != null || searchKeyword != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchBook = currentBookFilter;
                searchKeyword = currentKeywordFilter;
            }

            CurrentBookFilter = searchBook;
            CurrentKeywordFilter = searchKeyword;

            var journalEntries = from m in _context.JournalEntry select m;
            if (!string.IsNullOrEmpty(searchBook))
            {
                journalEntries = journalEntries.Where(s => s.Book.Contains(searchBook));
            }
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                journalEntries = journalEntries.Where(s => s.Note.Contains(searchKeyword));
            }

            switch (sortOrder)
            {
                case "book_desc":
                    journalEntries = journalEntries.OrderByDescending(s => s.Book);
                    break;
                case "dateAdded":
                    journalEntries = journalEntries.OrderBy(s => s.DateAdded);
                    break;
                case "dateAdded_desc":
                    journalEntries = journalEntries.OrderByDescending(s => s.DateAdded);
                    break;
                default:
                    journalEntries = journalEntries.OrderBy(s => s.Book);
                    break;
            }

            int pageSize = 5;
            JournalEntry = await PaginatedList<JournalEntry>.CreateAsync(
                journalEntries.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
