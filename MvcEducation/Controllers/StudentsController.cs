﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcEducation.Models;
using MvcEducation.ViewModels;

namespace MvcEducation.Controllers
{
    public class StudentsController : Controller
    {
        private MvcEducationContext db = new MvcEducationContext();

		private void TestLinqJoin() {
			var results = from s in db.Students
						  join e in db.Enrolleds
							on s.Id equals e.StudentId
						  join c in db.Classes
							on e.ClassId equals c.Id
						  select c;
		}

		// Get Classes for Student
		public ActionResult ClassesForStudent(int? id) {
			ClassesForStudent cfs = new ClassesForStudent();
			cfs.Student = db.Students.Find(id);
			var classes = new List<Class>();
			var enrolleds = db.Enrolleds.Where(e => e.StudentId == id).ToArray();
			foreach(var enrolled in enrolleds) {
				classes.Add(db.Classes.Find(enrolled.ClassId));
			}
			cfs.Classes = from s in db.Students
							join e in db.Enrolleds
								on s.Id equals e.StudentId
							join c in db.Classes
								on e.ClassId equals c.Id
							select c;
			cfs.Classes = classes;
			return View(cfs);
		}

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Major);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.MajorId = new SelectList(db.Majors, "Id", "Description");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,MajorId,SAT")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorId = new SelectList(db.Majors, "Id", "Description", student.MajorId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorId = new SelectList(db.Majors, "Id", "Description", student.MajorId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,MajorId,SAT")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorId = new SelectList(db.Majors, "Id", "Description", student.MajorId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
