app.service("courseService", ['$http', function ($http) {
   
    this.getCourse = function()
    {
        return $http.get("/Course/GetCourseList");
    }
    this.saveCourse = function (data) {
        return $http.post('/Course/SaveCourse', JSON.stringify(data))
    }
    
}]);