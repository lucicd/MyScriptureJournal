using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyScriptureJournal.Models;
using System;
using System.Linq;

namespace RazorPagesJournalEntry.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>()))
            {
                // Look for any journal entries.
                if (context.JournalEntry.Any())
                {
                    return;   // DB has been seeded
                }

                context.JournalEntry.AddRange(
                    new JournalEntry
                    {
                        Book = "Job 1:1",
                        DateAdded = DateTime.Parse("1989-2-12"),
                        Note = "There was a man in the land of Uz, whose name was Job; and that man was perfect and upright, and one that feared God, and eschewed evil."
                    },

                    new JournalEntry
                    {
                        Book = "Genesis 2:2",
                        DateAdded = DateTime.Parse("1984-3-13"),
                        Note = "And on the seventh day God ended his work which he had made; and he rested on the seventh day from all his work which he had made."
                    },

                    new JournalEntry
                    {
                        Book = "Psalm 19:14",
                        DateAdded = DateTime.Parse("1986-2-23"),
                        Note = "Let the words of my mouth, and the meditation of my heart, be acceptable in thy sight, O Lord, my strength, and my redeemer."
                    },

                    new JournalEntry
                    {
                        Book = "Luke 17:13",
                        DateAdded = DateTime.Parse("1959-4-15"),
                        Note = "And they lifted up their voices, and said, Jesus, Master, have mercy on us."
                    },

                    new JournalEntry
                    {
                         Book = "1 Nephi 1:3",
                         DateAdded = DateTime.Parse("2019-2-28"),
                         Note = "And I know that the record which I make is true; and I make it with mine own hand; and I make it according to my knowledge."
                    },

                    new JournalEntry
                    {
                        Book = "Mosiah 1:12",
                        DateAdded = DateTime.Parse("2019-2-28"),
                        Note = "And I give unto them a name that never shall be blotted out, except it be through transgression."
                    },

                    new JournalEntry
                    {
                        Book = "Ether 6:3",
                        DateAdded = DateTime.Parse("2019-2-28"),
                        Note = "And thus the Lord caused stones to shine in darkness, to give light unto men, women, and children, that they might not cross the great waters in darkness."
                    }

                );
                context.SaveChanges();
            }
        }
    }
}