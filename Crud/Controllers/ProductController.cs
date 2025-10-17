using System;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
public class ProductController : Controller
{
    private static List<Product> Products = new List<Product>();
    public IActionResult Index()
    {
        return View(Products);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
        product.CreatedAt = DateTime.Now;
        Products.Add(product);
        return RedirectToAction("Index");
    }
    public IActionResult Edit(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.CreatedAt = DateTime.Now;
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        Products.Remove(product);
        return RedirectToAction("Index");
    }
[HttpPost , ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        Products.Remove(product);
        return RedirectToAction("Index");
    }   
}
