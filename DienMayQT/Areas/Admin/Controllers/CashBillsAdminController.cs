﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQT.Models;

namespace DienMayQT.Areas.Admin.Controllers
{
    public class CashBillsAdminController : Controller
    {
        private DmQT06Entities1 db = new DmQT06Entities1();

        private void Check(CashBill model)
        {
            if (model.CustomerName.Length > 50)
                ModelState.AddModelError("CustomerName", "Tên khách hàng phải ngắn hơn 50 kí tự!");
            if (model.PhoneNumber.Length < 10)
                ModelState.AddModelError("PhoneNumber", "Số điện thoại phải nhiều hơn 10 kí tự!");
            if (model.Address.Length > 100)
                ModelState.AddModelError("Address", "Địa chỉ phải ngắn hơn 100 kí tự!");
            if (model.Shipper.Length > 50)
                ModelState.AddModelError("Shipper", "Tên Shipper phải ngắn hơn 50 kí tự!");
            if (model.Note.Length > 100)
                ModelState.AddModelError("Note", "Ghi chú phải ngắn hơn 100 kí tự!");

            //if (model.ProductID > 20)
            //    ModelState.AddModelError("ProductID", "Mã sản phẩm phải ngắn hơn 20 kí tự!");
            //if (model.ProductName > 50)
            //    ModelState.AddModelError("ProductName", "Tên sản phẩm phải ngắn hơn 50 kí tự!");
            //if (model.Quantity <= 0)
            //    ModelState.AddModelError("Quantity", "Số lượng phải lớn hơn 0!");
            //if (model.SalePrice <= 0)
            //    ModelState.AddModelError("SalePrice", "Giá bán phải lớn hơn 0!");

            if (model.GrandTotal < 0)
                ModelState.AddModelError("GrandTotal", "Tổng giá tiền phải lớn hơn 0!");
        }

        // GET: Admin/CashBillsAdmin
        public ActionResult Index()
        {
            return View(db.CashBill.ToList());
        }

        // GET: Admin/CashBillsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBill.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }

            var model = new KhuModels();
            model.Product = db.Product.ToList();
            model.ProductType = db.ProductType.ToList();
            model.Cashbill = cashBill;
            return View(model);
        }

        // GET: Admin/CashBillsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CashBillsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            Check(cashBill);
            if (ModelState.IsValid)
            {
                db.CashBill.Add(cashBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cashBill);
        }

        // GET: Admin/CashBillsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBill.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: Admin/CashBillsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            Check(cashBill);
            if (ModelState.IsValid)
            {
                db.Entry(cashBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashBill);
        }

        // GET: Admin/CashBillsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBill.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: Admin/CashBillsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBill cashBill = db.CashBill.Find(id);
            db.CashBill.Remove(cashBill);
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
