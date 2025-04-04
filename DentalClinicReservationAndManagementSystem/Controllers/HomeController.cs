using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DentalClinicReservationAndManagementSystem.Models;

namespace DentalClinicReservationAndManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        DentalClinicEntities db = new DentalClinicEntities();
        public ActionResult Index()
        {
            List<Dentist> dentistList1 = db.Dentists.ToList();
            List<Appointment> todaysAppointments = db.Appointments.ToList() 
                                                   .Where(x => x.PreferredDateTime.Date == DateTime.Now.Date)
                                                   .ToList();
            var dentalNews = db.DentalNews.ToList()
                           .Where(n => n.IsActive)
                           .ToList();

            List<FeedBack> dentalFeedBackList = db.FeedBacks.ToList();
                          

            List<Dentist> dentistList = new List<Dentist>();
            foreach (var dentist in dentistList1)
            {
                if (dentist.isAvailable == true)
                {
                    dentistList.Add(dentist);
                }

            }

            // Store data in ViewBag
            ViewBag.DentalNews = dentalNews;
            ViewBag.dentistList = dentistList;
            ViewBag.feedBack = dentalFeedBackList;
            ViewBag.TodaysAppointments = todaysAppointments;
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Dentists()
        {
            return View();
        }
        public ActionResult DentistLogin()
        {
            if (Session["LoggedDentistID"] != null) return RedirectToAction("DentistPanel");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DentistLogin(Dentist dentist)
        {
            if (ModelState.IsValid)
            {

                var v = db.Dentists.Where(a => a.Username.Equals(dentist.Username) && a.Password.Equals(dentist.Password)).FirstOrDefault();
                if (v != null)
                {
                    Session["LoggedDentistID"] = v.Id.ToString();
                    Session["LoggedDentistUsername"] = v.Username.ToString();
                    Session["LoggedDentistFullName"] = v.Name.ToString();
                    return RedirectToAction("DentistPanel");
                }
                else
                {
                    TempData["Error"] = "Wrong username or password.";
                    return RedirectToAction("DentistLogin");

                }

            }
            return RedirectToAction("DentistLogin");
        }
        public ActionResult AfterDentistLogin()
        {

            return View();

        }
        public ActionResult DentistPanel()
        {
            return View();
        }
        public ActionResult PatientLogin()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientLogin(PatientLogin patient)
        {
            if (ModelState.IsValid)
            {
               
                    var v = db.PatientRegisters.Where(a => a.Username.Equals(patient.username) && a.Password.Equals(patient.password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LoggedPatientID"] = v.Id.ToString();
                        Session["LoggedPatientUsername"] = v.Username.ToString();
                        Session["LoggedPatientFullName"] = v.PatientName.ToString();
                        return RedirectToAction("PatientPanel");
                    }
                    else
                    {
                        patient.LoginErrorMessage = "Wrong Username or Password";
                        return View("PatientLogin");
                    }
                
            }
            return RedirectToAction("PatientLogin");
        }
        public ActionResult PatientPanel()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Appointment()
        {
            /*if (Session["LoggedPatientUsername"] == null) return RedirectToAction("PatientLogin");
            DentistModel dm = new DentistModel();
            List<Dentist> dentistList1 = dm.Dentists.ToList();
            List<Dentist> dentistList = new List<Dentist>();
            foreach ( var dentist in dentistList1 )
            {
                if ( dentist.isAvailable == 1 )
                {
                    dentistList.Add(dentist);
                }
                
            }
            ViewBag.dentistList = dentistList;*/
            return View();
        }
        [HttpPost]
        public ActionResult Appointment(FormCollection collection)
        {
            foreach (string key in collection.AllKeys)
            {
                Response.Write("Key = " + key + " , ");
                Response.Write("Value = " + collection[key]);
                Response.Write("<br/>");
            }

            return View();
        }

        public ActionResult PatientRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PatientRegister(Patient patient)
        {
            ViewBag.Message1 = "";
            ViewBag.Message2 = "";
            if (ModelState.IsValid)
            {
                var ExistingPatient = db.PatientRegisters.Where(a => a.Username == patient.username).FirstOrDefault();
                if (ExistingPatient != null)
                {
                    ViewBag.Message2 = "Username already exists";
                    return View("PatientRegister", patient);
                }
                var PatientEmail = db.PatientRegisters.Where(a => a.Email == patient.email).FirstOrDefault();
                if (PatientEmail != null)
                {
                    ViewBag.Message1 = "Email already exists";
                    return View("PatientRegister", patient);
                }
                else
                {
                    var newPatientRegister = new PatientRegister
                    {
                        PatientName = patient.name,
                        Username = patient.username,
                        Password = patient.password,
                        Email = patient.email,
                        Address = patient.address,
                        DOB = patient.dateofbirth,
                        Contact = patient.contact,
                        BloodGroup = patient.bloodgroup
                    };
                    db.PatientRegisters.Add(newPatientRegister);
                    db.SaveChanges();
                    return RedirectToAction("PatientPanel");
                }
            }
            return RedirectToAction("PatientLogin");
        }

        public ActionResult SelectDentist()
        {
            if (Session["LoggedPatientUsername"] == null) return RedirectToAction("PatientLogin");
            DentistModel dm = new DentistModel();
            List<Dentist> dentistList1 = db.Dentists.ToList();
            List<Dentist> dentistList = new List<Dentist>();
            foreach (var dentist in dentistList1)
            {
                if (dentist.isAvailable == true)
                {
                    dentistList.Add(dentist);
                }

            }
            ViewBag.dentistList = dentistList;
            return View();
        }
        public ActionResult SelectedAppointmentList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SelectedAppointmentList(FormCollection form)
        {
            string option = form["ddl"].ToString();
            ViewBag.selectedDentistOption = option;
            return View();
        }

        public ActionResult MyAppointments()
        {
            List<Appointment> appointmentList1 = db.Appointments.ToList();
            List<Appointment> appointmentList = new List<Appointment>();
            string s = Session["LoggedPatientID"].ToString();
            int n = Convert.ToInt32(s);
            foreach (var appointment in appointmentList1)
            {
                //if (appointment.PatientId == n)
                //{
                    appointmentList.Add(appointment);
                //}

            }
            ViewBag.appointmentList = appointmentList;
            return View();
        }


        public ActionResult AppointmentList()
        {
            List<Appointment> appointmentList = db.Appointments.ToList();
            //string s = Session["LoggedDentistID"].ToString();
            //int n = Convert.ToInt32(s);
            ViewBag.appointmentList = appointmentList;
            return View();
        }

        public ActionResult CreatePrescription()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePrescription(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                using (PrescriptionModel prm = new PrescriptionModel())
                {


                    prm.Prescriptions.Add(prescription);
                    prm.SaveChanges();
                    return RedirectToAction("DentistPanel");
                    //}

                }
            }
            return View();
        }

        public ActionResult TakeAppointment()
        {
            List<Dentist> appointmentList2 = db.Dentists.ToList();
            ViewBag.appointmentList2 = appointmentList2;
            return View();
        }
        [HttpPost]
        public ActionResult TakeAppointment(DentalClinicReservationAndManagementSystem.Models.Appointment appointment)
        {
            ViewBag.Message2 = "";
            if (ModelState.IsValid)
            {
                var appointments = db.Appointments.AsEnumerable() 
                                    .Where(a => a.PreferredDateTime == appointment.PreferredDateTime)
                                    .ToList();

                if (appointments.Count>0)
                {
                    ViewBag.Message2 = "Time slot is not available";
                    return View("Index", appointment);
                }
                else
                {
                    var newAppointment = new DentalClinicReservationAndManagementSystem.Appointment
                    {
                        PatientName = appointment.PatientName,
                        Email = appointment.Email,
                        PhoneNumber = appointment.PhoneNumber,
                        PreferredDateTime =appointment.PreferredDateTime,
                        ReasonForAppointment=appointment.ReasonForAppointment,
                        CreatedAt=DateTime.Now
                    };
                    db.Appointments.Add(newAppointment);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                        throw;
                    }
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult SeeAppointments()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SeeAppointments(Appointment appointment)
        {
            List<Appointment> aList = db.Appointments.ToList();
            List<Appointment> fList = new List<Appointment>();
            foreach (Appointment ap in aList)
            {
                //if (appointment.DatientId == ap.DatientId)
                //{
                    fList.Add(ap);
               // }
            }
            ViewBag.fList = fList;
            return View();
        }

        [HttpPost]
        public ActionResult subscription(string email, string phoneNumber)
        {
            var subscribied = db.subscriptions.AsEnumerable()
                                     .Where(a => a.Email == email && a.PhoneNumber == phoneNumber).FirstOrDefault();


            if (subscribied != null)
            {
                ViewBag.Message2 = "Already subscribied";
                return View("Index");
            }
            else
            {
                var newsubscription = new subscription()
                {
                    Email = email,
                    PhoneNumber=phoneNumber
                };

                db.subscriptions.Add(newsubscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult SubmitFeedback(string PatientName, string FeedbackText, int Rating)
        {
            
                var newFeedback = new FeedBack()
                {
                    PatientName = PatientName,
                    FeedbackText = FeedbackText,
                    Rating = Rating,
                    CreatedAt = DateTime.Now
                };

                db.FeedBacks.Add(newFeedback);
                db.SaveChanges();

                ViewBag.Message2 = "Thank you for your feedback!";
              return RedirectToAction("Index");

        }

        public ActionResult Dashboard()
        {
            var patientCount = db.Appointments.Count();
            var appointmentCount = db.Appointments.Count();

            ViewBag.PatientCount = patientCount;
            ViewBag.AppointmentCount = appointmentCount;
            return View();
        }


        public JsonResult GetRealTimeData()
        {
            Random rnd = new Random();
            var labels = Enumerable.Range(1, 10).Select(i => $"{i} min ago").ToList();
            var data = labels.Select(_ => rnd.Next(10, 50)).ToList();

            int activeUsers = MvcApplication.GetActiveUserCount(); // Get real-time users
            int totalVisitors = db.SiteStats.OrderByDescending(s => s.Id).FirstOrDefault()?.TotalVisitors ?? 0;

            return Json(new { labels, data, activeUsers, totalVisitors }, JsonRequestBehavior.AllowGet);
        }


    }
}
