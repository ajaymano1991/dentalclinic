using System;
using System.Collections.Generic;
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
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            
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
        public ActionResult DentistLogin(DentistLogin dentist)
        {
            if (ModelState.IsValid)
            {
                using (DentistLoginModel dm = new DentistLoginModel())
                {
                    var v = dm.Dentists.Where(a => a.username.Equals(dentist.username) && a.password.Equals(dentist.password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LoggedDentistID"] = v.Id.ToString();
                        Session["LoggedDentistUsername"] = v.username.ToString();
                        Session["LoggedDentistFullName"] = v.name.ToString();
                        return RedirectToAction("DentistPanel");
                    }
                    else
                    {
                        dentist.LoginErrorMessage = "Wrong username or password.";
                        return View("DentistLogin", dentist);
                    }
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
                using (PatientLoginModel pm = new PatientLoginModel())
                {
                    var v = pm.Patients.Where(a => a.username.Equals(patient.username) && a.password.Equals(patient.password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LoggedPatientID"] = v.Id.ToString();
                        Session["LoggedPatientUsername"] = v.username.ToString();
                        Session["LoggedPatientFullName"] = v.name.ToString();
                        return RedirectToAction("PatientPanel");
                    }
                    else
                    {
                        patient.LoginErrorMessage = "Wrong Username or Password";
                        return View("PatientLogin", patient);
                    }
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
            AppointmentModel dm = new AppointmentModel();
            List<Appointment> appointmentList1 = db.Appointments.ToList();
            List<Appointment> appointmentList = new List<Appointment>();
            string s = Session["LoggedPatientID"].ToString();
            int n = Convert.ToInt32(s);
            foreach (var appointment in appointmentList1)
            {
                if (appointment.PatientId == n)
                {
                    appointmentList.Add(appointment);
                }

            }
            ViewBag.appointmentList = appointmentList;
            return View();
        }


        public ActionResult AppointmentList()
        {

            AppointmentModel dm = new AppointmentModel();
            List<Appointment> appointmentList1 = db.Appointments.ToList();
            List<Appointment> appointmentList = new List<Appointment>();
            string s = Session["LoggedDentistID"].ToString();
            int n = Convert.ToInt32(s);
            foreach (var appointment in appointmentList1)
            {
                if (appointment.DatientId == n)
                {
                    appointmentList.Add(appointment);
                }

            }
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
                var v = db.Appointments
                          .Where(a => a.DatientId == appointment.dentist_id && a.Datetime == DateTime.Parse(appointment.datetime))
                          .FirstOrDefault();
                if (v != null)
                {
                    ViewBag.Message2 = "Time slot is not available";
                    return View("TakeAppointment", appointment);
                }
                else
                {
                    var newAppointment = new DentalClinicReservationAndManagementSystem.Appointment
                    {
                        PatientId = appointment.patient_id,
                        DatientId = appointment.dentist_id,
                        Datetime = DateTime.Parse(appointment.datetime)
                    };
                    db.Appointments.Add(newAppointment);
                    db.SaveChanges();
                    return RedirectToAction("PatientPanel");
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
            foreach ( Appointment ap in aList )
            {
                if ( appointment.DatientId == ap.DatientId )
                {
                    fList.Add(ap);
                }
            }
            ViewBag.fList = fList;
            return View();
        }

    }
}
