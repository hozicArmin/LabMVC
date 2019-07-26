using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LabMVC.Dtos;
using LabMVC.Models;
using AutoMapper;
using System.Data.Entity;

namespace LabMVC.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers   (Get all customers)
        //public IEnumerable<CustomerDto> GetCustomers()
        public IHttpActionResult GetCustomers()
        {
            //Select ovdje radi to da(Selektuje svaki elemenat sequence u "new Form")
            //Map ovdje nije metod on je ref. Tj.samo delegira
            var customerDtos = _context.Customers
                .Include(c=> c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET /api/customers/1   (Get customer with ID 1)
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            //Vracamo Ok Helper Metod i u njega setujemo da prosljedi Customer u CustomerDto. U cTor prosljedjujemo Source iz Db
            return Ok(Mapper.Map<Customer, CustomerDto>(customer)); //Prosljedjujemo iz DB. customer source u konstruktor metoda
        }

        //POST /api/customers   (Add a new customer(customer data in the request body))
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); //vracamo Helper Metod BadRequest() koji je klasa koja inplementira IHttpActionResult

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto); //customer obj. je mapiran iz CustomerDto u Customer

            _context.Customers.Add(customer);       //Id postavka u ovom sl. ce biti bazirana po Id generisanom iz DB.
            _context.SaveChanges();

            customerDto.Id = customer.Id; //posto je postavka bazirana po Id moramo vratiti taj Id KLIJENTU!

            //Ovdje kao dio RESTfull konvencije treba da vratimo klijentu URI "OD NOVO-KREIRANOG RESURSA"
            //Ideja Uri je: /api/customers/10-novo-kreirani-customer
            //Za Pribavljanje Uri za Request koristi Request property od Kontrolera, /customers "/" + customer.Id
            //Drugi argumenat za novokreirani Uri treba da prosljedimo kreirani "Actual Object" customerDto
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/1  (Update customer data in the request body)
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto) //customerDto je nas INPUT objekat-param
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            //Kompajler izbacuje generic Mapper.Map<T,T> jer vidi nove prosljedjene parametre u konst. metod (customerDto, customerInDb)
            Mapper.Map(customerDto, customerInDb);
            //AutoMapper umjesto "customerInDb.Name = customerDto.Name; da bi automatski spasavali propertise u DB.
            _context.SaveChanges();
            return Ok();
        }

        //DELETE api/customers/1
        [HttpDelete]
        //public void DeleteCustomer(int id)
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}

