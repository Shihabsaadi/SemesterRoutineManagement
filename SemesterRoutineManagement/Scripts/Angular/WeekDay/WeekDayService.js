app.service("weekDayService", ['$http', function ($http) {
   
    this.getWeekDay = function()
    {
        return $http.get("/WeekDay/GetWeekDayList");
    }
    this.saveWeekDay = function (data) {
        return $http.post('/WeekDay/SaveWeekDay', JSON.stringify(data))
    }
    
}]);