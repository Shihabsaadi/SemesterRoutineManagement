app.service("sessionService", ['$http', function ($http) {
   
    this.getSession = function()
    {
        return $http.get("/Session/GetSessionList");
    }
    this.saveSession = function (data) {
        return $http.post('/Session/SaveSession', JSON.stringify(data))
    }
    
}]);