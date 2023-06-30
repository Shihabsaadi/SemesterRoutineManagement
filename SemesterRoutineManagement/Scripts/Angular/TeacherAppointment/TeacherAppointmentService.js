app.service("teacherAppointmentService", ['$http', function ($http) {
   
    this.getTeacherAppointment = function()
    {
        return $http.get("/TeacherAppointment/GetTeacherAppointmentList");
    }
    this.getTeacher = function () {
        return $http.get("/TeacherAppointment/GetTeacherList");
    }
    this.getCourse = function () {
        return $http.get("/TeacherAppointment/GetCourseList");
    }
    //Save 
    this.save = function (data) {
        return $http.post('/TeacherAppointment/Save', JSON.stringify(data))
    }
    
}]);