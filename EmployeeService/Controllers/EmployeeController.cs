using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
            {
               return entities.Employees.ToList();
            }
        }

        public HttpResponseMessage Get(int ID)
        {
            using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
            {
              var entity=  entities.Employees.FirstOrDefault(e=>e.ID==ID);
                if(entity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID" +
                        ID.ToString() + "Not found");
                }
            }
        }

        //-> Post data to the server 
        public HttpResponseMessage Post ([FromBody] Employee employee)
        {
            try { 
            using (EmployeeAPIEntities entites = new EmployeeAPIEntities())
            {
                entites.Employees.Add(employee);
                entites.SaveChanges();
                    var message = Request.CreateErrorResponse(HttpStatusCode.Created, employee.ToString());
                    message.Headers.Location =
                   new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //---------------------------------------Post Delete Method------------------------------------------------
        public HttpResponseMessage Delete (int id)
        {
            try {
                using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id= " + id.ToString() + " Not Found");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,"Done Succesfully");
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }

        //------------------------------PUT Method----------------------------------
        public HttpResponseMessage Put(int id,[FromBody]Employee employee)
        {
            try
            {
                using (EmployeeAPIEntities entities = new EmployeeAPIEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity==null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee With ID " + id.ToString() + "Not Found");

                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.Lastname = employee.Lastname;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Updated Successfully");
                    }
                   
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            }
           


    }
}
