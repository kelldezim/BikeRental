using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //Get /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customer.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }
        //Get /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customer.SingleOrDefault(x => x.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer, CustomerDto>(customer);
        }
        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customer.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customer.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }
        //DELETE 
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customer.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customer.Remove(customerInDb);
            _context.SaveChanges();
        }
            
    }
}
