using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TM.Models;
using TM.Data;

namespace TM.Controllers
{
    public class EventsController : Controller
    {
        //声明了一个私有变量，保存 ToMeetContext 的引用
        private readonly ApplicationDbContext _context;

        //要创建一个 EventsController，你必须提供一个能匹配 ToMeetContext的对象
        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
       
        public async Task<IActionResult> Index(string searchString, string eventTitle)
        {
            //all title string list
            IQueryable<string> titleQuery = from e in _context.Event
                                        orderby e.Title
                                        select  e.Title;
                                        
            var events = from e in _context.Event
                        select e;

            if(!String.IsNullOrEmpty(eventTitle)){
                events = events.Where(e => e.Title == eventTitle);
            }

            if(!String.IsNullOrEmpty(searchString)){
                events = events.Where(e => e.Location.Contains(searchString));
            }

            var EventTitleMV = new EventTitleViewModel();
            EventTitleMV.titles = new SelectList(await titleQuery.Distinct().ToListAsync());
            EventTitleMV.events = await events.ToListAsync();
            return View(EventTitleMV);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Publisher,Title,StartTime,Number,Location,Description,Interest")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            //var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Publisher,Title,StartTime,Number,Location,Description,Interest")] Event @event)
        {
            if (id != @event.ID)
            {
                return NotFound();
            }
            //verifies that the data submitted in the form can be used to modify (edit or update) a Event object
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.ID == id);
        }
    }
}
